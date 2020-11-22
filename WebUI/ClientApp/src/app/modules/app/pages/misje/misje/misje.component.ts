/// <reference types="@types/googlemaps" />
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { SelectItem } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { finalize, map } from 'rxjs/operators';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';
import { UslugaForm } from 'src/app/shared/models/usluga/nowa-usluga-form.model';
import { StatusMisjiDto, StatusyMisjiClient, TypMisjiDto, TypyMisjiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-misje',
  templateUrl: './misje.component.html',
  styleUrls: ['./misje.component.css']
})
export class MisjeComponent implements OnInit {
  nowaMisjaForm: IFormGroup<UslugaForm>;
  formBuilder: IFormBuilder;
  typyMisji: SelectItem<TypMisjiDto>[] = [];
  statusMisji: StatusMisjiDto;
  opcjeMapy = {
    center: { lat: 52.2334836, lng: 21.0122257 },
    zoom: 12
  };
  nakladkiNaMape: google.maps.Circle[];
  pl = Kalendarz.pl;
  pracownicy: SelectItem<Pracownik>[];

  constructor(
    formBuilder: FormBuilder,
    private dynamicDialogRef: DynamicDialogRef,
    private typyMisjiClient: TypyMisjiClient,
    private statusyMisjiClient: StatusyMisjiClient,
    private dynamicDialogConfig: DynamicDialogConfig,
    private http: HttpClient
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
    this.pobierzTypyMisji();
    this.pobierzStatusMisji();
    this.pobierzPracownikow();
    this.typOnChange();
    this.promienOnChange();
  }

  typOnChange(): void {
    this.nowaMisjaForm.controls.typ.valueChanges.subscribe(
      typ => {
        if (!this.nowaMisjaForm.value.przypisanyPracownik)
          return;
        const uprawnienia = this.nowaMisjaForm.value.przypisanyPracownik.claims.filter(x => x.type === 'uprawnienia').map(x => x.value);
        this.pracownicy.forEach(pracownik => pracownik.disabled = false);
        if (typ?.nazwa === 'VLOS') {
          if (!uprawnienia.includes('VLOS')) {
            this.nowaMisjaForm.controls.przypisanyPracownik.setValue(null);
          }
          const pracownicyBezUprawnienia = this.pracownicy
            .filter(x => !x.value.claims.filter(x => x.type === 'uprawnienia').some(x => x.value === 'VLOS'));
          pracownicyBezUprawnienia.forEach(pracownik => pracownik.disabled = true)
        } else if (typ?.nazwa === 'BVLOS') {
          if (!uprawnienia.includes('BVLOS')) {
            this.nowaMisjaForm.controls.przypisanyPracownik.setValue(null);
          }
          const pracownicyBezUprawnienia = this.pracownicy
            .filter(x => !x.value.claims.filter(x => x.type === 'uprawnienia').some(x => x.value === 'BVLOS'));
          pracownicyBezUprawnienia.forEach(pracownik => pracownik.disabled = true)
        }
      });
  }

  pobierzPracownikow(): void {
    this.http.get('https://localhost:44318/account/uzytkownicy')
      .pipe(
        map((pracownicy: Pracownik[]) =>
          pracownicy.map(pracownik => ({
            label: pracownik.claims.find(x => x.type === 'name').value,
            value: pracownik
          } as SelectItem<Pracownik>))))
      .subscribe(
        (uzytkownicy: SelectItem<Pracownik>[]) => {
          console.log(uzytkownicy);

          this.pracownicy = uzytkownicy;
        }
      );
  }

  zbudujFormularz(): void {
    this.nowaMisjaForm = this.formBuilder.group<UslugaForm>({
      nazwa: [null, Validators.required],
      dataRozpoczecia: [null, Validators.required],
      dataZakonczenia: [null],
      opis: [null, Validators.required],
      typ: [null, Validators.required],
      status: [{ value: null, disabled: true }],
      statusId: [null],
      maksymalnaWysokoscLotu: [null, Validators.required],
      przypisanyPracownik: [null],
      szerokoscGeograficzna: [{ value: null, disabled: true }],
      dlugoscGeograficzna: [{ value: null, disabled: true }],
      promien: [200, Validators.required]
    });
  }

  pobierzTypyMisji(): void {
    this.typyMisjiClient.pobierzTypyMisji()
      .pipe(
        map(typyMisji => typyMisji.map(tm => ({ label: tm.nazwa, value: tm }) as SelectItem<TypMisjiDto>)),
        finalize(() => this.wczytajPolaDoFormularza())
      )
      .subscribe(
        typyMisji => this.typyMisji = typyMisji
      );
  }

  wczytajPolaDoFormularza(): void {
    const misja = this.dynamicDialogConfig.data;
    if (!misja)
      return;
    this.nakladkiNaMape = [
      new google.maps.Circle({
        center: {
          lat: misja.szerokoscGeograficzna,
          lng: misja.dlugoscGeograficzna
        },
        fillColor: '#c286d8',
        fillOpacity: 0.35,
        strokeWeight: 1,
        radius: +this.nowaMisjaForm.controls.promien.value
      })
    ];
    this.nowaMisjaForm.setValue(misja);
  }

  pobierzStatusMisji(): void {
    this.statusyMisjiClient.pobierzStatusMisji("Utworzona")
      .subscribe(
        statusMisji => {
          this.nowaMisjaForm.controls.status.setValue(statusMisji.nazwa);
          this.nowaMisjaForm.controls.statusId.setValue(statusMisji.id);
          this.statusMisji = statusMisji;
        });
  }

  promienOnChange(): void {
    this.nowaMisjaForm.controls.promien.valueChanges
      .subscribe(
        (promien: number) => this.nakladkiNaMape[0]?.setRadius(promien)
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
        radius: +this.nowaMisjaForm.controls.promien.value
      })
    ];
    this.nowaMisjaForm.controls.szerokoscGeograficzna.setValue(event.latLng.lat());
    this.nowaMisjaForm.controls.dlugoscGeograficzna.setValue(event.latLng.lng());
  }

  wyczyscMapeOnClick(): void {
    this.nakladkiNaMape = [];
    this.nowaMisjaForm.controls.szerokoscGeograficzna.reset();
    this.nowaMisjaForm.controls.dlugoscGeograficzna.reset();
  }

  dodajMisjeOnClick(): void {
    const misja = this.nowaMisjaForm.getRawValue();
    this.dynamicDialogRef.close(misja);
  }

  anulujOnClick(): void {
    this.dynamicDialogRef.destroy();
  }

  nazwaPrzycisku(): string {
    if (this.dynamicDialogConfig.data) {
      return 'Zaktualizuj misję'
    } else {
      return 'Dodaj misję';
    }
  }
}
