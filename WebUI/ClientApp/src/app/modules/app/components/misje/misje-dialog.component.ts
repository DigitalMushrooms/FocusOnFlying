import { Component } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { finalize } from 'rxjs/operators';
import { MisjaDto, PagedResultOfMisjaDto, UslugiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-misje-dialog',
  templateUrl: './misje-dialog.component.html',
  styleUrls: ['./misje-dialog.component.css']
})
export class MisjeDialogComponent {
  misje: MisjaDto[];
  liczbaRekordow = 0;
  loading = true;

  constructor(
    private uslugiClient: UslugiClient,
    private dynamicDialogConfig: DynamicDialogConfig
  ) { }

  pobierzMisje(event: LazyLoadEvent): void {
    this.loading = true;
    const idUslugi: string = this.dynamicDialogConfig.data.idUslugi;
    this.uslugiClient.pobierzMisjeUslugi(idUslugi, `${event.sortField} ${event.sortOrder}`, event.first, event.rows)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (misje: PagedResultOfMisjaDto) => {
          this.misje = misje.results;
        }
      );
  }

  naWybraniuMisji(event: { data: MisjaDto }): void {

  }
}
