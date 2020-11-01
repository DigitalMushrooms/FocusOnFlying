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

        public TypMisji TypMisji { get; set; }
        public StatusMisji StatusMisji { get; set; }
    }
}
