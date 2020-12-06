using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Drony.Queries.PobierzDrony;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge
{
    public class MisjaDronDto : IMapFrom<MisjaDron>
    {
        public Guid IdMisji { get; set; }
        public Guid IdDrona { get; set; }
        public MisjaDto Misja { get; set; }
        public DronDto Dron { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MisjaDron, MisjaDronDto>().ReverseMap();
        }
    }
}
