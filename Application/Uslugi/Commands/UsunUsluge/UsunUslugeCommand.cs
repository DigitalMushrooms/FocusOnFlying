using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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

        public UsunUslugeCommandHandler(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
        }

        public async Task<Unit> Handle(UsunUslugeCommand request, CancellationToken cancellationToken)
        {
            Usluga usluga = await _focusOnFlyingContext.Uslugi.SingleAsync(x => x.Id == request.Id);
            _focusOnFlyingContext.Uslugi.Remove(usluga);

            await _focusOnFlyingContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
