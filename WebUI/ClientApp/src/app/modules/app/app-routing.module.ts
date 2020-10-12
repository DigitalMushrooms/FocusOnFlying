import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { NowaUslugaComponent } from './pages/uslugi/nowa-usluga/nowa-usluga.component';

const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "uslugi/nowa-usluga", component: NowaUslugaComponent },
  { path: "**", component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
