import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import * as moment from 'moment';
import { DialogService } from 'primeng/dynamicdialog';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { KlientDto } from 'src/app/web-api-client';
import { KlienciComponent } from '../../klienci/klienci/klienci.component';
import { MisjeComponent } from '../../misje/misje/misje.component';

@Component({
  selector: 'app-nowa-usluga',
  templateUrl: './nowa-usluga.component.html',
  styleUrls: ['./nowa-usluga.component.css'],
  providers: [DialogService]
})
export class NowaUslugaComponent {
  nowaUslugaForm = this.formBuilder.group({
    dataPrzyjeciaZalecenia: [this.dzisiaj()]
  });
  controls = this.nowaUslugaForm.controls;
  pl = Kalendarz.pl;
  klient: KlientDto;
  nazwaKlienta: string;
  tekstIdentyfikacyjnyKlienta: string;

  constructor(
    private formBuilder: FormBuilder,
    public dialogService: DialogService
  ) { }

  dzisiaj(): Date {
    const dzisiaj = moment().startOf('day').toDate();
    return dzisiaj;
  }

  wybierzKlientaOnClick(): void {
    const dialog = this.dialogService.open(KlienciComponent, {
      header: 'Wybierz klienta',
      width: '80%'
    });

    dialog.onClose.subscribe(
      (klient: KlientDto) => {
        if (klient?.pesel) {
          this.nazwaKlienta = `${klient.imie} ${klient.nazwisko}`;
          this.tekstIdentyfikacyjnyKlienta = `PESEL: ${klient.pesel}`;
        } else {
          this.nazwaKlienta = klient.nazwa;
          this.tekstIdentyfikacyjnyKlienta = `REGON: ${klient.regon}, NIP: ${klient.nip}`;
        }
        this.klient = klient;
      });
  }

  dodajMisjeOnClick(): void {
    const dialog = this.dialogService.open(MisjeComponent, {
      header: 'Dodaj misje',
      width: '80%'
    });

    dialog.onClose.subscribe(
      (misja) => {

      }
    );
  }
}
