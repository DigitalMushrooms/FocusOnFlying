import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizationGuard } from 'src/app/core/guards/authorization.guard';
import { DronComponent } from './pages/drony/dron/dron.component';
import { ListaDronowComponent } from './pages/drony/lista-dronow/lista-dronow.component';
import { HomeComponent } from './pages/home/home.component';
import { KlienciComponent } from './pages/klienci/klienci/klienci.component';
import { KlientComponent } from './pages/klienci/klient/klient.component';
import { ListaUslugComponent } from './pages/uslugi/lista-uslug/lista-uslug.component';
import { UslugaComponent } from './pages/uslugi/usluga/usluga.component';

const routes: Routes = [
  { path: 'strona-glowna', component: HomeComponent, canActivate: [AuthorizationGuard] },
  { path: 'klienci/klient', component: KlientComponent, canActivate: [AuthorizationGuard] },
  { path: 'klienci', component: KlienciComponent, canActivate: [AuthorizationGuard] },
  { path: 'uslugi/usluga', component: UslugaComponent, canActivate: [AuthorizationGuard] },
  { path: 'uslugi/lista-uslug', component: ListaUslugComponent, canActivate: [AuthorizationGuard] },
  { path: 'drony/dron', component: DronComponent, canActivate: [AuthorizationGuard] },
  { path: 'drony/lista-dronow', component: ListaDronowComponent, canActivate: [AuthorizationGuard] },
  { path: '**', redirectTo: 'strona-glowna' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
