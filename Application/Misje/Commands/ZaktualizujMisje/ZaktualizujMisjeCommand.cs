using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = FocusOnFlying.Application.Common.Exceptions.ValidationException;

namespace FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje
{
    public class ZaktualizujMisjeCommand : IRequest
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<MisjaUpdateDto> Patch { get; set; }
    }

    public class ZaktualizujMisjeCommandHandler : IRequestHandler<ZaktualizujMisjeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IValidator<MisjaUpdateDto> _validator;
        private readonly IMailService _mailService;
        private ZaktualizujMisjeCommand _request;

        public ZaktualizujMisjeCommandHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IValidator<MisjaUpdateDto> validator,
            IMailService mailService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _validator = validator;
            _mailService = mailService;
        }

        public async Task<Unit> Handle(ZaktualizujMisjeCommand request, CancellationToken cancellationToken)
        {
            _request = request;

            Misja misjaEntity = await _focusOnFlyingContext.Misje
                .Include(x => x.MisjeDrony)
                .SingleAsync(x => x.Id == request.Id);
            var misja = _mapper.Map<MisjaUpdateDto>(misjaEntity);

            request.Patch.ApplyTo(misja);

            ValidationResult validationResult = await _validator.ValidateAsync(misja);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToList());
            }

            string opisWiadomosci = WygenerujOpisWiadomosci(misjaEntity);
            _mapper.Map(misja, misjaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            Klient klient = (await _focusOnFlyingContext.Misje
                .Include(x => x.Usluga)
                .ThenInclude(x => x.Klient)
                .SingleAsync(x => x.Id == request.Id)).Usluga.Klient;
            await _mailService.WyslijWadomoscEmail(klient.Email, "Zaktualizowano misję", opisWiadomosci);

            return Unit.Value;
        }

        private string WygenerujOpisWiadomosci(Misja misja)
        {
            List<Operation<MisjaUpdateDto>> operacje = _request.Patch.Operations;
            var listaObserwowanychOperacji = new[] {
                "/nazwa", "/dataRozpoczecia", "/dataZakonczenia", "/opis", "/szerokoscGeograficzna", "/dlugoscGeograficzna"
            };
            if (!operacje.Any(x => listaObserwowanychOperacji.Contains(x.path)))
                return string.Empty;

            CultureInfo polska = new CultureInfo("pl-pl", false);

            StringBuilder sb = new StringBuilder(
                $@"Dzień dobry,<br/>
                Pragniemy poinformować o zmianie misji w Twojej usłudze.<br/>
                Podsumowanie zmian:<br/>");

            if (operacje.Any(x => x.path == "/nazwa"))
            {
                string nowaNazwa = (string)operacje.Single(x => x.path == "/nazwa").value;
                sb.AppendLine($"Misja <span style=\"text-decoration: line-through;\">{misja.Nazwa}</span> {nowaNazwa}");
            }
            else
            {
                sb.AppendLine($"Misja {misja.Nazwa}");
            }
            if (operacje.Any(x => x.path == "/dataRozpoczecia"))
            {
                sb.AppendLine("<ul>");
                DateTime nowaDataRozpoczecia = ((long)operacje.Single(x => x.path == "/dataRozpoczecia").value).ToLocalDateTime();
                sb.AppendLine($"<li>Data rozpoczęcia: <span style=\"text-decoration: line-through;\">{misja.DataRozpoczecia.ToString("f", polska)}</span> {nowaDataRozpoczecia.ToString("f", polska)}</li>");
            }
            if (operacje.Any(x => x.path == "/dataZakonczenia"))
            {
                DateTime nowaDataZakonczenia = ((long)operacje.Single(x => x.path == "/dataZakonczenia").value).ToLocalDateTime();
                sb.AppendLine($"<li>Data zakończenia: <span style=\"text-decoration: line-through;\">{misja.DataZakonczenia.ToString("f", polska)}</span> {nowaDataZakonczenia.ToString("f", polska)}</li>");
            }
            if (operacje.Any(x => x.path == "/opis"))
            {
                string nowyOpis = (string)operacje.Single(x => x.path == "/opis").value;
                sb.AppendLine($"<li>Opis: <span style=\"text-decoration: line-through;\">{misja.Opis}</span> {nowyOpis}</li>");
            }
            if (operacje.Any(x => x.path == "/szerokoscGeograficzna" || x.path == "/dlugoscGeograficzna"))
            {
                double nowaSzerokoscGeograficzna = (double)operacje.Single(x => x.path == "/szerokoscGeograficzna").value;
                double nowaDlugoscGeograficzna = (double)operacje.Single(x => x.path == "/dlugoscGeograficzna").value;
                sb.AppendLine($"<li>Lokalizacja miejsca: <span style=\"text-decoration: line-through;\"><a href=\"https://www.google.com/maps/@{misja.SzerokoscGeograficzna},{misja.DlugoscGeograficzna},16z\" target=\"_blank\">Mapa Google</a></span> <a href=\"https://www.google.com/maps/@{nowaSzerokoscGeograficzna},{nowaDlugoscGeograficzna},16z\" target=\"_blank\">Mapa Google</a></li>");
            }
            if (operacje.Any(x => x.path != "/nazwa"))
            {
                sb.Append("</ul>");
            }

            return sb.ToString();
        }
    }
}
