using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.StatusyMisji.Queries.PobierzStatusyMisji
{
    public class PobierzStatusyMisjiQuery : IRequest<List<StatusMisjiDto>>
    {
    }

    public class PobierzStatusyMisjiQueryHandler : IRequestHandler<PobierzStatusyMisjiQuery, List<StatusMisjiDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzStatusyMisjiQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<List<StatusMisjiDto>> Handle(PobierzStatusyMisjiQuery request, CancellationToken cancellationToken)
        {
            List<StatusMisjiDto> statusyMisji = await _focusOnFlyingContext.StatusyMisji
                .ProjectTo<StatusMisjiDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return statusyMisji;
        }
    }
}
