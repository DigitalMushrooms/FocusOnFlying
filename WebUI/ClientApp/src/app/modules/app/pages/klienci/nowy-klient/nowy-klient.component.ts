import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SelectItem } from 'primeng/api';
import { map } from 'rxjs/operators';
import { KrajeClient } from 'src/app/web-api-client';

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
    private krajeClient: KrajeClient,
    private fb: FormBuilder
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
}
