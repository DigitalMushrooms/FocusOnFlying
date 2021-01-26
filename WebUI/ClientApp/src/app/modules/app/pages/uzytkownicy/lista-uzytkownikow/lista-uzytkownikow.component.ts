import { Component, OnInit } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { finalize } from 'rxjs/operators';
import { PracownicyService } from 'src/app/core/services/pracownicy.service';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';

@Component({
  selector: 'app-lista-uzytkownikow',
  templateUrl: './lista-uzytkownikow.component.html',
  styleUrls: ['./lista-uzytkownikow.component.css']
})
export class ListaUzytkownikowComponent implements OnInit {
  pracownicy: Pracownik[];
  loading = true;
  
  constructor(private pracownicyService: PracownicyService) { }

  ngOnInit(): void {
    
  }

  pobierzPracownikow(event: LazyLoadEvent): void {
    this.loading = true;
    this.pracownicyService.pobierzPracownikow()
    .pipe(finalize(() => this.loading = false))
    .subscribe(
      (uzytkownik: Pracownik[]) => {
        this.pracownicy = uzytkownik.slice(event.first, event.first + event.rows);
      }
    );
  }

  pelnaNazwa(uzytkownik: Pracownik): string | [] {
    return uzytkownik.claims.find(x => x.type === "name").value;
  }

  uprawnienia(uzytkownik: Pracownik): string {
    return uzytkownik.claims.filter(x => x.type === "uprawnienia").map(x => x.value).join(', ');
  }

  role(uzytkownik: Pracownik): string {
    return uzytkownik.claims.filter(x => x.type === "role").map(x => x.value).join(', ');
  }

  email(uzytkownik: Pracownik): string | [] {
    return uzytkownik.claims.find(x => x.type === "email").value;
  }
}
