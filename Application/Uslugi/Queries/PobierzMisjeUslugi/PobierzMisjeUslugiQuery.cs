using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using NSwag.Annotations;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Queries.PobierzMisjeUslugi
{
    public class PobierzMisjeUslugiQuery : IRequest<Models.PagedResult<MisjaDto>>, ISortowalne, IStronnicowalne
    {
        [OpenApiIgnore]
        public Guid Id { get; set; }
        public string Sort { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }
    }

    public class PobierzMisjeUslugiQueryHandler : IRequestHandler<PobierzMisjeUslugiQuery, Models.PagedResult<MisjaDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public PobierzMisjeUslugiQueryHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<Models.PagedResult<MisjaDto>> Handle(PobierzMisjeUslugiQuery request, CancellationToken cancellationToken)
        {
            var mapping = _propertyMappingService.GetPropertyMapping<MisjaDto, Misja>();

            Models.PagedResult<MisjaDto> misje = await _focusOnFlyingContext.Misje
                .Where(x => x.IdUslugi == request.Id)
                .ApplySort(request.Sort, mapping)
                .ProjectTo<MisjaDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return misje;
        }
    }
}
