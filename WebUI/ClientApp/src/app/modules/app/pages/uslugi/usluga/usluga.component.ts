import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import * as moment from 'moment';
import { SelectItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { map } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { NowaMisjaForm } from 'src/app/shared/models/misje/nowa-misja-form.model';
import { NowaUslugaForm } from 'src/app/shared/models/usluga/nowa-usluga-form.model';
import { KlienciClient, KlientDto, MisjaDto, StatusUslugiDto, StatusyUslugiClient, UslugiClient, UtworzUslugeCommand } from 'src/app/web-api-client';
import { MisjeComponent } from '../../misje/misje/misje.component';

@Component({
  selector: 'app-usluga',
  templateUrl: './usluga.component.html',
  styleUrls: ['./usluga.component.css'],
  providers: [DialogService, MessageToast]
})
export class UslugaComponent implements OnInit {
  formBuilder: IFormBuilder;
  nowaUslugaForm: IFormGroup<NowaUslugaForm>;
  pl = Kalendarz.pl;
  misje: NowaMisjaForm[] = [];
  statusUtworzonejUslugi: StatusUslugiDto;
  klienci: SelectItem<KlientDto>[];

  constructor(
    formBuilder: FormBuilder,
    private dialogService: DialogService,
    private uslugiClient: UslugiClient,
    private messageToast: MessageToast,
    private router: Router,
    private statusyUslugiClient: StatusyUslugiClient,
    private klienciClient: KlienciClient
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
    this.pobierzStatusUtworzonejUslugi();
    this.pobierzKlientow();
  }

  zbudujFormularz(): void {
    this.nowaUslugaForm = this.formBuilder.group<NowaUslugaForm>({
      dataPrzyjeciaZalecenia: [this.dzisiaj()],
      klient: [null]
    });
  }

  pobierzStatusUtworzonejUslugi(): void {
    this.statusyUslugiClient.pobierzStatusUslugi("Utworzona").subscribe(
      (statusUslugi: StatusUslugiDto) => {
        this.statusUtworzonejUslugi = statusUslugi;
      }
    );
  }

  pobierzKlientow(): void {
    this.klienciClient.pobierzKlientow(null, null, null, null, 'imie 1', 0, 0)
      .pipe(map(klienci => klienci.results.map(klient => {
        if (klient.pesel) {
          return { label: `${klient.imie} ${klient.nazwisko}, PESEL: ${klient.pesel}`, value: klient } as SelectItem<KlientDto>;
        } else {
          return { label: `${klient.nazwa}, NIP: ${klient.nip}, REGON: ${klient.regon}`, value: klient } as SelectItem<KlientDto>;
        }
      }
      )))
      .subscribe(
        (klienci: SelectItem<KlientDto>[]) => this.klienci = klienci
      );
  }

  dzisiaj(): Date {
    const dzisiaj = moment().startOf('day').toDate();
    return dzisiaj;
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
      dataPrzyjeciaZlecenia: this.nowaUslugaForm.value.dataPrzyjeciaZalecenia.getTime(),
      idKlienta: this.nowaUslugaForm.value.klient.id,
      idStatusuUslugi: this.statusUtworzonejUslugi.id,
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
        promien: misja.promien,
        drony: misja.drony
      } as MisjaDto))
    } as UtworzUslugeCommand;
    this.uslugiClient.utworzUsluge(command)
      .subscribe(
        () => {
          this.messageToast.success('Utworzono usługę.');
          this.router.navigate(['/strona-glowna']);
        }
      );
  }
}
