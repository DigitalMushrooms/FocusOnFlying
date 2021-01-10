import { Component } from '@angular/core';
import { LazyLoadEvent, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { PracownicyService } from 'src/app/core/services/pracownicy.service';
import { MisjaForm } from 'src/app/shared/models/misje/misja-form.model';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';
import { FakturyClient, MisjaDto, MisjeClient, Operation, PagedResultOfUslugaDto, StatusUslugiDto, StatusyUslugiClient, UslugaDto, UslugiClient, UtworzMisjeUslugiCommand } from 'src/app/web-api-client';
import { FakturaDialogComponent } from '../../../components/faktura/faktura-dialog.component';
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
  kontekstoweMenu: MenuItem[];

  constructor(
    private uslugiClient: UslugiClient,
    private dialogService: DialogService,
    private pracownicyService: PracownicyService,
    private misjeClient: MisjeClient,
    private messageToast: MessageToast,
    private statusyUslugiClient: StatusyUslugiClient,
    private fakturyClient: FakturyClient
  ) { }

  pobierzUslugi(event: LazyLoadEvent): void {
    this.loading = true;
    this.uslugiClient.pobierzUslugi(null, null, event.first, event.rows, `${event.sortField} ${event.sortOrder}`)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (uslugi: PagedResultOfUslugaDto) => {
          this.uslugi = uslugi.results;
          this.liczbaRekordow = uslugi.rowCount;
        }
      );
  }

  utworzDialogMisji(tableRef: Table, doOdczytu: boolean, naglowekListyMisji: string, naglowekMisji: string): void {
    const misjeDialog = this.dialogService.open(MisjeDialogComponent, {
      header: naglowekListyMisji,
      width: '80%',
      data: { idUslugi: this.wybranaUsluga.id }
    });

    misjeDialog.onClose.subscribe(
      (misja: MisjaDto) => {
        if (misja === null) {
          this.dodanieMisji();
        } else if (misja === undefined) {
          this.odswiezTabele(tableRef);
        } else {
          this.pracownicyService.pobierzPracownikow()
            .subscribe(
              (pracownicy: Pracownik[]) => {
                const misjaForm: MisjaForm = {
                  id: misja.id,
                  nazwa: misja.nazwa,
                  dataRozpoczecia: misja.dataRozpoczecia ? new Date(misja.dataRozpoczecia) : null,
                  dataZakonczenia: misja.dataZakonczenia ? new Date(misja.dataZakonczenia) : null,
                  opis: misja.opis,
                  typ: misja.typMisji,
                  maksymalnaWysokoscLotu: misja.maksymalnaWysokoscLotu,
                  przypisanyPracownik: pracownicy.find(x => x.subjectId === misja.idPracownika),
                  drony: misja.misjeDrony.map(x => x.dron),
                  szerokoscGeograficzna: misja.szerokoscGeograficzna,
                  dlugoscGeograficzna: misja.dlugoscGeograficzna,
                  promien: misja.promien
                };

                const dialog = this.dialogService.open(MisjeComponent, {
                  header: naglowekMisji,
                  width: '80%',
                  data: { doOdczytu, misjaForm }
                });

                dialog.onClose.subscribe(
                  () => {
                    this.odswiezTabele(tableRef);
                  }
                );
              }
            );
        }
      }
    );
  }

  dodanieMisji(): void {
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
          dataRozpoczecia: misja.dataRozpoczecia?.getTime(),
          dataZakonczenia: misja.dataZakonczenia?.getTime(),
          idPracownika: misja.przypisanyPracownik.subjectId,
          szerokoscGeograficzna: misja.szerokoscGeograficzna,
          dlugoscGeograficzna: misja.dlugoscGeograficzna,
          promien: misja.promien
        } as UtworzMisjeUslugiCommand;
        this.uslugiClient.utworzMisjeUslugi(this.wybranaUsluga.id, command)
          .subscribe(
            () => this.messageToast.success('Dodano misję.')
          );
      }
    );
  }

  naWybraniuUslugi(tableRef: Table): void {
    this.kontekstoweMenu = [
      { label: 'Podejrzyj misję', icon: 'pi pi-fw pi-star-o', command: () => this.podejrzyjMisje(tableRef) },
      { label: 'Edytuj misję', icon: 'pi pi-fw pi-star', command: () => this.edytujMisje(tableRef), visible: this.edytujMisjeVisible() },
      { label: 'Usuń misję', icon: 'pi pi-fw pi-times', command: () => this.usunMisje(), visible: this.usunMisjeVisible() },
      { label: 'Zmień status usługi na "Wykonana"', icon: 'pi pi-fw pi-star', command: () => this.zmienStatusUslugi(tableRef, 'Zakończona'), visible: this.zmienStatusUslugiNaWykonanaVisible() },
      { label: 'Dodaj fakturę', icon: 'pi pi-fw pi-file', command: () => this.dodajFakture(tableRef), visible: this.dodajFaktureVisible() },
      { label: 'Edytuj fakturę', icon: 'pi pi-fw pi-file', command: () => this.edytujFakture(tableRef), visible: this.edytujFaktureVisible() },
      { label: 'Usuń fakturę', icon: 'pi pi-fw pi-times', command: () => this.usunFakture(tableRef), visible: this.usunFaktureVisible() },
    ];
  }

  podejrzyjMisje(tableRef: Table): void {
    this.utworzDialogMisji(tableRef, true, 'Wybór misji do podglądu', 'Podgląd misji');
  }

  edytujMisje(tableRef: Table): void {
    this.utworzDialogMisji(tableRef, false, 'Wybór misji do edycji', 'Edycja misji');
  }

  edytujMisjeVisible(): boolean {
    return this.wybranaUsluga.statusUslugi.nazwa !== 'Zakończona';
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

  usunMisjeVisible(): boolean {
    return this.wybranaUsluga.statusUslugi.nazwa !== 'Zakończona';
  }

  zmienStatusUslugi(tableRef: Table, status: string): void {
    this.statusyUslugiClient.pobierzStatusUslugi(status)
      .subscribe(
        (statusUslugi: StatusUslugiDto) => {
          const operacja = [{ op: 'add', path: `/idStatusuUslugi`, value: statusUslugi.id } as Operation];
          this.uslugiClient.zaktualizujUsluge(this.wybranaUsluga.id, operacja)
            .subscribe(
              () => {
                this.messageToast.success('Zaktualizowano status misji');
                this.odswiezTabele(tableRef);
              }
            );
        }
      );
  }

  zmienStatusUslugiNaWykonanaVisible(): boolean {
    if (this.wybranaUsluga.statusUslugi.nazwa === 'Zakończona')
      return false;
    const wynik = this.wybranaUsluga.misje.every(x => x.statusMisji.nazwa === 'Wykonana' || x.statusMisji.nazwa === 'Anulowana')
    return wynik;
  }

  dodajFakture(tableRef: Table): void {
    const fakturaDialog = this.dialogService.open(FakturaDialogComponent, {
      header: 'Dodanie faktury',
      width: '50%',
      data: { usluga: this.wybranaUsluga }
    });

    fakturaDialog.onClose.subscribe(() => this.odswiezTabele(tableRef));
  }

  dodajFaktureVisible(): boolean {
    return !this.wybranaUsluga.faktura;
  }

  edytujFakture(tableRef: Table): void {
    const fakturaDialog = this.dialogService.open(FakturaDialogComponent, {
      header: 'Edycja faktury',
      width: '50%',
      data: { usluga: this.wybranaUsluga }
    });

    fakturaDialog.onClose.subscribe(() => this.odswiezTabele(tableRef));

    this.odswiezTabele(tableRef);
  }

  odswiezTabele(tableRef: Table): void {
    const event = tableRef.createLazyLoadMetadata();
    event.first = 0;
    this.pobierzUslugi(event);
  }

  edytujFaktureVisible(): boolean {
    return !!this.wybranaUsluga.faktura;
  }

  usunFakture(tableRef: Table): void {
    this.fakturyClient.usunFakture(this.wybranaUsluga.faktura.id)
      .subscribe(
        () => {
          this.messageToast.success('Usunięto fakturę.');
          this.odswiezTabele(tableRef);
        }
      );
  }

  usunFaktureVisible(): boolean {
    return !!this.wybranaUsluga.faktura;
  }
}
