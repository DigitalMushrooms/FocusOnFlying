import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizationGuard } from 'src/app/core/guards/authorization.guard';
import { DronComponent } from './pages/drony/dron/dron.component';
import { HomeComponent } from './pages/home/home.component';
import { NowyKlientComponent } from './pages/klienci/nowy-klient/nowy-klient.component';
import { ListaUslugComponent } from './pages/uslugi/lista-uslug/lista-uslug.component';
import { UslugaComponent } from './pages/uslugi/usluga/usluga.component';

const routes: Routes = [
  { path: 'strona-glowna', component: HomeComponent, canActivate: [AuthorizationGuard] },
  { path: 'klienci/nowy-klient', component: NowyKlientComponent, canActivate: [AuthorizationGuard] },
  { path: 'uslugi/usluga', component: UslugaComponent, canActivate: [AuthorizationGuard] },
  { path: 'uslugi/lista-uslug', component: ListaUslugComponent, canActivate: [AuthorizationGuard] },
  { path: 'drony/dron', component: DronComponent, canActivate: [AuthorizationGuard] },
  { path: '**', redirectTo: 'strona-glowna' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
