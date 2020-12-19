using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Queries.PobierzUslugi
{
    public class PobierzUslugiQuery : IRequest<Common.Models.PagedResult<UslugaDto>>, IStronnicowalne, ISortowalne
    {
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string Sort { get; set; }
    }

    public class PobierzUslugiQueryHandler : IRequestHandler<PobierzUslugiQuery, Common.Models.PagedResult<UslugaDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public PobierzUslugiQueryHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<Common.Models.PagedResult<UslugaDto>> Handle(PobierzUslugiQuery request, CancellationToken cancellationToken)
        {
            var mapping = _propertyMappingService.GetPropertyMapping<UslugaDto, Usluga>();

            var uslugi = await _focusOnFlyingContext.Uslugi
                .Include(x => x.Misje).ThenInclude(x => x.StatusMisji)
                .Include(x => x.Misje).ThenInclude(x => x.TypMisji)
                .ApplySort(request.Sort, mapping)
                .ProjectTo<UslugaDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return uslugi;
        }
    }
}
