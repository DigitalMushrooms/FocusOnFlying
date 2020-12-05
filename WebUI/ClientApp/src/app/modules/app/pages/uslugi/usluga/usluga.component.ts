import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { findIndex } from 'lodash-es';
import * as moment from 'moment';
import { MenuItem, SelectItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { map } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { NowaMisjaForm } from 'src/app/shared/models/misje/nowa-misja-form.model';
import { NowaUslugaForm } from 'src/app/shared/models/usluga/nowa-usluga-form.model';
import { KlienciClient, KlientDto, MisjaDronDto, MisjaDto, StatusUslugiDto, StatusyUslugiClient, UslugiClient, UtworzUslugeCommand } from 'src/app/web-api-client';
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
  kontekstoweMenu: MenuItem[];
  wybranaMisja: NowaMisjaForm;

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
    this.utworzMenuKontekstowe();
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

  utworzMenuKontekstowe(): void {
    this.kontekstoweMenu = [
      { label: 'Edytuj misję', icon: 'pi pi-fw pi-star-o', command: () => this.edytujMisje() },
      { label: 'Usuń misję', icon: 'pi pi-fw pi-times', command: () => this.edytujMisje() }
    ];
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

  edytujMisje(): void {
    const index = findIndex(this.misje, (misja) => misja.nazwa === this.wybranaMisja.nazwa);

    const dialog = this.dialogService.open(MisjeComponent, {
      header: 'Edycja misji',
      width: '80%',
      data: this.wybranaMisja
    });

    dialog.onClose.subscribe(
      (misja) => {
        if (!misja)
          return;
        this.misje[index] = misja;
      }
    );
  }

  usunMisje(): void {
    const index = findIndex(this.misje, (misja) => misja.nazwa === this.wybranaMisja.nazwa);
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
        misjeDrony: misja.drony?.map(d => ({ idDrona: d.id } as MisjaDronDto)) ?? null,
        idPracownika: misja.przypisanyPracownik.subjectId
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
