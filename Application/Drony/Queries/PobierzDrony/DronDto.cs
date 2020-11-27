using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.TypyDrona.PobierzTypyDrona;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Drony.Queries.PobierzDrony
{
    public class DronDto : IMapFrom<Dron>
    {
        public Guid Id { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public string NumerSeryjny { get; set; }
        public TypDronaDto TypDrona { get; set; }
        public long DataNastepnegoPrzegladu { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dron, DronDto>()
                .ForMember(dest => dest.DataNastepnegoPrzegladu, opt => opt.MapFrom(src => src.DataNastepnegoPrzegladu.ToUnixTime()));
        }
    }
}
