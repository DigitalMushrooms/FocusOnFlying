using FluentValidation;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzMisjeUslugi
{
    public class UtworzMisjeUslugiCommandValidation : AbstractValidator<UtworzMisjeUslugiCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public UtworzMisjeUslugiCommandValidation(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;

            RuleFor(x => x.Id)
                .MustAsync(TenSamDronNieMozeBycUzywanyWTymDniu)
                .WithMessage("Dron jest już wykorzystywany w tym terminie.");
        }

        private async Task<bool> TenSamDronNieMozeBycUzywanyWTymDniu(
            UtworzMisjeUslugiCommand command, Guid arg1, CancellationToken arg2)
        {
            if (!command.Misja.DataRozpoczecia.HasValue || !command.Misja.DataZakonczenia.HasValue)
                return true;

            foreach (MisjaDronDto misjaDron in command.Misja.MisjeDrony)
            {
                bool istniejeTakiDron = await _focusOnFlyingContext.MisjeDrony
                    .Include(x => x.Misja)
                    .Include(x => x.Dron)
                    .AnyAsync(x =>
                        x.Dron.Id == misjaDron.IdDrona &&
                        (command.Misja.DataRozpoczecia.ToLocalDateTime() >= x.Misja.DataRozpoczecia &&
                        command.Misja.DataRozpoczecia.ToLocalDateTime() <= x.Misja.DataZakonczenia
                        ||
                        command.Misja.DataZakonczenia.ToLocalDateTime() >= x.Misja.DataRozpoczecia &&
                        command.Misja.DataZakonczenia.ToLocalDateTime() <= x.Misja.DataZakonczenia));

                if (istniejeTakiDron)
                    return false;

            }
            return true;
        }
    }
}
