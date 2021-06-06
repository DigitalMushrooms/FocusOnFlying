using FluentValidation;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje
{
    public class ZaktualizujMisjeCommandValidator : AbstractValidator<MisjaUpdateDto>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public ZaktualizujMisjeCommandValidator(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;

            RuleFor(x => x.DataRozpoczecia)
                .MustAsync(DataMusiMiecWolnyTerminNaDrony)
                .WithMessage("Dron jest już wykorzystywany w tym terminie.");
        }

        private async Task<bool> DataMusiMiecWolnyTerminNaDrony(
            MisjaUpdateDto misjaUpdateDto, long? dataRozpoczecia, CancellationToken cancellationToken)
        {
            if (!dataRozpoczecia.HasValue || !misjaUpdateDto.DataZakonczenia.HasValue)
                return true;

            DateTime dataRozpoczeciaDateTime = dataRozpoczecia.Value.ToLocalDateTime();
            DateTime dataZakonczeniaDateTime = misjaUpdateDto.DataZakonczenia.Value.ToLocalDateTime();

            foreach (MisjaDronDto misjaDron in misjaUpdateDto.MisjeDrony)
            {
                bool istniejeTakiDron = await _focusOnFlyingContext.MisjeDrony
                .Include(x => x.Misja)
                .Include(x => x.Dron)
                .AnyAsync(x =>
                    x.Misja.Id != misjaDron.IdMisji &&
                    x.Dron.Id == misjaDron.IdDrona &&
                    (dataRozpoczeciaDateTime >= x.Misja.DataRozpoczecia && 
                    dataRozpoczeciaDateTime <= x.Misja.DataZakonczenia 
                    ||
                    dataZakonczeniaDateTime >= x.Misja.DataRozpoczecia && 
                    dataZakonczeniaDateTime <= x.Misja.DataZakonczenia));

                return !istniejeTakiDron;
            }

            return true;
        }
    }
}
