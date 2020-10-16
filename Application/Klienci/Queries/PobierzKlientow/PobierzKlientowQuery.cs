using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Klienci.Queries.PobierzKlientow
{
    public class PobierzKlientowQuery : IRequest<List<KlienciDto>>
    {
    }

    public class PobierzKlientowQueryHandler : IRequestHandler<PobierzKlientowQuery, List<KlienciDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzKlientowQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<List<KlienciDto>> Handle(PobierzKlientowQuery request, CancellationToken cancellationToken)
        {
            var klienci = await _focusOnFlyingContext.Klienci
                .ProjectTo<KlienciDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return klienci;
        }
    }
}
