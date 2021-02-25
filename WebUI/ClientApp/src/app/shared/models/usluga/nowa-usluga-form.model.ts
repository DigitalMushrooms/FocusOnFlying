import { KlientDto } from 'src/app/web-api-client';

export interface NowaUslugaForm {
    dataPrzyjeciaZalecenia: Date;
    klient: KlientDto;
}