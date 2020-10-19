/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.8.2.0 (NJsonSchema v10.2.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export interface IKlienciClient {
    pobierzKlientow(): Observable<KlienciDto[]>;
}

@Injectable({
    providedIn: 'root'
})
export class KlienciClient implements IKlienciClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    pobierzKlientow(): Observable<KlienciDto[]> {
        let url_ = this.baseUrl + "/api/klienci";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPobierzKlientow(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPobierzKlientow(<any>response_);
                } catch (e) {
                    return <Observable<KlienciDto[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<KlienciDto[]>><any>_observableThrow(response_);
        }));
    }

    protected processPobierzKlientow(response: HttpResponseBase): Observable<KlienciDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(KlienciDto.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<KlienciDto[]>(<any>null);
    }
}

export interface IKrajeClient {
    pobierzKraje(): Observable<KrajDto[]>;
}

@Injectable({
    providedIn: 'root'
})
export class KrajeClient implements IKrajeClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    pobierzKraje(): Observable<KrajDto[]> {
        let url_ = this.baseUrl + "/api/kraje";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPobierzKraje(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPobierzKraje(<any>response_);
                } catch (e) {
                    return <Observable<KrajDto[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<KrajDto[]>><any>_observableThrow(response_);
        }));
    }

    protected processPobierzKraje(response: HttpResponseBase): Observable<KrajDto[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(KrajDto.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<KrajDto[]>(<any>null);
    }
}

export class KlienciDto implements IKlienciDto {
    id?: string;
    imie?: string | undefined;
    nazwisko?: string | undefined;
    pesel?: string | undefined;
    regon?: string | undefined;
    nip?: string | undefined;
    numerPaszportu?: string | undefined;
    numerTelefonu?: string | undefined;
    kodPocztowy?: string | undefined;
    miejscowosc?: string | undefined;
    gmina?: string | undefined;
    dzielnica?: string | undefined;
    ulica?: string | undefined;
    numerDomu?: string | undefined;
    numerLokalu?: string | undefined;
    symbolPanstwa?: string | undefined;
    zagranicznyKodPocztowy?: string | undefined;
    email?: string | undefined;

    constructor(data?: IKlienciDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.imie = _data["imie"];
            this.nazwisko = _data["nazwisko"];
            this.pesel = _data["pesel"];
            this.regon = _data["regon"];
            this.nip = _data["nip"];
            this.numerPaszportu = _data["numerPaszportu"];
            this.numerTelefonu = _data["numerTelefonu"];
            this.kodPocztowy = _data["kodPocztowy"];
            this.miejscowosc = _data["miejscowosc"];
            this.gmina = _data["gmina"];
            this.dzielnica = _data["dzielnica"];
            this.ulica = _data["ulica"];
            this.numerDomu = _data["numerDomu"];
            this.numerLokalu = _data["numerLokalu"];
            this.symbolPanstwa = _data["symbolPanstwa"];
            this.zagranicznyKodPocztowy = _data["zagranicznyKodPocztowy"];
            this.email = _data["email"];
        }
    }

    static fromJS(data: any): KlienciDto {
        data = typeof data === 'object' ? data : {};
        let result = new KlienciDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["imie"] = this.imie;
        data["nazwisko"] = this.nazwisko;
        data["pesel"] = this.pesel;
        data["regon"] = this.regon;
        data["nip"] = this.nip;
        data["numerPaszportu"] = this.numerPaszportu;
        data["numerTelefonu"] = this.numerTelefonu;
        data["kodPocztowy"] = this.kodPocztowy;
        data["miejscowosc"] = this.miejscowosc;
        data["gmina"] = this.gmina;
        data["dzielnica"] = this.dzielnica;
        data["ulica"] = this.ulica;
        data["numerDomu"] = this.numerDomu;
        data["numerLokalu"] = this.numerLokalu;
        data["symbolPanstwa"] = this.symbolPanstwa;
        data["zagranicznyKodPocztowy"] = this.zagranicznyKodPocztowy;
        data["email"] = this.email;
        return data; 
    }
}

export interface IKlienciDto {
    id?: string;
    imie?: string | undefined;
    nazwisko?: string | undefined;
    pesel?: string | undefined;
    regon?: string | undefined;
    nip?: string | undefined;
    numerPaszportu?: string | undefined;
    numerTelefonu?: string | undefined;
    kodPocztowy?: string | undefined;
    miejscowosc?: string | undefined;
    gmina?: string | undefined;
    dzielnica?: string | undefined;
    ulica?: string | undefined;
    numerDomu?: string | undefined;
    numerLokalu?: string | undefined;
    symbolPanstwa?: string | undefined;
    zagranicznyKodPocztowy?: string | undefined;
    email?: string | undefined;
}

export class KrajDto implements IKrajDto {
    id?: string;
    nazwaKraju?: string | undefined;
    skrot?: string | undefined;

    constructor(data?: IKrajDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.nazwaKraju = _data["nazwaKraju"];
            this.skrot = _data["skrot"];
        }
    }

    static fromJS(data: any): KrajDto {
        data = typeof data === 'object' ? data : {};
        let result = new KrajDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["nazwaKraju"] = this.nazwaKraju;
        data["skrot"] = this.skrot;
        return data; 
    }
}

export interface IKrajDto {
    id?: string;
    nazwaKraju?: string | undefined;
    skrot?: string | undefined;
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}