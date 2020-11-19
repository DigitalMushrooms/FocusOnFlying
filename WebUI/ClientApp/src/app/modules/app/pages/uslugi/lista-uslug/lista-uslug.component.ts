import { Component, OnInit } from '@angular/core';
import { UslugaDto, UslugiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-lista-uslug',
  templateUrl: './lista-uslug.component.html',
  styleUrls: ['./lista-uslug.component.css']
})
export class ListaUslugComponent implements OnInit {
  uslugi: UslugaDto[] = [];

  constructor(private uslugiClient: UslugiClient) { }

  ngOnInit(): void {
    this.uslugiClient.pobierzUslugi()
      .subscribe(
        (uslugi: UslugaDto[]) => {
          this.uslugi = uslugi;
        }
      );
  }
}
