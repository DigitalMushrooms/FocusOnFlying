using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.TypyDrona.PobierzTypyDrona
{
    public class PobierzTypyDronaQuery : IRequest<List<TypDronaDto>>
    {
    }

    public class PobierzTypyDronaQueryHandler : IRequestHandler<PobierzTypyDronaQuery, List<TypDronaDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzTypyDronaQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<List<TypDronaDto>> Handle(PobierzTypyDronaQuery request, CancellationToken cancellationToken)
        {
            List<TypDronaDto> typyDrona = await _focusOnFlyingContext.TypyDrona
                .ProjectTo<TypDronaDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return typyDrona;
        }
    }
}
