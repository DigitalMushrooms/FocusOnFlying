using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Drony.Queries.PobierzDrony
{
    public class PobierzDronyQuery : IRequest<PagedResult<DronDto>>, IStronnicowalne, ISortowalne
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
    }

    public class PobierzDronyQueryHandler : IRequestHandler<PobierzDronyQuery, PagedResult<DronDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzDronyQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<DronDto>> Handle(PobierzDronyQuery request, CancellationToken cancellationToken)
        {
            PagedResult<DronDto> drony = await _focusOnFlyingContext.Drony
                .GetSorted(request.SortField, request.SortOrder)
                .ProjectTo<DronDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return drony;
        }
    }
}
