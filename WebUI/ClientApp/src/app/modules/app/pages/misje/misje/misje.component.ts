/// <reference types="@types/googlemaps" />
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SelectItem } from 'primeng/api';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { map } from 'rxjs/operators';
import { StatusMisjiDto, StatusyMisjiClient, TypMisjiDto, TypyMisjiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-misje',
  templateUrl: './misje.component.html',
  styleUrls: ['./misje.component.css']
})
export class MisjeComponent implements OnInit {
  nowaMisjaForm = this.formBuilder.group({
    nazwa: [null, Validators.required],
    dataRozpoczecia: [null, Validators.required],
    dataZakonczenia: [null],
    opis: [null, Validators.required],
    typ: [null, Validators.required],
    status: [{ value: null, disabled: true }],
    maksymalnaWysokoscLotu: [null, Validators.required],
    szerokoscGeograficzna: [{ value: null, disabled: true }],
    dlugoscGeograficzna: [{ value: null, disabled: true }],
    promien: [200, Validators.required]
  });
  controls = this.nowaMisjaForm.controls;
  typyMisji: SelectItem<TypMisjiDto>[] = [];
  statusMisji: StatusMisjiDto;
  opcjeMapy = {
    center: { lat: 52.2334836, lng: 21.0122257 },
    zoom: 12
  };
  nakladkiNaMape: google.maps.Circle[];

  constructor(
    private formBuilder: FormBuilder,
    private dynamicDialogRef: DynamicDialogRef,
    private typyMisjiClient: TypyMisjiClient,
    private statusyMisjiClient: StatusyMisjiClient
  ) { }

  ngOnInit(): void {
    this.pobierzTypyMisji();
    this.pobierzStatusMisji();
    this.promienOnChange();
  }

  pobierzTypyMisji(): void {
    this.typyMisjiClient.pobierzTypyMisji()
      .pipe(map(typyMisji => typyMisji.map(tm => ({ label: tm.nazwa, value: tm }) as SelectItem<TypMisjiDto>)))
      .subscribe(
        typyMisji => this.typyMisji = typyMisji
      );
  }

  pobierzStatusMisji(): void {
    this.statusyMisjiClient.pobierzStatusMisji("Zaplanowana")
      .subscribe(
        statusMisji => {
          this.controls['status'].setValue(statusMisji.nazwa);
          this.statusMisji = statusMisji;
        });
  }

  promienOnChange(): void {
    this.controls['promien'].valueChanges
      .subscribe(
        (value: string) => this.nakladkiNaMape[0].setRadius(+value)
      );
  }

  mapaOnClick(event: { latLng: { lat: () => number; lng: () => number; } }): void {
    this.nakladkiNaMape = [
      new google.maps.Circle({
        center: {
          lat: event.latLng.lat(),
          lng: event.latLng.lng()
        },
        fillColor: '#c286d8',
        fillOpacity: 0.35,
        strokeWeight: 1,
        radius: +this.controls['promien'].value
      })
    ];
    this.controls['szerokoscGeograficzna'].setValue(event.latLng.lat());
    this.controls['dlugoscGeograficzna'].setValue(event.latLng.lng());
  }

  wyczyscMapeOnClick(): void {
    this.nakladkiNaMape = [];
    this.controls['szerokoscGeograficzna'].reset();
    this.controls['dlugoscGeograficzna'].reset();
  }

  dodajMisjeOnClick(): void {
    const abc = this.nowaMisjaForm;
    debugger;
    this.dynamicDialogRef.close(null);
  }

  anulujOnClick(): void {
    this.dynamicDialogRef.destroy();
  }
}
