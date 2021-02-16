import { KrajDto } from "src/app/web-api-client";
import { FirmaForm } from "./firma-form.model";
import { OsobaFizycznaForm } from "./osoba-fizyczna-form.model";

export interface NowyKlientForm {
    id: string,
    osobaFizyczna: OsobaFizycznaForm,
    firma: FirmaForm,
    kraj: KrajDto,
    numerTelefonu: string,
    kodPocztowy: string,
    ulica: string,
    numerDomu: string,
    numerLokalu: string,
    miejscowosc: string,
    email: string
}