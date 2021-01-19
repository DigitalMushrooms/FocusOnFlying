import { TypDronaDto } from 'src/app/web-api-client';

export interface DronForm {
    producent: string;
    model: string;
    numerSeryjny: string;
    typ: TypDronaDto;
    dataNastepnegoPrzegladu: Date;
}