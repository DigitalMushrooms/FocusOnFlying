import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SelectItem } from 'primeng/api';
import { map } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { KlienciClient, KrajeClient, UtworzKlientaCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-klient',
  templateUrl: './klient.component.html',
  styleUrls: ['./klient.component.css']
})
export class KlientComponent implements OnInit {
  kraje: SelectItem[] = [];
  nowyKlientForm = this.formBuilder.group({
    imie: [null],
    nazwisko: [null],
    nazwa: [null],
    kraj: [null, Validators.required],
    pesel: [null],
    regon: [null],
    nip: [null],
    numerPaszportu: [null],
    numerTelefonu: [null],
    kodPocztowy: [null],
    ulica: [null],
    numerDomu: [null],
    numerLokalu: [null],
    miejscowosc: [null],
    email: [null]
  });
  controls = this.nowyKlientForm.controls;

  constructor(
    private formBuilder: FormBuilder,
    private krajeClient: KrajeClient,
    private klienciClient: KlienciClient,
    private messageToast: MessageToast,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.pobierzKraje();
  }

  pobierzKraje(): void {
    this.krajeClient.pobierzKraje('nazwaKraju', 1)
      .pipe(map(kraje => kraje.map((k => ({ label: k.nazwaKraju, value: { id: k.id, skrot: k.skrot } } as SelectItem)))))
      .subscribe(
        (kraje) => {
          this.kraje = kraje;
          const polska = this.kraje.find(x => x.value.skrot === 'PL').value;
          this.controls['kraj'].setValue(polska);
        }
      );
  }

  zapiszOnClick(): void {
    const command = {
      imie: this.controls['imie'].value,
      nazwisko: this.controls['nazwisko'].value,
      nazwa: this.controls['nazwa'].value,
      idKraju: this.controls['kraj'].value ? (this.controls['kraj'].value.id) : null,
      pesel: this.controls['pesel'].value ? this.controls['pesel'].value : null,
      regon: this.controls['regon'].value ? this.controls['regon'].value : null,
      nip: this.controls['nip'].value ? this.controls['nip'].value : null,
      numerPaszportu: this.controls['numerPaszportu'].value ? this.controls['numerPaszportu'].value : null,
      numerTelefonu: this.controls['numerTelefonu'].value ? this.controls['numerTelefonu'].value : null,
      kodPocztowy: this.controls['kodPocztowy'].value ? this.controls['kodPocztowy'].value : null,
      ulica: this.controls['ulica'].value ? this.controls['ulica'].value : null,
      numerDomu: this.controls['numerDomu'].value ? this.controls['numerDomu'].value : null,
      numerLokalu: this.controls['numerLokalu'].value ? this.controls['numerLokalu'].value : null,
      miejscowosc: this.controls['miejscowosc'].value ? this.controls['miejscowosc'].value : null,
      email: this.controls['email'].value ? this.controls['email'].value : null
    } as UtworzKlientaCommand;
    this.klienciClient.utworzKlienta(command).subscribe(
      () => {
        this.messageToast.success('Utworzono klienta.');
        this.router.navigate(['/klienci']);
      },
      (e) => {
        //TODO: Dorobić powiadomienia
      }
    );
  }
}
