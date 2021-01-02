import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { MessageToast } from '../services/message-toast.service';
import { PracownicyService } from '../services/pracownicy.service';

@Injectable({ providedIn: 'root' })
export class AuthorizationGuard implements CanActivate {
    constructor(
        private oidcSecurityService: OidcSecurityService,
        private pracownicyService: PracownicyService,
        private messageToast: MessageToast
    ) { }

    canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
        return this.oidcSecurityService.checkAuth()
            .pipe(
                map((isAuthorized: boolean) => {
                    if (!isAuthorized) {
                        this.oidcSecurityService.authorize();
                    }
                    if (!route.data.rola) {
                        return true;
                    }
                    if (this.pracownicyService.role.includes(route.data.rola)) {
                        return true;
                    } else {
                        this.messageToast.error('Nie masz uprawnień, aby podejrzeć tę stronę.');
                        return false;
                    }
                })
            );
    }
}