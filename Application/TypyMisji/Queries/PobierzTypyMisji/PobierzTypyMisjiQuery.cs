using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.TypyMisji.Queries.PobierzTypyMisji
{
    public class PobierzTypyMisjiQuery : IRequest<List<TypMisjiDto>>
    {
    }

    public class PobierzTypyMisjiQueryHandler : IRequestHandler<PobierzTypyMisjiQuery, List<TypMisjiDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzTypyMisjiQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<List<TypMisjiDto>> Handle(PobierzTypyMisjiQuery request, CancellationToken cancellationToken)
        {
            List<TypMisjiDto> typyMisji = await _focusOnFlyingContext.TypyMisji
                .ProjectTo<TypMisjiDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return typyMisji;
        }
    }
}
