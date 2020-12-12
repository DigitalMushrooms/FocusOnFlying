using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Misje.Queries
{
    public class PobierzMisjeQuery : IRequest<PagedResult<MisjaDto>>
    {
    }

    public class PobierzMisjeQueryHandler : IRequestHandler<PobierzMisjeQuery, PagedResult<MisjaDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public PobierzMisjeQueryHandler(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
        }

        public async Task<PagedResult<MisjaDto>> Handle(PobierzMisjeQuery request, CancellationToken cancellationToken)
        {
            var x = await _focusOnFlyingContext.Misje.ToListAsync();
            throw new NotImplementedException();
        }
    }
}
