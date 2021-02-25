import { KlientDto, StatusUslugiDto } from "src/app/web-api-client";

export interface ListaUslugForm {
    dataPrzyjeciaZleceniaOd: Date;
    dataPrzyjeciaZleceniaDo: Date;
    klient: KlientDto;
    statusyUslugi: StatusUslugiDto
}