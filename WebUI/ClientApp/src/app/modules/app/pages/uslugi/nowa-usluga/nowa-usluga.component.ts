import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { DialogService } from 'primeng/dynamicdialog';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { NowaUslugaForm } from 'src/app/shared/models/nowa-usluga/nowa-usluga-form.model';
import { KlientDto, MisjaDto, StatusUslugiDto, StatusyUslugiClient, UslugiClient, UtworzUslugeCommand } from 'src/app/web-api-client';
import { KlienciComponent } from '../../klienci/klienci/klienci.component';
import { MisjeComponent } from '../../misje/misje/misje.component';

@Component({
  selector: 'app-nowa-usluga',
  templateUrl: './nowa-usluga.component.html',
  styleUrls: ['./nowa-usluga.component.css'],
  providers: [DialogService, MessageToast]
})
export class NowaUslugaComponent implements OnInit {
  nowaUslugaForm = this.formBuilder.group({
    dataPrzyjeciaZalecenia: [this.dzisiaj()]
  });
  controls = this.nowaUslugaForm.controls;
  pl = Kalendarz.pl;
  klient: KlientDto;
  nazwaKlienta: string;
  tekstIdentyfikacyjnyKlienta: string;
  misje: NowaUslugaForm[] = [];
  statusUslugi: StatusUslugiDto;

  constructor(
    private formBuilder: FormBuilder,
    private dialogService: DialogService,
    private uslugiClient: UslugiClient,
    private messageToast: MessageToast,
    private router: Router,
    private statusyUslugiClient: StatusyUslugiClient
  ) { }

  ngOnInit(): void {
    this.statusyUslugiClient.pobierzStatusUslugi("Utworzona").subscribe(
      (statusUslugi: StatusUslugiDto) => {
        this.statusUslugi = statusUslugi;
      }
    );
  }

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
        if (!klient)
          return;
        if (klient.pesel) {
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
      header: 'Dodanie misji',
      width: '80%'
    });

    dialog.onClose.subscribe(
      (misja) => {
        if (!misja)
          return;
        this.misje.push(misja);
      }
    );
  }

  edytujMisje(index: number): void {
    const misja = this.misje[index];

    const dialog = this.dialogService.open(MisjeComponent, {
      header: 'Edycja misji',
      width: '80%',
      data: misja
    });

    dialog.onClose.subscribe(
      (misja) => {
        if (!misja)
          return;
        this.misje[index] = misja;
      }
    );
  }

  usunMisje(index: number): void {
    this.misje.splice(index, 1);
  }

  zapiszUsluge(): void {
    const command = {
      dataPrzyjeciaZlecenia: this.controls['dataPrzyjeciaZalecenia'].value.getTime(),
      idKlienta: this.klient.id,
      idStatusuUslugi: this.statusUslugi.id,
      misje: this.misje.map(misja => ({
        nazwa: misja.nazwa,
        opis: misja.opis,
        idTypuMisji: misja.typ.id,
        maksymalnaWysokoscLotu: misja.maksymalnaWysokoscLotu,
        idStatusuMisji: misja.statusId,
        dataRozpoczecia: misja.dataRozpoczecia.getTime(),
        dataZakonczenia: misja.dataZakonczenia?.getTime(),
        szerokoscGeograficzna: misja.szerokoscGeograficzna,
        dlugoscGeograficzna: misja.dlugoscGeograficzna,
        promien: misja.promien
      } as MisjaDto))
    } as UtworzUslugeCommand;
    this.uslugiClient.utworzUsluge(command)
      .subscribe(
        () => {
          this.messageToast.success('Utworzono usługę.');
          this.router.navigate(['/home']);
        }
      );
  }
}
