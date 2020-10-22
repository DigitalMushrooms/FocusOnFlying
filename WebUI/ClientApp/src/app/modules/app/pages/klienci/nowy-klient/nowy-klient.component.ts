import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SelectItem } from 'primeng/api';
import { map } from 'rxjs/operators';
import { KlienciClient, KrajeClient, UtworzKlientaCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-nowy-klient',
  templateUrl: './nowy-klient.component.html',
  styleUrls: ['./nowy-klient.component.css']
})
export class NowyKlientComponent implements OnInit {
  kraje: SelectItem[] = [];
  nowyKlientForm = this.fb.group({
    imie: [null],
    nazwisko: [null],
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
    private fb: FormBuilder,
    private krajeClient: KrajeClient,
    private klienciClient: KlienciClient
  ) { }

  ngOnInit(): void {
    this.pobierzKraje();
  }

  pobierzKraje(): void {
    this.krajeClient.pobierzKraje()
      .pipe(map(kraje => kraje.map((k => ({ label: k.nazwaKraju, value: { id: k.id, skrot: k.skrot } } as SelectItem)))))
      .subscribe(
        (kraje) => this.kraje = kraje
      );
  }

  zapiszOnClick(): void {
    const command = {
      imie: this.controls['imie'].value,
      nazwisko: this.controls['nazwisko'].value,
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
      () => { });
  }
}
