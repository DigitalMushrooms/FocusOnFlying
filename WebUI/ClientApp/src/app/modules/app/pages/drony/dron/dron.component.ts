import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { SelectItem } from 'primeng/api';
import { map } from 'rxjs/operators';
import { NowyDronForm } from 'src/app/shared/models/dron/nowy-dron-form.model';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { TypDronaDto, TypyDronaClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-dron',
  templateUrl: './dron.component.html',
  styleUrls: ['./dron.component.css']
})
export class DronComponent implements OnInit {
  nowyDronForm: IFormGroup<NowyDronForm>;
  formBuilder: IFormBuilder;
  typyDrona: SelectItem<TypDronaDto>[];
  pl = Kalendarz.pl;
  minimalnaData = new Date()

  constructor(
    formBuilder: FormBuilder,
    private typyDronaClient: TypyDronaClient
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
    this.pobierzTypyDrona();
  }

  zbudujFormularz(): void {
    this.nowyDronForm = this.formBuilder.group<NowyDronForm>({
      nazwaProducenta: [null, Validators.required],
      nazwaModelu: [null, Validators.required],
      numerSeryjny: [null, Validators.required],
      typ: [null, Validators.required],
      dataNastepnegoPrzegladu: [null, Validators.required]
    });
  }

  pobierzTypyDrona(): void {
    this.typyDronaClient.pobierzTypyDrona()
      .pipe(map(typyDrona => typyDrona.map(typDrona => ({ label: typDrona.nazwa, value: typDrona } as SelectItem<TypDronaDto>))))
      .subscribe(
        (typyDrona: SelectItem<TypDronaDto>[]) => {
          this.typyDrona = typyDrona;
        }
      );
  }
}
