import { Component, OnInit } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { finalize } from 'rxjs/operators';
import { KlienciClient, KlientDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-klienci',
  templateUrl: './klienci.component.html',
  styleUrls: ['./klienci.component.css']
})
export class KlienciComponent implements OnInit {
  klienci: KlientDto[];
  liczbaRekordow = 0;
  loading = true;

  constructor(
    private dynamicDialogRef: DynamicDialogRef,
    private klienciClient: KlienciClient
  ) { }

  ngOnInit(): void {
  }

  pobierzKlientow(event: LazyLoadEvent): void {
    this.klienciClient.pobierzKlientow(event.first, event.rows, event.sortField, event.sortOrder)
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
