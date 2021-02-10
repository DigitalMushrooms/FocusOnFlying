import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { SelectItem } from 'primeng/api';
import { map } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { FirmaForm } from 'src/app/shared/models/klient/firma-form.model';
import { NowyKlientForm } from 'src/app/shared/models/klient/nowy-klient-form.model';
import { OsobaFizycznaForm } from 'src/app/shared/models/klient/osoba-fizyczna-form.model';
import { KlienciClient, KrajDto, KrajeClient, UtworzKlientaCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-klient',
  templateUrl: './klient.component.html',
  styleUrls: ['./klient.component.css']
})
export class KlientComponent implements OnInit {
  formBuilder: IFormBuilder;
  kraje: SelectItem<KrajDto>[] = [];
  nowyKlientForm: IFormGroup<NowyKlientForm>;
  aktywnaZakladka = 0;

  constructor(
    formBuilder: FormBuilder,
    private krajeClient: KrajeClient,
    private klienciClient: KlienciClient,
    private messageToast: MessageToast,
    private router: Router,
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
    this.pobierzKraje();
  }

  zbudujFormularz(): void {
    this.nowyKlientForm = this.formBuilder.group<NowyKlientForm>({
      osobaFizyczna: this.formBuilder.group<OsobaFizycznaForm>({
        imie: [null],
        nazwisko: [null],
        pesel: [null],
        numerPaszportu: [null]
      }),
      firma: this.formBuilder.group<FirmaForm>({
        nazwa: [null],
        regon: [null],
        nip: [null]
      }),
      kraj: [null, Validators.required],
      numerTelefonu: [null],
      kodPocztowy: [null],
      ulica: [null],
      numerDomu: [null],
      numerLokalu: [null],
      miejscowosc: [null],
      email: [null]
    })
  }

  pobierzKraje(): void {
    this.krajeClient.pobierzKraje('nazwaKraju', 1)
      .pipe(map(kraje => kraje.map((k => ({ label: k.nazwaKraju, value: k } as SelectItem<KrajDto>)))))
      .subscribe(
        (kraje) => {
          this.kraje = kraje;
          const polska = this.kraje.find(x => x.value.skrot === 'PL').value;
          this.nowyKlientForm.controls.kraj.setValue(polska);
        }
      );
  }

  zapiszOnClick(): void {
    const command = {} as UtworzKlientaCommand;
    if (this.aktywnaZakladka === 0) {
      command.imie = this.nowyKlientForm.value.osobaFizyczna.imie;
      command.nazwisko = this.nowyKlientForm.value.osobaFizyczna.nazwisko;
      command.pesel = this.nowyKlientForm.value.osobaFizyczna.pesel;
      command.numerPaszportu = this.nowyKlientForm.value.osobaFizyczna.numerPaszportu
    } else {
      command.nazwa = this.nowyKlientForm.value.firma.nazwa;
      command.regon = this.nowyKlientForm.value.firma.regon;
      command.nip = this.nowyKlientForm.value.firma.nip;
    }
    command.idKraju = this.nowyKlientForm.value.kraj.id;
    command.numerTelefonu = this.nowyKlientForm.value.numerTelefonu;
    command.kodPocztowy = this.nowyKlientForm.value.kodPocztowy;
    command.ulica = this.nowyKlientForm.value.ulica;
    command.numerDomu = this.nowyKlientForm.value.numerDomu;
    command.numerLokalu = this.nowyKlientForm.value.numerLokalu;
    command.miejscowosc = this.nowyKlientForm.value.miejscowosc;
    command.email = this.nowyKlientForm.value.email;
    this.klienciClient.utworzKlienta(command)
      .subscribe(
        () => {
          this.messageToast.success('Utworzono klienta.');
          this.router.navigate(['/klienci']);
        },
        (e) => {
          //TODO: Dorobić powiadomienia
        }
      );
  }

  naZamknieciuZakladki(event: { index: number; }): void {
    this.aktywnaZakladka = event.index;
  }
}
