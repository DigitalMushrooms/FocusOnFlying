using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Faktury.Commands.UsunFakture
{
    public class UsunFaktureCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class UsunFaktureCommandHandler : IRequestHandler<UsunFaktureCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public UsunFaktureCommandHandler(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
        }

        public async Task<Unit> Handle(UsunFaktureCommand request, CancellationToken cancellationToken)
        {
            Usluga usluga = await _focusOnFlyingContext.Uslugi
                .Include(x => x.Faktura)
                .SingleAsync(x => x.IdFaktury == request.Id);

            usluga.IdFaktury = null;

            _focusOnFlyingContext.Faktury.Remove(usluga.Faktura);
            
            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
