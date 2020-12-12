using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                .ForMember(d => d.DataPrzyjeciaZlecenia, o => o.MapFrom(s => s.DataPrzyjeciaZlecenia.ToLocalDateTime()));
        }
    }

    public class UtworzUslugeCommandHandler : IRequestHandler<UtworzUslugeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private UtworzUslugeCommand _request;

        public UtworzUslugeCommandHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IMailService mailService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<Unit> Handle(UtworzUslugeCommand request, CancellationToken cancellationToken)
        {
            _request = request;

            Usluga uslugaEntity = _mapper.Map<Usluga>(request);
            _focusOnFlyingContext.Uslugi.Attach(uslugaEntity);
            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            Klient klient = await _focusOnFlyingContext.Klienci.AsNoTracking().SingleAsync(x => x.Id == request.IdKlienta);

            await _mailService.WyslijWadomoscEmail(klient.Email, "Utworzono usługę", WygenerujOpisWiadomosci());

            return Unit.Value;
        }

        private string WygenerujOpisWiadomosci()
        {
            CultureInfo polska = new CultureInfo("pl-pl", false);
            var powitanie = $@"Dzień dobry,<br/>
                    Z przyjemnością pragniemy poinformować, że zlecona przez Ciebie usługa została zarejestrowana w naszym systemie.<br/>
                    Podsumowanie:<br/>";
            IEnumerable<string> misje = _request.Misje.Select(x => 
                $@"Misja ""{x.Nazwa}""
                <ul>
                    <li>Data rozpoczęcia: {x.DataRozpoczecia.ToLocalDateTime().ToString("f", polska)}</li>
                    <li>Data zakończenia: {x.DataZakonczenia.ToLocalDateTime().ToString("f", polska)}</li>
                    <li>Opis: ""{x.Opis}""</li>
                    <li>Lokalizacja miejsca: <a href=""https://www.google.com/maps/@{x.SzerokoscGeograficzna},{x.DlugoscGeograficzna},16z"" target=""_blank"">Mapa Google</a></li>
                </ul>");

            return powitanie + string.Join(string.Empty, misje);
        }
    }
}
