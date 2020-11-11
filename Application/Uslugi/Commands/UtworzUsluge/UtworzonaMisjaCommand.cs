using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge
{
    public class UtworzonaMisjaCommand : IMapFrom<Misja>
    {
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public Guid IdTypuMisji { get; set; }
        public int MaksymalnaWysokoscLotu { get; set; }
        public Guid IdStatusuMisji { get; set; }
        public long DataRozpoczecia { get; set; }
        public long DataZakonczenia { get; set; }

        public decimal SzerokoscGeograficzna { get; set; }
        public decimal DlugoscGeograficzna { get; set; }
        public int Promien { get; set; }
    }
}
