import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-nowy-klient',
  templateUrl: './nowy-klient.component.html',
  styleUrls: ['./nowy-klient.component.css']
})
export class NowyKlientComponent implements OnInit {
  kraje: SelectItem[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
