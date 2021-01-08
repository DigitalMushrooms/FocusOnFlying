import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { FakturaForm } from 'src/app/shared/models/faktura/faktura-form.model';
import { FakturyClient, Operation, UslugaDto, UslugiClient, UtworzFaktureUslugiCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-faktura-dialog',
  templateUrl: './faktura-dialog.component.html',
  styleUrls: ['./faktura-dialog.component.css']
})
export class FakturaDialogComponent implements OnInit {
  formBuilder: IFormBuilder;
  fakturaForm: IFormGroup<FakturaForm>;
  usluga: UslugaDto;

  constructor(
    formBuilder: FormBuilder,
    private dynamicDialogRef: DynamicDialogRef,
    private uslugiClient: UslugiClient,
    private dynamicDialogConfig: DynamicDialogConfig,
    private messageToast: MessageToast,
    private fakturyClient: FakturyClient
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.usluga = this.dynamicDialogConfig.data.usluga;
    this.zbudujFormularz();
  }

  zbudujFormularz(): void {
    this.fakturaForm = this.formBuilder.group<FakturaForm>({
      numerFaktury: [null, Validators.required],
      wartoscNetto: [null, Validators.required],
      wartoscBrutto: [null, Validators.required],
      zaplaconaFaktura: [false]
    });

    if (this.usluga.faktura) {
      this.fakturaForm.setValue({
        numerFaktury: this.usluga.faktura.numerFaktury,
        wartoscNetto: this.usluga.faktura.wartoscNetto,
        wartoscBrutto: this.usluga.faktura.wartoscBrutto,
        zaplaconaFaktura: this.usluga.faktura.zaplaconaFaktura
      });
    }
  }

  zapiszFaktureOnClick(): void {
    if (this.usluga.faktura) {
      if (this.fakturaForm.pristine) {
        this.messageToast.warning('Nie zmieniono żadnego pola.');
        return;
      }

      const operacje: Operation[] = [];
      const controls = this.fakturaForm.controls;
      const value = this.fakturaForm.value;

      if (controls.numerFaktury.dirty)
        operacje.push({ op: 'add', path: `/numerFaktury`, value: value.numerFaktury } as Operation);
      if (controls.wartoscNetto.dirty)
        operacje.push({ op: 'add', path: `/wartoscNetto`, value: value.wartoscNetto } as Operation);
      if (controls.wartoscBrutto.dirty)
        operacje.push({ op: 'add', path: `/wartoscBrutto`, value: value.wartoscBrutto } as Operation);
      if (controls.zaplaconaFaktura.dirty)
        operacje.push({ op: 'add', path: `/zaplaconaFaktura`, value: value.zaplaconaFaktura } as Operation);

      this.fakturyClient.zaktualizujFakture(this.usluga.faktura.id, operacje)
        .subscribe(
          () => {
            this.messageToast.success('Zaktualizowano fakturę.');
            this.dynamicDialogRef.close();
          }
        );
    } else {
      const command = {
        numerFaktury: this.fakturaForm.value.numerFaktury,
        wartoscNetto: this.fakturaForm.value.wartoscNetto,
        wartoscBrutto: this.fakturaForm.value.wartoscBrutto,
        zaplaconaFaktura: this.fakturaForm.value.zaplaconaFaktura
      } as UtworzFaktureUslugiCommand;
      this.uslugiClient.utworzFaktureUslugi(this.usluga.id, command)
        .subscribe(
          () => {
            this.messageToast.success('Dodano fakturę.');
            this.dynamicDialogRef.close();
          }
        );
    }
  }

  anulujOnClick(): void {
    this.dynamicDialogRef.destroy();
  }
}
