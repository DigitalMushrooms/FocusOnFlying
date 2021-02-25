using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusUslugi;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusyUslugi
{
    public class PobierzStatusyUslugiQuery : IRequest<List<StatusUslugiDto>>
    {
    }

    public class PobierzStatusyUslugiQueryHandler : IRequestHandler<PobierzStatusyUslugiQuery, List<StatusUslugiDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzStatusyUslugiQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<List<StatusUslugiDto>> Handle(PobierzStatusyUslugiQuery request, CancellationToken cancellationToken)
        {
            List<StatusUslugiDto> statusyUslugi = await _focusOnFlyingContext.StatusyUslugi
                .ProjectTo<StatusUslugiDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return statusyUslugi;
        }
    }
}
