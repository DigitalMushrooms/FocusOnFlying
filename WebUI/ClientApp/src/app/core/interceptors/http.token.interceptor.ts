import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';


@Injectable()
export class HttpTokenInterceptor {

    /**
     * Creates an instance of TokenInterceptor.
     * @param {OidcSecurityService} auth
     * @memberof TokenInterceptor
     */
    constructor(public oidcSecurityService: OidcSecurityService) { }

    /**
     * Intercept all HTTP request to add JWT token to Headers
     * @param {HttpRequest<any>} request
     * @param {HttpHandler} next
     * @returns {Observable<HttpEvent<any>>}
     * @memberof TokenInterceptor
     */
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${this.oidcSecurityService.getToken()}`
            }
        });

        return next.handle(request);
    }
}