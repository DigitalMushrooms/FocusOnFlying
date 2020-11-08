import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { GMapModule } from 'primeng/gmap';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MenuModule } from 'primeng/menu';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TableModule } from 'primeng/table';
import { HttpTokenInterceptor } from 'src/app/core/interceptors/http.token.interceptor';
import { MomentPipe } from 'src/app/shared/pipes/moment.pipe';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './pages/app/app.component';
import { HomeComponent } from './pages/home/home.component';
import { KlienciComponent } from './pages/klienci/klienci/klienci.component';
import { NowyKlientComponent } from './pages/klienci/nowy-klient/nowy-klient.component';
import { MisjeComponent } from './pages/misje/misje/misje.component';
import { NowaUslugaComponent } from './pages/uslugi/nowa-usluga/nowa-usluga.component';

export function configureAuth(oidcConfigService: OidcConfigService) {
  return (): Promise<unknown> =>
    oidcConfigService.withConfig({
      stsServer: 'https://localhost:44318',
      responseType: 'code',
      redirectUrl: window.location.origin,
      postLogoutRedirectUri: window.location.origin,
      clientId: 'angular',
      scope: 'openid profile address FocusOnFlyingAPI',
      logLevel: LogLevel.Debug,
    });
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    KlienciComponent,
    MisjeComponent,
    MomentPipe,
    NowaUslugaComponent,
    NowyKlientComponent
  ],
  imports: [
    AppRoutingModule,
    AuthModule.forRoot(),
    BrowserAnimationsModule,
    BrowserModule,
    ButtonModule,
    CalendarModule,
    CardModule,
    CommonModule,
    DropdownModule,
    DynamicDialogModule,
    FormsModule,
    FormsModule,
    GMapModule,
    HttpClientModule,
    InputNumberModule,
    InputTextareaModule,
    InputTextModule,
    MenuModule,
    RadioButtonModule,
    ReactiveFormsModule,
    TableModule,
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
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
