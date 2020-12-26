import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IFormBuilder, IFormGroup } from '@rxweb/types';
import * as moment from 'moment';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { ListaRaportowForm } from 'src/app/shared/models/raport/lista-raportow-form.model';
import { KlientDto, MisjaDronDto, MisjaDto, MisjeClient, PagedResultOfMisjaDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-lista-raportow',
  templateUrl: './lista-raportow.component.html',
  styleUrls: ['./lista-raportow.component.css']
})
export class ListaRaportowComponent implements OnInit {
  listaRaportowForm: IFormGroup<ListaRaportowForm>;
  formBuilder: IFormBuilder;
  pl = Kalendarz.pl;
  misje: MisjaDto[];
  loading = false;
  liczbaRekordow = 0;

  constructor(
    formBuilder: FormBuilder,
    private misjeClient: MisjeClient) {
    this.formBuilder = formBuilder;
  }

  ngOnInit(): void {
    this.zbudujFormularz();
  }

  zbudujFormularz(): void {
    this.listaRaportowForm = this.formBuilder.group<ListaRaportowForm>({
      miesiac: [new Date(), Validators.required]
    })
  }

  wygenerujRaportOnClick(tableRef: Table): void {
    const event = tableRef.createLazyLoadMetadata();
    this.pobierzUslugi(event);
  }

  pobierzUslugi(event: LazyLoadEvent): void {
    this.loading = true;

    const miesiac = this.listaRaportowForm.value.miesiac;
    const poczatekMiesiaca = moment(miesiac).startOf('month').valueOf();
    const koniecMiesiaca = moment(miesiac).endOf('month').valueOf();

    this.misjeClient.pobierzMisje(poczatekMiesiaca, koniecMiesiaca, 0, 0, `${event.sortField} ${event.sortOrder}`)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (misje: PagedResultOfMisjaDto) => {
          this.misje = misje.results;
          this.liczbaRekordow = misje.rowCount;
        }
      );
  }

  adres(klient: KlientDto): string {
    return `${klient.ulica} ${klient.numerDomu}${klient.numerLokalu ? '/' + klient.numerLokalu : ''}, ${klient.kodPocztowy} ${klient.miejscowosc} ${klient.kraj.nazwaKraju}`;
  }

  drony(misjeDrony: MisjaDronDto[]): string {
    return misjeDrony.map(x => `${x.dron.producent} ${x.dron.model} ${x.dron.numerSeryjny}`).join(', ');
  }
}
