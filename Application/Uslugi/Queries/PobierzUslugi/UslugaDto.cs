using AutoMapper;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
using FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusUslugi;
using FocusOnFlying.Application.Uslugi.Commands.UtworzFaktureUslugi;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Application.Uslugi.Queries.PobierzUslugi
{
    public class UslugaDto : IMapFrom<Usluga>
    {
        public Guid Id { get; set; }
        public long DataPrzyjeciaZlecenia { get; set; }

        public KlientDto Klient { get; set; }
        public FakturaDto Faktura { get; set; }
        public StatusUslugiDto StatusUslugi { get; set; }
        public List<MisjaDto> Misje { get; set; } = new List<MisjaDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Usluga, UslugaDto>()
                .ForMember(dest => dest.DataPrzyjeciaZlecenia, opt => opt.MapFrom(src => src.DataPrzyjeciaZlecenia.ToUnixTime()));
            profile.CreateMap<UslugaDto, Usluga>()
                .ForMember(dest => dest.DataPrzyjeciaZlecenia, opt => opt.MapFrom(src => src.DataPrzyjeciaZlecenia.ToLocalDateTime()));
        }
    }
}
