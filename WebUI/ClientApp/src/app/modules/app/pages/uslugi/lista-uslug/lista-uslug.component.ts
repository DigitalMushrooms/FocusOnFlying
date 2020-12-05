import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { UslugaDto, UslugiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-lista-uslug',
  templateUrl: './lista-uslug.component.html',
  styleUrls: ['./lista-uslug.component.css']
})
export class ListaUslugComponent implements OnInit {
  uslugi: UslugaDto[] = [];
  wybranaUsluga: UslugaDto;
  indeksWybranejUslugi: number;
  kontekstoweMenu: MenuItem[];

  constructor(private uslugiClient: UslugiClient) { }

  ngOnInit(): void {
    this.pobierzUslugi();
    this.utworzMenuKontekstowe();
  }

  pobierzUslugi(): void {
    this.uslugiClient.pobierzUslugi().subscribe(
      (uslugi: UslugaDto[]) => {
        this.uslugi = uslugi;
      }
    );
  }

  utworzMenuKontekstowe(): void {
    this.kontekstoweMenu = [
      { label: 'Edytuj misję', icon: 'pi pi-fw pi-star-o', command: () => this.edytujMisje() },
      { label: 'Usuń misję', icon: 'pi pi-fw pi-times', command: () => this.usunMisje() }
    ];
  }

  edytujMisje(): void {
    
  }

  usunMisje(): void {

  }

  naWybraniuUslugi(event: { index: number; }): void {
    this.indeksWybranejUslugi = event.index;
  }
}
