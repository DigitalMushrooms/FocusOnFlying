import { TypMisjiDto } from "src/app/web-api-client";

export interface NowaUslugaForm {
    nazwa: string;
    dataRozpoczecia: Date;
    dataZakonczenia: Date,
    opis: string;
    typ: TypMisjiDto;
    status: string,
    statusId: string,
    maksymalnaWysokoscLotu: number;
    szerokoscGeograficzna: number,
    dlugoscGeograficzna: number,
    promien: number
}