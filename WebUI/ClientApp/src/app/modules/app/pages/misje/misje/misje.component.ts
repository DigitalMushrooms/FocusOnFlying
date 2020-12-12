/// <reference types="@types/googlemaps" />
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { isEmpty } from 'lodash-es';
import * as moment from 'moment';
import { SelectItem } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { PracownicyService } from 'src/app/core/services/pracownicy.service';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { MisjaForm } from 'src/app/shared/models/misje/misja-form.model';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';
import { DronDto, DronyClient, MisjaDronDto, MisjeClient, Operation, StatusMisjiDto, StatusyMisjiClient, TypMisjiDto, TypyMisjiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-misje',
  templateUrl: './misje.component.html',
  styleUrls: ['./misje.component.css']
})
export class MisjeComponent implements OnInit {
  formBuilder: IFormBuilder;
  misjaForm: IFormGroup<MisjaForm>;
  typyMisji: SelectItem<TypMisjiDto>[] = [];
  statusyMisji: SelectItem<StatusMisjiDto>[];
  opcjeMapy = {
    center: { lat: 52.2334836, lng: 21.0122257 },
    zoom: 12
  };
  nakladkiNaMape: google.maps.Circle[];
  pl = Kalendarz.pl;
  pracownicy: SelectItem<Pracownik>[];
  drony: SelectItem<DronDto>[];
  nazwaPrzycisku = 'Zapisz misję';

  constructor(
    formBuilder: FormBuilder,
    private dynamicDialogRef: DynamicDialogRef,
    private typyMisjiClient: TypyMisjiClient,
    private statusyMisjiClient: StatusyMisjiClient,
    private dynamicDialogConfig: DynamicDialogConfig,
    private dronyClient: DronyClient,
    private pracownicyService: PracownicyService,
    private messageToast: MessageToast,
    private misjeClient: MisjeClient
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.nadajNazwePrzyciskowi();
    this.zbudujFormularz();
    const typyMisji$ = this.pobierzTypyMisji();
    const statusyMisji$ = this.pobierzStatusyMisji();
    const pracownicy$ = this.pobierzPracownikow();
    const drony$ = this.pobierzDrony();
    this.typOnChange();
    this.promienOnChange();

    forkJoin([typyMisji$, statusyMisji$, pracownicy$, drony$])
      .subscribe({
        next: ([typyMisji, statusyMisji, pracownicy, drony]) => {
          this.typyMisji = typyMisji;

          const utworzona = statusyMisji.find(x => x.value.nazwa === 'Utworzona');
          this.misjaForm.controls.status.setValue(utworzona.value);
          this.statusyMisji = statusyMisji;

          this.pracownicy = pracownicy;

          this.drony = drony;
        },
        complete: () => this.wczytajPolaDoFormularza()
      });
  }

  zbudujFormularz(): void {
    this.misjaForm = this.formBuilder.group<MisjaForm>({
      id: [null],
      nazwa: [null, Validators.required],
      dataRozpoczecia: [null, Validators.required],
      dataZakonczenia: [null],
      opis: [null, Validators.required],
      typ: [null, Validators.required],
      status: [{ value: null, disabled: true }],
      maksymalnaWysokoscLotu: [null, Validators.required],
      przypisanyPracownik: [null],
      drony: [null],
      szerokoscGeograficzna: [{ value: null, disabled: true }],
      dlugoscGeograficzna: [{ value: null, disabled: true }],
      promien: [200, Validators.required]
    });
  }

  pobierzTypyMisji(): Observable<SelectItem<TypMisjiDto>[]> {
    return this.typyMisjiClient.pobierzTypyMisji()
      .pipe(map(typyMisji => typyMisji.map(tm => ({ label: tm.nazwa, value: tm }) as SelectItem<TypMisjiDto>)));
  }

  pobierzStatusyMisji(): Observable<SelectItem<StatusMisjiDto>[]> {
    return this.statusyMisjiClient.pobierzStatusyMisji()
      .pipe(map(statusyMisji => statusyMisji.map(s => ({ label: s.nazwa, value: s } as SelectItem<StatusMisjiDto>))));
  }

  pobierzPracownikow(): Observable<SelectItem<Pracownik>[]> {
    return this.pracownicyService.pobierzPracownikow()
      .pipe(map(pracownicy => pracownicy.map(pracownik => ({ label: pracownik.claims.find(x => x.type === 'name').value, value: pracownik } as SelectItem<Pracownik>))));
  }

  pobierzDrony(): Observable<SelectItem<DronDto>[]> {
    return this.dronyClient.pobierzDrony(0, 0, 'producent 1, model 1')
      .pipe(map(drony => drony.results.map(d =>
        ({ label: `${d.producent} ${d.model}, SN: ${d.numerSeryjny}`, value: d, disabled: moment(d.dataNastepnegoPrzegladu).isBefore(moment()) } as SelectItem<DronDto>))));
  }

