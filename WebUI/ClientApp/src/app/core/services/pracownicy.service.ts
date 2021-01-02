import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';

@Injectable({
    providedIn: 'root'
})
export class PracownicyService {
    private userData: any;

    constructor(private http: HttpClient, private oidcSecurityService: OidcSecurityService) {
        oidcSecurityService.userData$.subscribe(userData => this.userData = userData);
    }

    get role(): string[] {
        return this.userData.role;
    }

    get sub(): string {
        return this.userData.sub;
    }

    pobierzPracownikow(): Observable<Pracownik[]> {
        return this.http.get<Pracownik[]>('https://localhost:44318/account/uzytkownicy');
    }

    wyloguj(): void {
        this.oidcSecurityService.logoffAndRevokeTokens().subscribe();
    }
}
