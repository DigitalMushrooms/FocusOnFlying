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
        { label: 'Nowy klient', icon: 'pi pi-fw pi-user-plus', routerLink: ['/klienci/klient', 0] },
        { label: 'Lista klientów', icon: 'pi pi-fw pi-users', routerLink: '/klienci' },
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
        { label: 'Nowy dron', icon: 'pi pi-fw pi-cloud', routerLink: '/drony/dron' },
        { label: 'Lista dronów', icon: 'pi pi-fw pi-list', routerLink: '/drony/lista-dronow' },
      ]
    },
    {
      label: 'Użytkownicy',
      items: [
        { label: 'Lista użytkowników', icon: 'pi pi-fw pi-users', routerLink: '/uzytkownicy' }
      ]
    },
    {
      label: 'Raporty',
      items: [
        { label: 'Lista raportów', icon: 'pi pi-fw pi-list', routerLink: '/raporty/lista-raportow' },
      ]
    }
  ];

  ngOnInit(): void {
    moment.locale('pl');
  }

  activeMenu(event): void {
    let node;
    if (event.target.classList.contains("p-submenu-header") == true) {
      node = "submenu";
    } else if (event.target.tagName === "SPAN") {
      node = event.target.parentNode.parentNode;
    } else {
      node = event.target.parentNode;
    }
    if (node != "submenu") {
      const menuitem = document.getElementsByClassName("p-menuitem");
      for (let i = 0; i < menuitem.length; i++) {
        menuitem[i].classList.remove("active");
      }
      node.classList.add("active");
    }
  }
}
