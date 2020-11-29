import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { ListaKlientowForm } from 'src/app/shared/models/klient/lista-klientow.model';
import { KlienciClient, KlientDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-klienci',
  templateUrl: './klienci.component.html',
  styleUrls: ['./klienci.component.css']
})
export class KlienciComponent implements OnInit {
  listaKlientowForm: IFormGroup<ListaKlientowForm>;
  formBuilder: IFormBuilder;
  klienci: KlientDto[];
  liczbaRekordow = 0;
  loading = false;

  constructor(
    formBuilder: FormBuilder,
    private klienciClient: KlienciClient
  ) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
  }

  zbudujFormularz(): void {
    this.listaKlientowForm = this.formBuilder.group<ListaKlientowForm>({
      fraza: [null],
      pesel: [null],
      nip: [null],
      regon: [null]
    });
  }

  pobierzKlientow(event: LazyLoadEvent): void {
    this.loading = true;
    const formValue = this.listaKlientowForm.value;
    this.klienciClient.pobierzKlientow(
      formValue.fraza, formValue.pesel, formValue.nip, formValue.regon,
      `${event.sortField} ${event.sortOrder}`, event.first, event.rows
    )
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        klienci => {
          this.klienci = klienci.results;
          this.liczbaRekordow = klienci.rowCount;
        }
      );
  }

  naWybraniuKlienta(event): void {

  }

  wyszukajOnClick(tableRef: Table): void {
    const event = tableRef.createLazyLoadMetadata() as LazyLoadEvent;
    event.first = 0;
    this.pobierzKlientow(event);
  }
}
