using FluentValidation;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using Microsoft.EntityFrameworkCore;
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

            RuleFor(x => x)
                .MustAsync(TenSamDronNieMozeBycUzywanyWTymDniu);
        }

        private async Task<bool> TenSamDronNieMozeBycUzywanyWTymDniu(
            UtworzMisjeUslugiCommand command, CancellationToken cancellationToken)
        {
            if (!command.Misja.DataRozpoczecia.HasValue || !command.Misja.DataZakonczenia.HasValue)
                return true;

            var idDrona = command.Misja.MisjeDrony[0].IdDrona;

            bool istniejeTakiDron = await _focusOnFlyingContext.MisjeDrony
            .Include(x => x.Misja)
            .Include(x => x.Dron)
            .AnyAsync(x =>
                x.Dron.Id == idDrona &&
                (command.Misja.DataRozpoczecia.ToLocalDateTime() >= x.Misja.DataRozpoczecia && 
                command.Misja.DataRozpoczecia.ToLocalDateTime() <= x.Misja.DataZakonczenia 
                ||
                command.Misja.DataZakonczenia.ToLocalDateTime() >= x.Misja.DataRozpoczecia && 
                command.Misja.DataZakonczenia.ToLocalDateTime() <= x.Misja.DataZakonczenia));

            return istniejeTakiDron;
        }
    }
}
