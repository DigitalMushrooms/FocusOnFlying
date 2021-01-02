import { Component } from '@angular/core';
import { PracownicyService } from 'src/app/core/services/pracownicy.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private pracownicyService: PracownicyService) {}

  wyloguj(): void {
    this.pracownicyService.wyloguj();
  }
}
