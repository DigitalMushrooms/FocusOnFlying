import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { PracownicyService } from 'src/app/core/services/pracownicy.service';
import { MisjaForm } from 'src/app/shared/models/misje/misja-form.model';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';
import { MisjaDto, UslugaDto, UslugiClient } from 'src/app/web-api-client';
import { MisjeDialogComponent } from '../../../components/misje/misje-dialog.component';
import { MisjeComponent } from '../../misje/misje/misje.component';

@Component({
  selector: 'app-lista-uslug',
  templateUrl: './lista-uslug.component.html',
  styleUrls: ['./lista-uslug.component.css'],
  providers: [DialogService]
})
export class ListaUslugComponent implements OnInit {
  uslugi: UslugaDto[] = [];
  wybranaUsluga: UslugaDto;
  indeksWybranejUslugi: number;
  kontekstoweMenu: MenuItem[];

  constructor(
    private uslugiClient: UslugiClient,
    private dialogService: DialogService,
    private pracownicyService: PracownicyService
  ) { }

  ngOnInit(): void {
    this.pobierzUslugi();
    this.utworzMenuKontekstowe();
  }

  pobierzUslugi(): void {
    this.uslugiClient.pobierzUslugi().subscribe(
      (uslugi: UslugaDto[]) => {
        this.uslugi = uslugi;
      }
    );
  }

  utworzMenuKontekstowe(): void {
    this.kontekstoweMenu = [
      { label: 'Edytuj misję', icon: 'pi pi-fw pi-star-o', command: () => this.edytujMisje() },
      { label: 'Usuń misję', icon: 'pi pi-fw pi-times', command: () => this.usunMisje() }
    ];
  }

  edytujMisje(): void {
    const misjeDialog = this.dialogService.open(MisjeDialogComponent, {
      header: 'Wybór misji do edycji',
      width: '80%',
      data: { idUslugi: this.wybranaUsluga.id }
    });

    misjeDialog.onClose.subscribe(
      (misja: MisjaDto) => {
        if (!misja)
          return;
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
                this.pobierzUslugi();
              }
            );
          });
      }
    );
  }

  usunMisje(): void {

  }

  naWybraniuUslugi(event: { index: number; }): void {
    this.indeksWybranejUslugi = event.index;
  }
}
