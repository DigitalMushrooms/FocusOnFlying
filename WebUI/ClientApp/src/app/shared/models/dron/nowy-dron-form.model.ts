import { TypDronaDto } from 'src/app/web-api-client';

export interface NowyDronForm {
    nazwaProducenta: string;
    nazwaModelu: string;
    numerSeryjny: string;
    typ: TypDronaDto;
    dataNastepnegoPrzegladu: Date;
}