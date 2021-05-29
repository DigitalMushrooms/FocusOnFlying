using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
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
        private readonly CultureInfo _polishCultureInfo = new CultureInfo("pl-pl", false);
        private Misja misjaEntity;
        private readonly StringBuilder _stringBuilder = new StringBuilder(
            @"Dzień dobry,<br/>
            Pragniemy poinformować o zmianie misji w Twojej usłudze.<br/>
            Podsumowanie zmian:<br/>"
        );

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
            misjaEntity = await _focusOnFlyingContext.Misje
                .Include(x => x.MisjeDrony)
                .SingleAsync(x => x.Id == request.Id);

            var misja = _mapper.Map<MisjaUpdateDto>(misjaEntity);

            misja.PropertyChanged += MisjaPropertyChanged;
            request.Patch.ApplyTo(misja);
            misja.PropertyChanged -= MisjaPropertyChanged;

            ValidationResult validationResult = await _validator.ValidateAsync(misja);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToList());
            }

            await NadajStatusMisji(misja);

            _mapper.Map(misja, misjaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            Klient klient = (await _focusOnFlyingContext.Misje
                .Include(x => x.Usluga)
                .ThenInclude(x => x.Klient)
                .SingleAsync(x => x.Id == request.Id)).Usluga.Klient;
            await _mailService.WyslijWadomoscEmail(klient.Email, "Zaktualizowano misję", _stringBuilder.ToString());

            return Unit.Value;
        }

        private void MisjaPropertyChanged(object sender, PropertyChangedEventArgs property)
        {
            var misja = sender as MisjaUpdateDto;
            if (property.PropertyName == nameof(misja.Nazwa))
            {
                _stringBuilder.AppendLine(
                    $"Misja: <span style=\"text-decoration: line-through;\">{misjaEntity.Nazwa}</span> {misja.Nazwa}"
                );
            }
            if (property.PropertyName == nameof(misja.DataRozpoczecia))
            {
                if (!_stringBuilder.ToString().Contains("<ul>"))
                {
                    _stringBuilder.AppendLine("<ul>");
                }
                _stringBuilder.AppendLine(
                    $"<li>Data rozpoczęcia: <span style=\"text-decoration: line-through;\">{misjaEntity.DataRozpoczecia?.ToString("f", _polishCultureInfo)}</span> {misja.DataRozpoczecia.ToLocalDateTime()?.ToString("f", _polishCultureInfo) ?? "N/D"}</li>");
            }
            if (property.PropertyName == nameof(misja.DataZakonczenia))
            {
                if (!_stringBuilder.ToString().Contains("<ul>"))
                {
                    _stringBuilder.AppendLine("<ul>");
                }
                _stringBuilder.AppendLine(
                    $"<li>Data zakończenia: <span style=\"text-decoration: line-through;\">{misjaEntity.DataZakonczenia?.ToString("f", _polishCultureInfo)}</span> {misja.DataZakonczenia.ToLocalDateTime()?.ToString("f", _polishCultureInfo) ?? "N/D"}</li>");
            }
            if (property.PropertyName == nameof(misja.Opis))
            {
                if (!_stringBuilder.ToString().Contains("<ul>"))
                {
                    _stringBuilder.AppendLine("<ul>");
                }
                _stringBuilder.AppendLine(
                    $"<li>Opis: <span style=\"text-decoration: line-through;\">{misjaEntity.Opis}</span> {misja.Opis}</li>");
            }
            if (property.PropertyName == nameof(misja.SzerokoscGeograficzna) || property.PropertyName == nameof(misja.DlugoscGeograficzna))
            {
                if (!_stringBuilder.ToString().Contains("<ul>"))
                {
                    _stringBuilder.AppendLine("<ul>");
                }
                _stringBuilder.AppendLine($"<li>Lokalizacja miejsca: <span style=\"text-decoration: line-through;\"><a href=\"https://www.google.com/maps/@{misjaEntity.SzerokoscGeograficzna},{misjaEntity.DlugoscGeograficzna},16z\" target=\"_blank\">Mapa Google</a></span> <a href=\"https://www.google.com/maps/@{misja.SzerokoscGeograficzna},{misja.DlugoscGeograficzna},16z\" target=\"_blank\">Mapa Google</a></li>");
            }
        }

        private async Task NadajStatusMisji(MisjaUpdateDto misja)
        {
            if (misja.DataRozpoczecia.HasValue && misja.DataZakonczenia.HasValue)
            {
                StatusMisji statusUtworzonejMisji = await _focusOnFlyingContext.StatusyMisji.SingleAsync(x => x.Nazwa == "Utworzona");
                if (misja.IdStatusuMisji == statusUtworzonejMisji.Id)
                {
                    misja.IdStatusuMisji = (await _focusOnFlyingContext.StatusyMisji.SingleAsync(x => x.Nazwa == "Zaplanowana")).Id;
                }
            }
        }
    }
}
