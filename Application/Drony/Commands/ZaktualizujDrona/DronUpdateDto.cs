using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;

namespace FocusOnFlying.Application.Drony.Commands.ZaktualizujDrona
{
    public class DronUpdateDto : IMapFrom<Dron>
    {
        public long DataNastepnegoPrzegladu { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dron, DronUpdateDto>()
                .ForMember(d => d.DataNastepnegoPrzegladu, o => o.MapFrom(s => s.DataNastepnegoPrzegladu.ToUnixTime()))
                .ReverseMap()
                .ForPath(s => s.DataNastepnegoPrzegladu, o => o.MapFrom(d => d.DataNastepnegoPrzegladu.ToLocalDateTime()));
        }
    }
}
