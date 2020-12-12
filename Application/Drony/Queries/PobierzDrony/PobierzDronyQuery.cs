using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Drony.Queries.PobierzDrony
{
    public class PobierzDronyQuery : IRequest<PagedResult<DronDto>>, IStronnicowalne, ISortowalne
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string Sort { get; set; }
    }

    public class PobierzDronyQueryHandler : IRequestHandler<PobierzDronyQuery, PagedResult<DronDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public PobierzDronyQueryHandler(
            IFocusOnFlyingContext focusOnFlyingContext, 
            IMapper mapper, 
            IPropertyMappingService propertyMappingService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<PagedResult<DronDto>> Handle(PobierzDronyQuery request, CancellationToken cancellationToken)
        {
            var mapping = _propertyMappingService.GetPropertyMapping<DronDto, Dron>();

            PagedResult<DronDto> drony = await _focusOnFlyingContext.Drony
                .ApplySort(request.Sort, mapping)
                .ProjectTo<DronDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return drony;
        }
    }
}
