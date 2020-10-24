import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  items: MenuItem[] = [
    {
      label: 'Ogólne',
      items: [
        { label: 'Strona główna', icon: 'pi pi-home', routerLink: '/home' }
      ]
    },
    {
      label: 'Klienci',
      items: [
        { label: 'Nowy klient', icon: 'pi pi-user-plus', routerLink: '/klienci/nowy-klient' },
      ]
    },
    {
      label: 'Usługi',
      items: [
        { label: 'Nowa usługa', icon: 'pi pi-fw pi-plus', routerLink: '/uslugi/nowa-usluga' },
      ]
    }
];

  constructor() {
  }

  ngOnInit() {

  }
}
