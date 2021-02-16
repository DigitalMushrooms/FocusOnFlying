using FocusOnFlying.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Klienci.Commands.UsunKlienta
{
    public class UsunKlientaCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class UsunKlientaCommandHandler : IRequestHandler<UsunKlientaCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public UsunKlientaCommandHandler(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
        }

        public async Task<Unit> Handle(UsunKlientaCommand request, CancellationToken cancellationToken)
        {
            var klient = await _focusOnFlyingContext.Klienci.SingleAsync(x => x.Id == request.Id);
            klient.Aktywny = false;
            await _focusOnFlyingContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
