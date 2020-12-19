import { Component } from '@angular/core';
import * as moment from 'moment';
import { LazyLoadEvent, MenuItem } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { finalize } from 'rxjs/operators';
import { MessageToast } from 'src/app/core/services/message-toast.service';
import { MisjaDto, MisjeClient, Operation, PagedResultOfMisjaDto, StatusMisjiDto, StatusyMisjiClient, UslugiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-misje-dialog',
  templateUrl: './misje-dialog.component.html',
  styleUrls: ['./misje-dialog.component.css']
})
export class MisjeDialogComponent {
  misje: MisjaDto[];
  liczbaRekordow = 0;
  loading = true;
  wybranaMisja: MisjaDto;
  kontekstoweMenu: MenuItem[];

  constructor(
    private uslugiClient: UslugiClient,
    private statusyMisjiClient: StatusyMisjiClient,
    private misjeClient: MisjeClient,
    private dynamicDialogConfig: DynamicDialogConfig,
    private dynamicDialogRef: DynamicDialogRef,
    private messageToast: MessageToast
  ) { }

  wybierzMisje(): void {
    this.dynamicDialogRef.close(this.wybranaMisja);
  }

  zmienStatusMisji(status: string): void {
    this.statusyMisjiClient.pobierzStatusMisji(status)
      .subscribe(
        (statusMisji: StatusMisjiDto) => {
          const operacja = [{ op: 'replace', path: `/idStatusuMisji`, value: statusMisji.id } as Operation];
          this.misjeClient.zaktualizujMisje(this.wybranaMisja.id, operacja)
            .subscribe(
              () => {
                this.messageToast.success('Zaktualizowano status misji');
                this.dynamicDialogRef.close();
              }
            );
        }
      );
  }

  zmienStatusMisjiNaWykonanaVisible(): boolean {
    if (this.wybranaMisja?.dataRozpoczecia && this.wybranaMisja.dataZakonczenia) {
      if (moment(this.wybranaMisja.dataZakonczenia).isBefore(moment())) {
        return true;
      }
    }
    return false;
  }

  pobierzMisje(event: LazyLoadEvent): void {
    this.loading = true;
    const idUslugi: string = this.dynamicDialogConfig.data.idUslugi;
    this.uslugiClient.pobierzMisjeUslugi(idUslugi, ['Utworzona', 'Zaplanowana', 'Wykonana'], `${event.sortField} ${event.sortOrder}`, event.first, event.rows)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (misje: PagedResultOfMisjaDto) => {
          this.misje = misje.results;
        }
      );
  }

  dodajMisje(): void {
    this.dynamicDialogRef.close(null);
  }

  naWybraniuUslugi(): void {
    this.kontekstoweMenu = [
      { label: 'Wybierz misje', icon: 'pi pi-fw pi-star-o', command: () => this.wybierzMisje() },
      { label: 'Zmień status misji na "Wykonana"', icon: 'pi pi-fw pi-star', command: () => this.zmienStatusMisji('Wykonana'), visible: this.zmienStatusMisjiNaWykonanaVisible() }
    ];
  }
}
