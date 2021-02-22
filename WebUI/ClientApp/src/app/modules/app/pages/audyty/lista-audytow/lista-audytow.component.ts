import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { PracownicyService } from 'src/app/core/services/pracownicy.service';
import { Pracownik } from 'src/app/shared/models/misje/pracownik.model';
import { AudytDto, AudytyClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-lista-audytow',
  templateUrl: './lista-audytow.component.html',
  styleUrls: ['./lista-audytow.component.css']
})
export class ListaAudytowComponent implements OnInit {
  audyty: AudytDto[];
  liczbaRekordow: number;
  loading = true;
  pracownicy: Pracownik[];

  constructor(
    private audytyClient: AudytyClient,
    private pracownicyService: PracownicyService
  ) { }

  ngOnInit(): void {
    this.pobierzPracownikow();
  }

  pobierzPracownikow(): void {
    this.pracownicyService.pobierzPracownikow()
      .subscribe(
        pracownicy => this.pracownicy = pracownicy,
        () => { return; },
        () => this.pobierzAudyty()
      );
  }

  pobierzAudyty(): void {
    this.audytyClient.pobierzAudyty(0, 10)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (audyty) => {
          this.audyty = audyty.results;
          this.liczbaRekordow = audyty.rowCount;
        }
      );
  }

  pobierzNazweUzytkownika(idUzytkownika: string): string {
    return this.pracownicy.find(x => x.subjectId === idUzytkownika).username;
  }

}
