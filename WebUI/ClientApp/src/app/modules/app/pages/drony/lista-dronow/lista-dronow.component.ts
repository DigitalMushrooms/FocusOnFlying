import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { LazyLoadEvent } from 'primeng/api';
import { finalize } from 'rxjs/operators';
import { DronDto, DronyClient, PagedResultOfDronDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-lista-dronow',
  templateUrl: './lista-dronow.component.html',
  styleUrls: ['./lista-dronow.component.css']
})
export class ListaDronowComponent implements OnInit {
  drony: DronDto[];
  liczbaRekordow = 0;
  loading = true;

  constructor(
    private dronyClient: DronyClient
  ) { }

  ngOnInit(): void {
  }

  pobierzDrony(event: LazyLoadEvent): void {
    this.loading = true;
    this.dronyClient.pobierzDrony(event.first, event.rows, event.sortField, event.sortOrder)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (drony: PagedResultOfDronDto) => {
          this.drony = drony.results;
          this.liczbaRekordow = drony.rowCount;
        }
      );
  }

  naWybraniuDrona(dron: DronDto): void {

  }

  przekroczonyDzienPrzegladu(dron: DronDto): boolean {
    return moment(dron.dataNastepnegoPrzegladu).isBefore(moment());
  }

}
