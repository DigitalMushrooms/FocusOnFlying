import { Component } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  items: MenuItem[] = [
    {
      label: 'Ogólne',
      items: [
        { label: 'Strona główna', icon: 'pi pi-home', routerLink: '/home' }
      ]
    },
    {
      label: 'Klienci',
      items: [
        { label: 'Nowy klient', icon: 'pi pi-user-plus', routerLink: '/klienci/nowy-klient' },
      ]
    },
    {
      label: 'Usługi',
      items: [
        { label: 'Nowa usługa', icon: 'pi pi-fw pi-plus', routerLink: '/uslugi/nowa-usluga' },
      ]
    }
];

  constructor(private oauthService: OAuthService) {
    this.configureSingesSignOn();
  }

  configureSingesSignOn(): void {
    const authCodeFlowConfig: AuthConfig = {
      // Url of the Identity Provider
      issuer: 'https://localhost:44318',

      // URL of the SPA to redirect the user to after login
      redirectUri: 'https://localhost:44389',

      // The SPA's id. The SPA is registerd with this id at the auth-server
      // clientId: 'server.code',
      clientId: 'angular',

      // Just needed if your auth server demands a secret. In general, this
      // is a sign that the auth server is not configured with SPAs in mind
      // and it might not enforce further best practices vital for security
      // such applications.
      // dummyClientSecret: 'secret',

      responseType: 'code',

      // set the scope for the permissions the client should request
      // The first four are defined by OIDC.
      // Important: Request offline_access to get a refresh token
      // The api scope is a usecase specific one
      scope: 'openid profile FocusOnFlyingAPI',

      showDebugInformation: true,
    };
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocumentAndLogin();
  }

  ngOnInit() {

  }
}
