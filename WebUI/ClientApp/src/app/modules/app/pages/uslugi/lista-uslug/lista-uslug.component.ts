import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UslugaDto, UslugiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-lista-uslug',
  templateUrl: './lista-uslug.component.html',
  styleUrls: ['./lista-uslug.component.css']
})
export class ListaUslugComponent implements OnInit {
  uslugi: UslugaDto[] = [];

  constructor(private uslugiClient: UslugiClient, private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('https://localhost:44318/account/uzytkownicy', {
      observe: "response",
      headers: new HttpHeaders({
          "Accept": "application/json"
      })
  })
      .subscribe(
        (uzytkownicy) => { console.log(uzytkownicy); },
        (error) => { console.log(error); }
      );
    
    this.uslugiClient.pobierzUslugi()
      .subscribe(
        (uslugi: UslugaDto[]) => {
          this.uslugi = uslugi;
        }
      );
  }
}
