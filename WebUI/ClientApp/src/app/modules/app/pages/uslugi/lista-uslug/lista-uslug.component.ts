import { Component, OnInit } from '@angular/core';
import { LazyLoadEvent, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { PracownicyService } from 'src/app/core/services/pracownicy.service';
import { MisjaForm } from 'src/app/shared/models/misje/misja-form.model';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';
import { MisjaDto, MisjeClient, PagedResultOfUslugaDto, UslugaDto, UslugiClient, UtworzMisjeUslugiCommand } from 'src/app/web-api-client';
import { MisjeDialogComponent } from '../../../components/misje/misje-dialog.component';
import { MisjeComponent } from '../../misje/misje/misje.component';

@Component({
  selector: 'app-lista-uslug',
  templateUrl: './lista-uslug.component.html',
  styleUrls: ['./lista-uslug.component.css'],
  providers: [DialogService]
})
export class ListaUslugComponent {
  uslugi: UslugaDto[] = [];
  loading = true;
  liczbaRekordow = 0;
  wybranaUsluga: UslugaDto;
  indeksWybranejUslugi: number;
  kontekstoweMenu: MenuItem[];

  constructor(
    private uslugiClient: UslugiClient,
    private dialogService: DialogService,
    private pracownicyService: PracownicyService,
    private misjeClient: MisjeClient,
    private messageToast: MessageToast,
  ) { }

  pobierzUslugi(event: LazyLoadEvent): void {
    this.loading = true;
    this.uslugiClient.pobierzUslugi(event.first, event.rows, `${event.sortField} ${event.sortOrder}`)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (uslugi: PagedResultOfUslugaDto) => {
          this.uslugi = uslugi.results;
          this.liczbaRekordow = uslugi.rowCount;
        }
      );
  }

  edytujMisje(tableRef: Table): void {
    const misjeDialog = this.dialogService.open(MisjeDialogComponent, {
      header: 'Wybór misji do edycji',
      width: '80%',
      data: { idUslugi: this.wybranaUsluga.id }
    });

    misjeDialog.onClose.subscribe(
      (misja: MisjaDto) => {
        if (misja === null) {
          const dialog = this.dialogService.open(MisjeComponent, {
            header: 'Dodanie misji',
            width: '80%'
          });

          dialog.onClose.subscribe(
            (misja: MisjaForm) => {
              if (!misja)
                return;
              const command = {
                nazwa: misja.nazwa,
                opis: misja.opis,
                idTypuMisji: misja.typ.id,
                maksymalnaWysokoscLotu: misja.maksymalnaWysokoscLotu,
                idStatusuMisji: misja.status.id,
                dataRozpoczecia: misja.dataRozpoczecia.getTime(),
                dataZakonczenia: misja.dataRozpoczecia.getTime(),
                idPracownika: misja.przypisanyPracownik.subjectId,
                szerokoscGeograficzna: misja.szerokoscGeograficzna,
                dlugoscGeograficzna: misja.dlugoscGeograficzna,
                promien: misja.promien
              } as UtworzMisjeUslugiCommand;
              this.uslugiClient.utworzMisjeUslugi(this.wybranaUsluga.id, command).subscribe();
            }
          );
        } else if (misja === undefined) {
          return;
        } else {
          misja = misja as MisjaDto;
          this.pracownicyService.pobierzPracownikow()
            .subscribe((pracownicy: Pracownik[]) => {
              const edytowanaMisja: MisjaForm = {
                id: misja.id,
                nazwa: misja.nazwa,
                dataRozpoczecia: new Date(misja.dataRozpoczecia),
                dataZakonczenia: new Date(misja.dataZakonczenia),
                opis: misja.opis,
                typ: misja.typMisji,
                status: misja.statusMisji,
                maksymalnaWysokoscLotu: misja.maksymalnaWysokoscLotu,
                przypisanyPracownik: pracownicy.find(x => x.subjectId === misja.idPracownika),
                drony: misja.misjeDrony.map(x => x.dron),
                szerokoscGeograficzna: misja.szerokoscGeograficzna,
                dlugoscGeograficzna: misja.dlugoscGeograficzna,
                promien: misja.promien
              }

              const dialogEdycjiMisji = this.dialogService.open(MisjeComponent, {
                header: 'Edycja misji',
                width: '80%',
                data: edytowanaMisja
              });

              dialogEdycjiMisji.onClose.subscribe(
                () => {
                  const event = tableRef.createLazyLoadMetadata();
                  this.pobierzUslugi(event);
                }
              );
            });
        }
      }
    );
  }

  usunMisje(): void {
    const misjeDialog = this.dialogService.open(MisjeDialogComponent, {
      header: 'Wybór misji do usunięcia',
      width: '80%',
      data: { idUslugi: this.wybranaUsluga.id }
    });
    misjeDialog.onClose.subscribe(
      (misja: MisjaDto) => {
        if (misja) {
          this.misjeClient.usunMisje(misja.id)
            .subscribe(
              () => this.messageToast.success('Usunięto misję.')
            );
        }
      });
  }

  naWybraniuUslugi(tableRef: Table): void {
    this.kontekstoweMenu = [
      { label: 'Edytuj misję', icon: 'pi pi-fw pi-star-o', command: () => this.edytujMisje(tableRef) },
      { label: 'Usuń misję', icon: 'pi pi-fw pi-times', command: () => this.usunMisje() }
    ];
  }
}
