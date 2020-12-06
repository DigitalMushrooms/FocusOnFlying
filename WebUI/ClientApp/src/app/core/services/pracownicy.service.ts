import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';

@Injectable({
    providedIn: 'root'
})
export class PracownicyService {
    constructor(private http: HttpClient) { }

    pobierzPracownikow(): Observable<Pracownik[]> {
        return this.http.get<Pracownik[]>('https://localhost:44318/account/uzytkownicy');
    }
}
