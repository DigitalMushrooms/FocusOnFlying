import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { ContextMenuModule } from 'primeng/contextmenu';
import { DropdownModule } from 'primeng/dropdown';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { FieldsetModule } from 'primeng/fieldset';
import { GMapModule } from 'primeng/gmap';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MenuModule } from 'primeng/menu';
import { MultiSelectModule } from 'primeng/multiselect';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RippleModule } from 'primeng/ripple';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { HttpTokenInterceptor } from 'src/app/core/interceptors/http.token.interceptor';
import { MomentPipe } from 'src/app/shared/pipes/moment.pipe';
import { NazwaKlientaPipe } from 'src/app/shared/pipes/nazwa-klienta.pipe';
import { AppRoutingModule } from './app-routing.module';
import { FakturaDialogComponent } from './components/faktura/faktura-dialog.component';
import { KlienciDialogComponent } from './components/klienci/klienci-dialog.component';
import { MisjeDialogComponent } from './components/misje/misje-dialog.component';
import { AppComponent } from './pages/app/app.component';
import { DronComponent } from './pages/drony/dron/dron.component';
import { ListaDronowComponent } from './pages/drony/lista-dronow/lista-dronow.component';
import { HomeComponent } from './pages/home/home.component';
import { KlienciComponent } from './pages/klienci/klienci/klienci.component';
import { KlientComponent } from './pages/klienci/klient/klient.component';
import { MisjeComponent } from './pages/misje/misje/misje.component';
import { ListaRaportowComponent } from './pages/raporty/lista-raportow/lista-raportow.component';
import { ListaUslugComponent } from './pages/uslugi/lista-uslug/lista-uslug.component';
import { UslugaComponent } from './pages/uslugi/usluga/usluga.component';

export function configureAuth(oidcConfigService: OidcConfigService) {
  return (): Promise<unknown> =>
    oidcConfigService.withConfig({
      stsServer: 'https://localhost:44318',
      responseType: 'code',
      redirectUrl: window.location.origin,
      postLogoutRedirectUri: window.location.origin,
      clientId: 'angular',
      scope: 'openid profile address role',
      logLevel: LogLevel.Debug,
    });
}

@NgModule({
  declarations: [
    AppComponent,
    DronComponent,
    FakturaDialogComponent,
    HomeComponent,
    KlienciComponent,
    KlienciDialogComponent,
    KlientComponent,
    ListaDronowComponent,
    ListaRaportowComponent,
    ListaUslugComponent,
    MisjeComponent,
    MisjeDialogComponent,
    MomentPipe,
    NazwaKlientaPipe,
    UslugaComponent,
  ],
  imports: [
    AppRoutingModule,
    AuthModule.forRoot(),
    BrowserAnimationsModule,
    BrowserModule,
    ButtonModule,
    CalendarModule,
    CardModule,
    CheckboxModule,
    CommonModule,
    ContextMenuModule,
    DropdownModule,
    DynamicDialogModule,
    FieldsetModule,
    FormsModule,
    FormsModule,
    GMapModule,
    HttpClientModule,
    InputNumberModule,
    InputTextareaModule,
    InputTextModule,
    MenuModule,
    MultiSelectModule,
    RadioButtonModule,
    ReactiveFormsModule,
    RippleModule,
    TableModule,
    ToastModule,
    ToggleButtonModule,
  ],
  providers: [
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService],
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpTokenInterceptor,
      multi: true
    },
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
