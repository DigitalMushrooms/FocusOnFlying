import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OAuthModule } from 'angular-oauth2-oidc';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { MenuModule } from 'primeng/menu';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './pages/app/app.component';
import { HomeComponent } from './pages/home/home.component';
import { NowaUslugaComponent } from './pages/uslugi/nowa-usluga/nowa-usluga.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NowaUslugaComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    ButtonModule,
    CardModule,
    HttpClientModule,
    MenuModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ['https://localhost:44389//api'],
        sendAccessToken: true
      }
    })
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
