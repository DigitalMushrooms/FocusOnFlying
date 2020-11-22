import { Component } from '@angular/core';
import * as moment from 'moment';
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
        { label: 'Strona główna', icon: 'pi pi-fw pi-home', routerLink: '/strona-glowna' }
      ]
    },
    {
      label: 'Klienci',
      items: [
        { label: 'Nowy klient', icon: 'pi pi-fw pi-user-plus', routerLink: '/klienci/nowy-klient' },
      ]
    },
    {
      label: 'Usługi',
      items: [
        { label: 'Nowa usługa', icon: 'pi pi-fw pi-plus', routerLink: '/uslugi/usluga' },
        { label: 'Lista usług', icon: 'pi pi-fw pi-list', routerLink: '/uslugi/lista-uslug' },
      ]
    },
    {
      label: 'Drony',
      items: [
        { label: 'Nowy dron', icon: 'pi pi-fw pi-cloud', routerLink: '/drony/dron' }
      ]
    }
  ];

  ngOnInit(): void {
    moment.locale('pl');
  }
}
