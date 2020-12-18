using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Misje.Commands.UsunMisje
{
    public class UsunMisjeCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class UsunMisjeCommandHandler : IRequestHandler<UsunMisjeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public UsunMisjeCommandHandler(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
        }

        public async Task<Unit> Handle(UsunMisjeCommand request, CancellationToken cancellationToken)
        {
            Misja misja = await _focusOnFlyingContext.Misje.SingleAsync(x => x.Id == request.Id);
            StatusMisji statusMisjiAnulowana = await _focusOnFlyingContext.StatusyMisji.SingleAsync(x => x.Nazwa == "Anulowana");

            misja.IdStatusuMisji = statusMisjiAnulowana.Id;
            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
