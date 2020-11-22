import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthorizationGuard implements CanActivate {
    constructor(private oidcSecurityService: OidcSecurityService) { }

    canActivate(): Observable<boolean> {
        return this.oidcSecurityService.checkAuth()
            .pipe(
                map((isAuthorized: boolean) => {
                    if (!isAuthorized) {
                        this.oidcSecurityService.authorize();
                        return false;
                    }
                    return true;
                })
            );
    }
}