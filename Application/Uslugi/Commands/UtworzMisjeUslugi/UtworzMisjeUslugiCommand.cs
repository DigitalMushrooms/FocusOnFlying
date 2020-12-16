using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzMisjeUslugi
{
    public class UtworzMisjeUslugiCommand : IRequest, IMapFrom<Misja>
    {
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Misja, UtworzMisjeUslugiCommand>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.DataRozpoczecia, o => o.MapFrom(s => s.DataRozpoczecia.ToUnixTime()))
                .ForMember(d => d.DataZakonczenia, o => o.MapFrom(s => s.DataZakonczenia.ToUnixTime()))
                .ReverseMap()
                .ForPath(s => s.Id, o => o.Ignore())
                .ForPath(s => s.DataRozpoczecia, o => o.MapFrom(d => d.DataRozpoczecia.ToLocalDateTime()))
                .ForPath(s => s.DataZakonczenia, o => o.MapFrom(d => d.DataZakonczenia.ToLocalDateTime()));
        }
    }

    public class UtworzMisjeUslugiCommandHandler : IRequestHandler<UtworzMisjeUslugiCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public UtworzMisjeUslugiCommandHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UtworzMisjeUslugiCommand request, CancellationToken cancellationToken)
        {
            Usluga uslugaEntity = await _focusOnFlyingContext.Uslugi
                .Include(x => x.Misje)
                .SingleAsync(x => x.Id == request.Id);

            var misjaEntity = _mapper.Map<Misja>(request);
            uslugaEntity.Misje.Add(misjaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
