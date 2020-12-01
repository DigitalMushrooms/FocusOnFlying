using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.TypyDrona.PobierzTypyDrona
{
    public class TypDronaDto : IMapFrom<TypDrona>
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TypDrona, TypDronaDto>().ReverseMap();
        }
    }
}
