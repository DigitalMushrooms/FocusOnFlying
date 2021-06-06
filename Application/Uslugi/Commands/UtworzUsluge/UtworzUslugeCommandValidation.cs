using FluentValidation;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge
{
    public class UtworzUslugeCommandValidation : AbstractValidator<UtworzUslugeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public UtworzUslugeCommandValidation(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;

            RuleFor(x => x.Misje)
                .MustAsync(TenSamDronNieMozeBycUzywanyWTymDniu)
                .WithMessage("Dron jest już wykorzystywany w tym terminie.");
        }

        private async Task<bool> TenSamDronNieMozeBycUzywanyWTymDniu(
            UtworzUslugeCommand command, List<MisjaDto> arg1, CancellationToken arg2)
        {
            var misjeZPodanaData = command.Misje.Where(x => x.DataRozpoczecia.HasValue && x.DataZakonczenia.HasValue);
            foreach (MisjaDto misja in misjeZPodanaData)
            {
                if (!misja.DataRozpoczecia.HasValue || !misja.DataZakonczenia.HasValue)
                    return true;

                foreach (MisjaDronDto misjaDron in misja.MisjeDrony)
                {
                    bool istniejeTakiDron = await _focusOnFlyingContext.MisjeDrony
                        .Include(x => x.Misja)
                        .Include(x => x.Dron)
                        .AnyAsync(x =>
                            x.Dron.Id == misjaDron.IdDrona &&
                            (misja.DataRozpoczecia.ToLocalDateTime() >= x.Misja.DataRozpoczecia &&
                            misja.DataRozpoczecia.ToLocalDateTime() <= x.Misja.DataZakonczenia
                            ||
                            misja.DataZakonczenia.ToLocalDateTime() >= x.Misja.DataRozpoczecia &&
                            misja.DataZakonczenia.ToLocalDateTime() <= x.Misja.DataZakonczenia));

                    if (istniejeTakiDron)
                        return false;
                }
            }
            return true;
        }
    }
}
