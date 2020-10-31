import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-misje',
  templateUrl: './misje.component.html',
  styleUrls: ['./misje.component.css']
})
export class MisjeComponent implements OnInit {
  nowaMisjaForm = this.formBuilder.group({
    nazwa: [null],
    opis: [null],
    typ: [null]
  });

  constructor(
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
  }

}
