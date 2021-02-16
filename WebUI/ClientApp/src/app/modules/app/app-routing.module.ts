import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizationGuard } from 'src/app/core/guards/authorization.guard';
import { DronComponent } from './pages/drony/dron/dron.component';
import { ListaDronowComponent } from './pages/drony/lista-dronow/lista-dronow.component';
import { HomeComponent } from './pages/home/home.component';
import { KlienciComponent } from './pages/klienci/klienci/klienci.component';
import { KlientComponent } from './pages/klienci/klient/klient.component';
import { ListaRaportowComponent } from './pages/raporty/lista-raportow/lista-raportow.component';
import { ListaUslugComponent } from './pages/uslugi/lista-uslug/lista-uslug.component';
import { UslugaComponent } from './pages/uslugi/usluga/usluga.component';
import { ListaUzytkownikowComponent } from './pages/uzytkownicy/lista-uzytkownikow/lista-uzytkownikow.component';

const routes: Routes = [
  { path: 'strona-glowna', component: HomeComponent, canActivate: [AuthorizationGuard] },
  { path: 'klienci/klient/:id', component: KlientComponent, canActivate: [AuthorizationGuard], data: { rola: 'KLIENT_TWORZENIE_EDYCJA' } },
  { path: 'klienci', component: KlienciComponent, canActivate: [AuthorizationGuard], data: { rola: 'KLIENT_PODGLAD' } },
  { path: 'uslugi/usluga', component: UslugaComponent, canActivate: [AuthorizationGuard], data: { rola: 'USLUGA_TWORZENIE_EDYCJA' } },
  { path: 'uslugi/lista-uslug', component: ListaUslugComponent, canActivate: [AuthorizationGuard], data: { rola: 'USLUGA_PODGLAD' } },
  { path: 'drony/dron', component: DronComponent, canActivate: [AuthorizationGuard], data: { rola: 'DRON_TWORZENIE_EDYCJA' } },
  { path: 'drony/lista-dronow', component: ListaDronowComponent, canActivate: [AuthorizationGuard], data: { rola: 'DRON_PODGLAD' } },
  { path: 'uzytkownicy', component: ListaUzytkownikowComponent, canActivate: [AuthorizationGuard] },
  { path: 'raporty/lista-raportow', component: ListaRaportowComponent, canActivate: [AuthorizationGuard], data: { rola: 'RAPORT_TWORZENIE_EDYCJA' } },
  { path: '**', redirectTo: 'strona-glowna' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
