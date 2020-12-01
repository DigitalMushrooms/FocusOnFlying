using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge
{
    public class MisjaDronDto : IMapFrom<MisjaDron>
    {
        public Guid IdMisji { get; set; }
        public Guid IdDrona { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MisjaDron, MisjaDronDto>().ReverseMap();
        }
    }
}
