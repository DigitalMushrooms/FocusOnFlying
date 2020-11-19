import { TypMisjiDto } from "src/app/web-api-client";
import { Pracownik } from '../misje/pracownik.model';

export interface UslugaForm {
    nazwa: string;
    dataRozpoczecia: Date;
    dataZakonczenia: Date,
    opis: string;
    typ: TypMisjiDto;
    status: string,
    statusId: string,
    maksymalnaWysokoscLotu: number;
    przypisanyPracownik: Pracownik,
    szerokoscGeograficzna: number,
    dlugoscGeograficzna: number,
    promien: number
}