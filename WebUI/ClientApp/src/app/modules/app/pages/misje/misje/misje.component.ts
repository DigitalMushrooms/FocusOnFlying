import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { SelectItem } from 'primeng/api';
import { map } from 'rxjs/operators';
import { StatusMisjiDto, StatusyMisjiClient, TypMisjiDto, TypyMisjiClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-misje',
  templateUrl: './misje.component.html',
  styleUrls: ['./misje.component.css']
})
export class MisjeComponent implements OnInit {
  nowaMisjaForm = this.formBuilder.group({
    nazwa: [null],
    opis: [null],
    typ: [null],
    status: [{ value: null, disabled: true }]
  });
  controls = this.nowaMisjaForm.controls;
  typyMisji: SelectItem<TypMisjiDto>[] = [];
  statusMisji: StatusMisjiDto;

  constructor(
    private formBuilder: FormBuilder,
    private typyMisjiClient: TypyMisjiClient,
    private statusyMisjiClient: StatusyMisjiClient
  ) { }

  ngOnInit(): void {
    this.pobierzTypyMisji();
    this.pobierzStatusMisji();
  }

  pobierzTypyMisji(): void {
    this.typyMisjiClient.pobierzTypyMisji()
      .pipe(map(typyMisji => typyMisji.map(tm => ({ label: tm.nazwa, value: tm }) as SelectItem<TypMisjiDto>)))
      .subscribe(
        typyMisji => this.typyMisji = typyMisji
      );
  }

  pobierzStatusMisji(): void {
    this.statusyMisjiClient.pobierzStatusMisji("Zaplanowana")
      .subscribe(
        statusMisji => {
          this.controls['status'].setValue(statusMisji.nazwa);
          this.statusMisji = statusMisji;
        });
  }
}
