using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzFaktureUslugi
{
    public class FakturaDto : IMapFrom<Faktura>
    {
        public Guid Id { get; set; }
        public string NumerFaktury { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscBrutto { get; set; }
        public bool ZaplaconaFaktura { get; set; }
    }
}