  typOnChange(): void {
    this.misjaForm.controls.typ.valueChanges.subscribe(
      typ => {
        if (!this.misjaForm.value.przypisanyPracownik)
          return;
        const uprawnienia = this.misjaForm.value.przypisanyPracownik.claims.filter(x => x.type === 'uprawnienia').map(x => x.value);
        this.pracownicy.forEach(pracownik => pracownik.disabled = false);
        if (typ?.nazwa === 'VLOS') {
          if (!uprawnienia.includes('VLOS')) {
            this.misjaForm.controls.przypisanyPracownik.setValue(null);
          }
          const pracownicyBezUprawnienia = this.pracownicy
            .filter(x => !x.value.claims.filter(x => x.type === 'uprawnienia').some(x => x.value === 'VLOS'));
          pracownicyBezUprawnienia.forEach(pracownik => pracownik.disabled = true)
        } else if (typ?.nazwa === 'BVLOS') {
          if (!uprawnienia.includes('BVLOS')) {
            this.misjaForm.controls.przypisanyPracownik.setValue(null);
          }
          const pracownicyBezUprawnienia = this.pracownicy
            .filter(x => !x.value.claims.filter(x => x.type === 'uprawnienia').some(x => x.value === 'BVLOS'));
          pracownicyBezUprawnienia.forEach(pracownik => pracownik.disabled = true)
        }
      });
  }

  promienOnChange(): void {
    this.misjaForm.controls.promien.valueChanges
      .subscribe(
        (promien: number) => this.nakladkiNaMape[0]?.setRadius(promien)
      );
  }

  wczytajPolaDoFormularza(): void {
    const misja: MisjaForm = this.dynamicDialogConfig.data;
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
        radius: +this.misjaForm.controls.promien.value
      })
    ];
    this.misjaForm.setValue(misja);
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
        radius: +this.misjaForm.controls.promien.value
      })
    ];
    this.misjaForm.patchValue({
      szerokoscGeograficzna: event.latLng.lat(),
      dlugoscGeograficzna: event.latLng.lng()
    });
    this.misjaForm.controls.szerokoscGeograficzna.markAsDirty();
    this.misjaForm.controls.dlugoscGeograficzna.markAsDirty();
  }

  wyczyscMapeOnClick(): void {
    this.nakladkiNaMape = [];
    this.misjaForm.controls.szerokoscGeograficzna.reset();
    this.misjaForm.controls.dlugoscGeograficzna.reset();
  }

  zapiszMisjeOnClick(): void {
    const misja = this.misjaForm.getRawValue();

    if (this.istniejacaMisja()) {
      const operacje = this.pobierzZmienionePola();
      if (isEmpty(operacje)) {
        this.messageToast.warning('Nie zmieniono żadnego pola.');
      } else {
        this.misjeClient.zaktualizujMisje(misja.id, operacje).subscribe(
          () => {
            this.messageToast.success('Zaktualizowano misję.');
            this.dynamicDialogRef.close();
          },
          e => {
            const response = JSON.parse(e.response);

            if (response && response.errors.DataRozpoczecia) {
              this.messageToast.error(response.errors.DataRozpoczecia[0]);
            }
          }
        );
      }
    } else {
      this.dynamicDialogRef.close(misja);
    }
  }

  pobierzZmienionePola(): Operation[] {
    const operacje: Operation[] = [];

    const controls = this.misjaForm.controls;
    const value = this.misjaForm.value;

    if (controls.nazwa.dirty)
      operacje.push({ op: 'replace', path: `/nazwa`, value: value.nazwa } as Operation);
    if (controls.dataRozpoczecia.dirty)
      operacje.push({ op: 'replace', path: `/dataRozpoczecia`, value: value.dataRozpoczecia.getTime() } as Operation);
    if (controls.dataZakonczenia.dirty)
      operacje.push({ op: 'replace', path: `/dataZakonczenia`, value: value.dataZakonczenia.getTime() } as Operation);
    if (controls.opis.dirty)
      operacje.push({ op: 'replace', path: `/opis`, value: value.opis } as Operation);
    if (controls.typ.dirty)
      operacje.push({ op: 'replace', path: `/idTypuMisji`, value: value.typ.id } as Operation);
    if (controls.status.dirty)
      operacje.push({ op: 'replace', path: `/status`, value: value.status.id } as Operation);
    if (controls.maksymalnaWysokoscLotu.dirty)
      operacje.push({ op: 'replace', path: `/maksymalnaWysokoscLotu`, value: value.maksymalnaWysokoscLotu } as Operation);
    if (controls.przypisanyPracownik.dirty)
      operacje.push({ op: 'replace', path: `/idPracownika`, value: value.przypisanyPracownik.subjectId } as Operation);
    if (controls.drony.dirty)
      operacje.push({ op: 'replace', path: `/misjeDrony`, value: value.drony.map(d => ({ idMisji: value.id, idDrona: d.id } as MisjaDronDto)) } as Operation);
    if (controls.szerokoscGeograficzna.dirty)
      operacje.push({ op: 'replace', path: `/szerokoscGeograficzna`, value: this.misjaForm.getRawValue().szerokoscGeograficzna } as Operation);
    if (controls.dlugoscGeograficzna.dirty)
      operacje.push({ op: 'replace', path: `/dlugoscGeograficzna`, value: this.misjaForm.getRawValue().dlugoscGeograficzna } as Operation);
    if (controls.promien.dirty)
      operacje.push({ op: 'replace', path: `/promien`, value: value.promien } as Operation);
    return operacje;
  }

  anulujOnClick(): void {
    this.dynamicDialogRef.destroy();
  }

  nadajNazwePrzyciskowi(): void {
    if (this.istniejacaMisja()) {
      this.nazwaPrzycisku = 'Zaktualizuj misję';
    }
  }

  istniejacaMisja(): boolean {
    return !!this.dynamicDialogConfig.data;
  }
}
