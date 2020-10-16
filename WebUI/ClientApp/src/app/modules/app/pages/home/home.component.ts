import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { KlienciClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private oauthService: OAuthService, private klienciClient: KlienciClient) { }

  ngOnInit(): void {
  }

  callApi() {
    // const token = this.oauthService.getIdentityClaims();
    // const at = this.oauthService.getAccessToken();
    // debugger;
    this.klienciClient.pobierzKlientow()
      .subscribe(
        (klienci) => { debugger; },
        () => { debugger; }
      )
  }
}
