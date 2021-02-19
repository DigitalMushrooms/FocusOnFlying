using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Audyty.Queries.PobierzAudyty
{
    public class PobierzAudytyQuery : IRequest<PagedResult<AudytDto>>, IStronnicowalne
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
    }

    public class PobierzAudytyQueryHandler : IRequestHandler<PobierzAudytyQuery, PagedResult<AudytDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzAudytyQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<AudytDto>> Handle(PobierzAudytyQuery request, CancellationToken cancellationToken)
        {
            var query = _focusOnFlyingContext.Audyty.AsQueryable();
            var wynik = await query
                .ProjectTo<AudytDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);
            return wynik;
        }
    }
}
