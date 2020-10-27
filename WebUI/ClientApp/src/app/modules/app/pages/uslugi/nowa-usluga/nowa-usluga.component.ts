import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import * as moment from 'moment';
import { DialogService } from 'primeng/dynamicdialog';
import { Kalendarz } from 'src/app/shared/models/localization.model';
import { KlienciComponent } from '../../klienci/klienci/klienci.component';

@Component({
  selector: 'app-nowa-usluga',
  templateUrl: './nowa-usluga.component.html',
  styleUrls: ['./nowa-usluga.component.css'],
  providers: [DialogService]
})
export class NowaUslugaComponent implements OnInit {
  nowaUslugaForm = this.formBuilder.group({
    dataPrzyjeciaZalecenia: [null]
  });
  controls = this.nowaUslugaForm.controls;
  pl = Kalendarz.pl;

  constructor(
    private formBuilder: FormBuilder,
    public dialogService: DialogService
  ) { }

  ngOnInit(): void {
    const dzisiaj = moment().startOf('day').toDate();
    this.controls['dataPrzyjeciaZalecenia'].setValue(dzisiaj);
  }

  pokazKlientowOnClick(): void {
    this.dialogService.open(KlienciComponent, {
      header: 'Wybierz klienta',
      width: '70%'
    });
  }
}
