using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Klienci.Queries.PobierzKlientow
{
    public class KlienciDto : IMapFrom<Klient>
    {
        public Guid Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string Regon { get; set; }
        public string Nip { get; set; }
        public string NumerPaszportu { get; set; }
        public string NumerTelefonu { get; set; }
        public string KodPocztowy { get; set; }
        public string Miejscowosc { get; set; }
        public string Gmina { get; set; }
        public string Dzielnica { get; set; }
        public string Ulica { get; set; }
        public string NumerDomu { get; set; }
        public string NumerLokalu { get; set; }
        public string SymbolPanstwa { get; set; }
        public string ZagranicznyKodPocztowy { get; set; }
        public string Email { get; set; }
    }
}
