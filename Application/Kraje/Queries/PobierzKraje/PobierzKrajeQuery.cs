using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Kraje.Queries.PobierzKraje
{
    public class PobierzKrajeQuery : IRequest<List<KrajDto>>
    {
        public string SortField { get; set; }
        public int SortOrder { get; set; }
    }

    public class PobierzKrajeQueryHandler : IRequestHandler<PobierzKrajeQuery, List<KrajDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzKrajeQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<List<KrajDto>> Handle(PobierzKrajeQuery request, CancellationToken cancellationToken)
        {
            List<KrajDto> kraje = await _focusOnFlyingContext.Kraje
                .ProjectTo<KrajDto>(_mapper.ConfigurationProvider)
                .GetSorted(request.SortField, request.SortOrder)
                .ToListAsync(cancellationToken);

            return kraje;
        }
    }
}
