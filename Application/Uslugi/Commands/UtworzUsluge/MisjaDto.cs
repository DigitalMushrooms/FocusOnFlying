using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.TypyMisji.Queries.PobierzTypyMisji;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge
{
    public class MisjaDto : IMapFrom<Misja>
    {
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public TypMisjiDto TypMisji { get; set; }
        public int MaksymalnaWysokoscLotu { get; set; }
        public Guid IdStatusuMisji { get; set; }
        public long DataRozpoczecia { get; set; }
        public long DataZakonczenia { get; set; }
        public string IdPracownika { get; set; }

        public decimal SzerokoscGeograficzna { get; set; }
        public decimal DlugoscGeograficzna { get; set; }
        public int Promien { get; set; }
        public List<MisjaDronDto> MisjeDrony { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Misja, MisjaDto>()
                .ForMember(dest => dest.DataRozpoczecia, opt => opt.MapFrom(src => src.DataRozpoczecia.ToUnixTime()))
                .ForMember(dest => dest.DataZakonczenia, opt => opt.MapFrom(src => src.DataZakonczenia.ToUnixTime()))
                .ReverseMap()
                .ForPath(dest => dest.DataRozpoczecia, opt => opt.MapFrom(src => src.DataRozpoczecia.ToLocalDateTime()))
                .ForPath(dest => dest.DataZakonczenia, opt => opt.MapFrom(src => src.DataZakonczenia.ToLocalDateTime()));
        }
    }
}
