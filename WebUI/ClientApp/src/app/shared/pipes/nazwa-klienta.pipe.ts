import { Pipe, PipeTransform } from '@angular/core';
import { KlientDto } from 'src/app/web-api-client';

@Pipe({ name: 'nazwaKlienta' })
export class NazwaKlientaPipe implements PipeTransform {
    transform(klient: KlientDto): string {
        if (klient.pesel) {
            return `${klient.imie} ${klient.nazwisko}`;
        } else {
            return klient.nazwa;
        }
    }
}