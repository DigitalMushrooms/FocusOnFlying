using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Misja
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public Guid IdTypuMisji { get; set; }
        public int MaksymalnaWysokoscLotu { get; set; }
        public Guid IdStatusuMisji { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public DateTime DataZakonczenia { get; set; }

        public string NazwaLokalizacji { get; set; }
        public decimal SzerokoscGeograficzna { get; set; }
        public decimal DlugoscGeograficzna { get; set; }
        public int Promien { get; set; }

        public TypMisji TypMisji { get; set; }
        public StatusMisji StatusMisji { get; set; }
    }
}
