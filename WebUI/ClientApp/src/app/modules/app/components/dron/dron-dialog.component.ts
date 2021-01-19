import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { DronyClient, Operation } from 'src/app/web-api-client';

@Component({
  selector: 'app-dron-dialog',
  templateUrl: './dron-dialog.component.html',
  styleUrls: ['./dron-dialog.component.css']
})
export class DronDialogComponent implements OnInit {
  dataNastepnegoPrzegladuForm: IFormGroup<{ dataNastepnegoPrzegladu: Date }>;
  formBuilder: IFormBuilder;
  pl = Kalendarz.pl;
  minimalnaData = new Date();

  constructor(
    formBuilder: FormBuilder,
    private dynamicDialogConfig: DynamicDialogConfig,
    private dronyClient: DronyClient,
    private messageToast: MessageToast,
    private dynamicDialogRef: DynamicDialogRef
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
  }

  zbudujFormularz(): void {
    this.dataNastepnegoPrzegladuForm = this.formBuilder.group<{ dataNastepnegoPrzegladu: Date }>({
      dataNastepnegoPrzegladu: [null, Validators.required]
    });
  }

  zapiszDateOnClick(): void {
    const dataNastepnegoPrzegladu = this.dataNastepnegoPrzegladuForm.value.dataNastepnegoPrzegladu.getTime();
    const operacje: Operation[] = [{ op: 'add', path: `/dataNastepnegoPrzegladu`, value: dataNastepnegoPrzegladu } as Operation];
    this.dronyClient.zaktualizujDrona(this.dynamicDialogConfig.data.id, operacje)
      .subscribe(
        () => {
          this.messageToast.success('Zaktualizowano drona');
          this.dynamicDialogRef.close();
        }
      );
  }
}
