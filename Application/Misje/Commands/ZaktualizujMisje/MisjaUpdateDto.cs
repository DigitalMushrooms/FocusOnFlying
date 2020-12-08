﻿using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using NSwag.Annotations;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje
{
    public class MisjaUpdateDto : IMapFrom<Misja>
    {
        [OpenApiIgnore]
        public Guid Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public Guid IdTypuMisji { get; set; }
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
            profile.CreateMap<Misja, MisjaUpdateDto>()
                .ForMember(dest => dest.DataRozpoczecia, opt => opt.MapFrom(src => src.DataRozpoczecia.ToUnixTime()))
                .ForMember(dest => dest.DataZakonczenia, opt => opt.MapFrom(src => src.DataZakonczenia.ToUnixTime()))
                .ReverseMap()
                .ForPath(dest => dest.DataRozpoczecia, opt => opt.MapFrom(src => src.DataRozpoczecia.ToLocalDateTime()))
                .ForPath(dest => dest.DataZakonczenia, opt => opt.MapFrom(src => src.DataZakonczenia.ToLocalDateTime()));
        }
    }
}
