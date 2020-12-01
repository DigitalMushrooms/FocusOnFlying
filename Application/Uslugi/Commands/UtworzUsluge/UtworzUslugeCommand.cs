using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Drony.Queries.PobierzDrony;
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
            List<Misja> misjeEntity = new List<Misja>();
            foreach (MisjaDto misja in request.Misje)
            {
                Misja misjaEntity = _mapper.Map<Misja>(misja);
                foreach (DronDto dron in misja.Drony)
                {
                    misjaEntity.MisjeDrony.Add(new MisjaDron { IdDrona = dron.Id });
                    misjeEntity.Add(misjaEntity);
                }
            }

            var uslugaEntity = new Usluga { 
                DataPrzyjeciaZlecenia = request.DataPrzyjeciaZlecenia.ToLocalDateTime(),
                IdKlienta = request.IdKlienta,
                IdStatusuUslugi = request.IdStatusuUslugi,
                Misje = misjeEntity,
            };

            _focusOnFlyingContext.Uslugi.Add(uslugaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
