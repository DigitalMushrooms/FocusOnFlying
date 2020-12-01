using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge
{
    public class UtworzUslugeCommand : IRequest, IMapFrom<Usluga>
    {
        public long DataPrzyjeciaZlecenia { get; set; }
        public Guid IdKlienta { get; set; }
        public Guid IdStatusuUslugi { get; set; }
        public List<MisjaDto> Misje { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UtworzUslugeCommand, Usluga>()
                .ForMember(dest => dest.DataPrzyjeciaZlecenia, opt => opt.MapFrom(src => src.DataPrzyjeciaZlecenia.ToLocalDateTime()));
        }
    }

    public class UtworzUslugeCommandHandler : IRequestHandler<UtworzUslugeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public UtworzUslugeCommandHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UtworzUslugeCommand request, CancellationToken cancellationToken)
        {
            Usluga uslugaEntity = _mapper.Map<Usluga>(request);
            _focusOnFlyingContext.Uslugi.Add(uslugaEntity);
            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
