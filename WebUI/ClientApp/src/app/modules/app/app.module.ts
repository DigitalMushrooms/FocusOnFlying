import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MenuModule } from 'primeng/menu';
import { HttpTokenInterceptor } from 'src/app/core/interceptors/http.token.interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './pages/app/app.component';
import { HomeComponent } from './pages/home/home.component';
import { NowyKlientComponent } from './pages/klienci/nowy-klient/nowy-klient.component';
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
    NowaUslugaComponent,
    NowyKlientComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    ButtonModule,
    CardModule,
    DropdownModule,
    FormsModule,
    FormsModule,
    HttpClientModule,
    InputNumberModule,
    InputTextModule,
    MenuModule,
    ReactiveFormsModule,
    AuthModule.forRoot(),
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
