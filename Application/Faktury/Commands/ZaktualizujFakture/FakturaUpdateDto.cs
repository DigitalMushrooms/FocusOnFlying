using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;

namespace FocusOnFlying.Application.Faktury.Commands.ZaktualizujFakture
{
    public class FakturaUpdateDto : IMapFrom<Faktura>
    {
        public string NumerFaktury { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscBrutto { get; set; }
        public bool ZaplaconaFaktura { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Faktura, FakturaUpdateDto>()
                .ReverseMap();
        }
    }
}
