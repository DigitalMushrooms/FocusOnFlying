import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { SelectItem } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { DronForm } from 'src/app/shared/models/dron/dron-form.model';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { DronyClient, TypDronaDto, TypyDronaClient, UtworzDronaCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-dron',
  templateUrl: './dron.component.html',
  styleUrls: ['./dron.component.css']
})
export class DronComponent implements OnInit {
  nowyDronForm: IFormGroup<DronForm>;
  formBuilder: IFormBuilder;
  typyDrona: SelectItem<TypDronaDto>[];
  pl = Kalendarz.pl;
  minimalnaData = new Date();

  constructor(
    formBuilder: FormBuilder,
    private typyDronaClient: TypyDronaClient,
    private dronyClient: DronyClient,
    private messageToast: MessageToast,
    private router: Router,
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
    const typDrona$ = this.pobierzTypyDrona();
    typDrona$.subscribe(
      (typyDrona: SelectItem<TypDronaDto>[]) => {
        this.typyDrona = typyDrona;
      }
    );
  }

  zbudujFormularz(): void {
    this.nowyDronForm = this.formBuilder.group<DronForm>({
      producent: [null, Validators.required],
      model: [null, Validators.required],
      numerSeryjny: [null, Validators.required],
      typ: [null, Validators.required],
      dataNastepnegoPrzegladu: [null, Validators.required]
    });
  }

  pobierzTypyDrona(): Observable<SelectItem<TypDronaDto>[]> {
    return this.typyDronaClient.pobierzTypyDrona()
      .pipe(map(typyDrona => typyDrona.map(typDrona => ({ label: typDrona.nazwa, value: typDrona } as SelectItem<TypDronaDto>))));
  }

  zapiszDronaOnClick(): void {
    const formValue = this.nowyDronForm.value;
    const command = {
      producent: formValue.producent,
      model: formValue.model,
      numerSeryjny: formValue.numerSeryjny,
      idTypuDrona: formValue.typ.id,
      dataNastepnegoPrzegladu: formValue.dataNastepnegoPrzegladu.getTime()
    } as UtworzDronaCommand;
    this.dronyClient.utworzDrona(command)
      .subscribe(
        () => {
          this.messageToast.success('Utworzono drona.');
          this.router.navigate(['/strona-glowna']);
        }
      );
  }
}
