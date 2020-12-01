import { DronDto, TypMisjiDto } from "src/app/web-api-client";
import { Pracownik } from './pracownik.model';

export interface NowaMisjaForm {
    nazwa: string;
    dataRozpoczecia: Date;
    dataZakonczenia: Date,
    opis: string;
    typ: TypMisjiDto;
    status: string,
    statusId: string,
    maksymalnaWysokoscLotu: number;
    przypisanyPracownik: Pracownik,
    drony: DronDto[],
    szerokoscGeograficzna: number,
    dlugoscGeograficzna: number,
    promien: number
}