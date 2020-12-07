import { DronDto, StatusMisjiDto, TypMisjiDto } from "src/app/web-api-client";
import { Pracownik } from './pracownik.model';

export interface MisjaForm {
    id: string;
    nazwa: string;
    dataRozpoczecia: Date;
    dataZakonczenia: Date,
    opis: string;
    typ: TypMisjiDto;
    status: StatusMisjiDto,
    maksymalnaWysokoscLotu: number;
    przypisanyPracownik: Pracownik,
    drony: DronDto[],
    szerokoscGeograficzna: number,
    dlugoscGeograficzna: number,
    promien: number
}