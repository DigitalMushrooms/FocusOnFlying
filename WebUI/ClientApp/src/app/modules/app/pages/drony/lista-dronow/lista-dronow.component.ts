import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { LazyLoadEvent, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Table } from 'primeng/table';
import { finalize } from 'rxjs/operators';
import { DronDto, DronyClient, PagedResultOfDronDto } from 'src/app/web-api-client';
import { DronDialogComponent } from '../../../components/dron/dron-dialog.component';

@Component({
  selector: 'app-lista-dronow',
  templateUrl: './lista-dronow.component.html',
  styleUrls: ['./lista-dronow.component.css'],
  providers: [DialogService]
})
export class ListaDronowComponent {
  drony: DronDto[];
  liczbaRekordow = 0;
  loading = true;
  wybranyDron: DronDto;
  kontekstoweMenu: MenuItem[];

  constructor(
    private dronyClient: DronyClient,
    private dialogService: DialogService,
  ) { }

  pobierzDrony(event: LazyLoadEvent): void {
    this.loading = true;
    this.dronyClient.pobierzDrony(event.first, event.rows, `${event.sortField} ${event.sortOrder}`)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (drony: PagedResultOfDronDto) => {
          this.drony = drony.results;
          this.liczbaRekordow = drony.rowCount;
        }
      );
  }

  naWybraniuDrona(tableRef: Table): void {
    this.kontekstoweMenu = [
      { label: 'Edytuj datę następnego przeglądu', icon: 'pi pi-fw pi-calendar-plus', command: () => this.edytujDateNastepnegoPrzegladu(tableRef), visible: this.wybierzMisjeVisible() },
    ];
  }

  edytujDateNastepnegoPrzegladu(tableRef: Table): void {
    const dialog = this.dialogService.open(DronDialogComponent, {
      header: 'Edycja daty następnego przeglądu',
      width: '50%',
      data: { id: this.wybranyDron.id }
    });

    dialog.onClose.subscribe(
      () => {
        const event = tableRef.createLazyLoadMetadata() as LazyLoadEvent;
        event.first = 0;
        this.pobierzDrony(event);
      }
    );
  }

  wybierzMisjeVisible(): boolean {
    return true;
  }

  przekroczonyDzienPrzegladu(dron: DronDto): boolean {
    return moment(dron.dataNastepnegoPrzegladu).isBefore(moment());
  }
}
