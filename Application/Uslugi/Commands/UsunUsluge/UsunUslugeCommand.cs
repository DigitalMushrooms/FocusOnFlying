using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UsunUsluge
{
    public class UsunUslugeCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class UsunUslugeCommandHandler : IRequestHandler<UsunUslugeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMailService _mailService;

        public UsunUslugeCommandHandler(IFocusOnFlyingContext focusOnFlyingContext, IMailService mailService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mailService = mailService;
        }

        public async Task<Unit> Handle(UsunUslugeCommand request, CancellationToken cancellationToken)
        {
            Usluga usluga = await _focusOnFlyingContext.Uslugi.SingleAsync(x => x.Id == request.Id);
            Klient klient = await _focusOnFlyingContext.Klienci.SingleAsync(x => x.Id == usluga.IdKlienta);
            StatusUslugi anulowanaUsluga = await _focusOnFlyingContext.StatusyUslugi
                .SingleAsync(x => x.Nazwa == "Anulowana");

            usluga.IdStatusuUslugi = anulowanaUsluga.Id;

            await _focusOnFlyingContext.SaveChangesAsync();

            await _mailService.WyslijWadomoscEmail(klient.Email, "Usunięto usługę", WygenerujOpisWiadomosci());

            return Unit.Value;
        }

        private string WygenerujOpisWiadomosci()
        {
            var trescWiadomosci = $@"Dzień dobry,<br/>
                    Pragniemy poinformować, że zlecona przez Ciebie usługa została anulowana w naszym systemie.<br/>";

            return trescWiadomosci;
        }
    }
}
