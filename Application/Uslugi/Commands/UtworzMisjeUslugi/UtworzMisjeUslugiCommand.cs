using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzMisjeUslugi
{
    public class UtworzMisjeUslugiCommand : IRequest, IMapFrom<Misja>
    {
        public Guid Id { get; set; }
        public MisjaDto Misja { get; set; }
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
                .Include(x => x.Misje).ThenInclude(x => x.StatusMisji)
                .Include(x => x.Misje).ThenInclude(x => x.TypMisji)
                .Include(x => x.Misje).ThenInclude(x => x.MisjeDrony).ThenInclude(x => x.Dron).ThenInclude(x => x.TypDrona)
                .SingleAsync(x => x.Id == request.Id);

            Misja misjaEntity = _mapper.Map<Misja>(request.Misja);
            await NadajStatusMisjom(misjaEntity);
            uslugaEntity.Misje.Add(misjaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task NadajStatusMisjom(Misja misjaEntity)
        {
            if (misjaEntity.DataRozpoczecia.HasValue && misjaEntity.DataZakonczenia.HasValue)
            {
                misjaEntity.IdStatusuMisji = (await _focusOnFlyingContext.StatusyMisji.SingleAsync(x => x.Nazwa == "Zaplanowana")).Id;
            }
            else
            {
                misjaEntity.IdStatusuMisji = (await _focusOnFlyingContext.StatusyMisji.SingleAsync(x => x.Nazwa == "Utworzona")).Id;
            }
        }
    }
}
