import { Component } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { finalize } from 'rxjs/operators';
import { KlienciClient, KlientDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-klienci-dialog',
  templateUrl: './klienci-dialog.component.html',
  styleUrls: ['./klienci-dialog.component.css']
})
export class KlienciDialogComponent {
  klienci: KlientDto[];
  liczbaRekordow = 0;
  loading = true;

  constructor(
    private dynamicDialogRef: DynamicDialogRef,
    private klienciClient: KlienciClient
  ) { }

  pobierzKlientow(event: LazyLoadEvent): void {
    this.klienciClient.pobierzKlientow(null, null, null, null, `${event.sortField} ${event.sortOrder}`, event.first, event.rows)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        klienci => {
          this.klienci = klienci.results;
          this.liczbaRekordow = klienci.rowCount;
        }
      );
  }

  naWybraniuKlienta(event: { data: KlientDto }): void {
    this.dynamicDialogRef.close(event.data);
  }
}
