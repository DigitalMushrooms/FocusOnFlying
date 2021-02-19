using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Audyty.Queries.PobierzAudyty
{
    public class AudytDto : IMapFrom<Audyt>
    {
        public Guid Id { get; set; }
        public Guid IdAudytowanegoWiersza { get; set; }
        public string NazwaTabeli { get; set; }
        public string Dane { get; set; }
        public long DataAudytu { get; set; }
        public string Uzytkownik { get; set; }
        public string TypOperacji { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Audyt, AudytDto>()
                .ForMember(d => d.DataAudytu, o => o.MapFrom(s => s.DataAudytu.ToUnixTime()));
        }
    }
}
