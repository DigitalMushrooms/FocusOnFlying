using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using NSwag.Annotations;
using System;

namespace FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje
{
    public class MisjaUpdateDto : IMapFrom<MisjaDto>
    {
        [OpenApiIgnore]
        public Guid Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public Guid IdTypuMisji { get; set; }
        public int MaksymalnaWysokoscLotu { get; set; }
        public Guid IdStatusuMisji { get; set; }
        public long DataRozpoczecia { get; set; }
        public long DataZakonczenia { get; set; }
        public string IdPracownika { get; set; }
        public decimal SzerokoscGeograficzna { get; set; }
        public decimal DlugoscGeograficzna { get; set; }
        public int Promien { get; set; }
    }
}
