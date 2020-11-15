import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { NowyKlientComponent } from './pages/klienci/nowy-klient/nowy-klient.component';
import { ListaUslugComponent } from './pages/uslugi/lista-uslug/lista-uslug.component';
import { NowaUslugaComponent } from './pages/uslugi/nowa-usluga/nowa-usluga.component';

const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "klienci/nowy-klient", component: NowyKlientComponent },
  { path: "uslugi/nowa-usluga", component: NowaUslugaComponent },
  { path: "uslugi/lista-uslug", component: ListaUslugComponent },
  { path: "**", redirectTo: "home" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
