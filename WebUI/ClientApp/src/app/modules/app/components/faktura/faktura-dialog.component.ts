import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { FakturaForm } from 'src/app/shared/models/faktura/faktura-form.model';
import { UslugiClient, UtworzFaktureUslugiCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-faktura-dialog',
  templateUrl: './faktura-dialog.component.html',
  styleUrls: ['./faktura-dialog.component.css']
})
export class FakturaDialogComponent implements OnInit {
  formBuilder: IFormBuilder;
  fakturaForm: IFormGroup<FakturaForm>;
  abc = false;

  constructor(
    formBuilder: FormBuilder,
    private dynamicDialogRef: DynamicDialogRef,
    private uslugiClient: UslugiClient,
    private dynamicDialogConfig: DynamicDialogConfig,
    private messageToast: MessageToast,
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
  }

  zbudujFormularz(): void {
    this.fakturaForm = this.formBuilder.group<FakturaForm>({
      numerFaktury: [null, Validators.required],
      wartoscNetto: [null, Validators.required],
      wartoscBrutto: [null, Validators.required],
      zaplaconaFaktura: [false]
    });
  }

  zapiszFaktureOnClick(): void {
    const command = {
      numerFaktury: this.fakturaForm.value.numerFaktury,
      wartoscNetto: this.fakturaForm.value.wartoscNetto,
      wartoscBrutto: this.fakturaForm.value.wartoscBrutto,
      zaplaconaFaktura: this.fakturaForm.value.zaplaconaFaktura
    } as UtworzFaktureUslugiCommand;
    this.uslugiClient.utworzFaktureUslugi(this.dynamicDialogConfig.data.idUslugi, command)
      .subscribe(
        () => this.messageToast.success('Dodano fakturę.')
      );
  }

  anulujOnClick(): void {
    this.dynamicDialogRef.destroy();
  }
}
