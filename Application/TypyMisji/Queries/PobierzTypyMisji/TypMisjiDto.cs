using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.TypyMisji.Queries.PobierzTypyMisji
{
    public class TypMisjiDto : IMapFrom<TypMisji>
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TypMisji, TypMisjiDto>().ReverseMap();
        }
    }
}
