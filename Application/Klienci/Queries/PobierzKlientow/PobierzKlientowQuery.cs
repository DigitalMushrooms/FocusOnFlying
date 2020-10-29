using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Klienci.Queries.PobierzKlientow
{
    public class PobierzKlientowQuery : IRequest<PagedResult<KlientDto>>, IStronnicowalne, ISortowalne
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
    }

    public class PobierzKlientowQueryHandler : IRequestHandler<PobierzKlientowQuery, PagedResult<KlientDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzKlientowQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<KlientDto>> Handle(PobierzKlientowQuery request, CancellationToken cancellationToken)
        {
            PagedResult<KlientDto> klienci = await _focusOnFlyingContext.Klienci.Include(x => x.Kraj)
                .ProjectTo<KlientDto>(_mapper.ConfigurationProvider)
                .GetSorted(request.SortField, request.SortOrder)
                .GetPagedAsync(request.Offset, request.Rows);

            return klienci;
        }
    }
}
