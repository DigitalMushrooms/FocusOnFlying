import { Component, OnInit } from '@angular/core';
import { KlienciClient, KlientDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-klienci',
  templateUrl: './klienci.component.html',
  styleUrls: ['./klienci.component.css']
})
export class KlienciComponent implements OnInit {
  klienci: KlientDto[];

  constructor(
    private klienciClient: KlienciClient
  ) { }

  ngOnInit(): void {
    this.klienciClient.pobierzKlientow()
      .subscribe(
        (klienci) => {
          this.klienci = klienci;
        }
      );
  }

}
