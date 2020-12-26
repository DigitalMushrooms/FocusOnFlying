using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Misje.Queries
{
    public class PobierzMisjeQuery : IRequest<PagedResult<MisjaDto>>, IStronnicowalne, ISortowalne
    {
        public long? DataPrzyjeciaZleceniaOd { get; set; }
        public long? DataPrzyjeciaZleceniaDo { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string Sort { get; set; }
    }

    public class PobierzMisjeQueryHandler : IRequestHandler<PobierzMisjeQuery, PagedResult<MisjaDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public PobierzMisjeQueryHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<PagedResult<MisjaDto>> Handle(PobierzMisjeQuery request, CancellationToken cancellationToken)
        {
            var mapping = _propertyMappingService.GetPropertyMapping<MisjaDto, Misja>();

            var query = _focusOnFlyingContext.Misje.Include(x => x.Usluga).AsQueryable();

            if (request.DataPrzyjeciaZleceniaOd.HasValue)
            {
                query = query.Where(x => x.Usluga.DataPrzyjeciaZlecenia >= request.DataPrzyjeciaZleceniaOd.ToLocalDateTime());
            }
            if (request.DataPrzyjeciaZleceniaDo.HasValue)
            {
                query = query.Where(x => x.Usluga.DataPrzyjeciaZlecenia <= request.DataPrzyjeciaZleceniaDo.ToLocalDateTime());
            }

            PagedResult<MisjaDto> misje = await query
                .Include(x => x.Usluga).ThenInclude(x => x.Klient).ThenInclude(x => x.Kraj)
                .Include(x => x.Usluga).ThenInclude(x => x.StatusUslugi)
                .Include(x => x.StatusMisji)
                .Include(x => x.TypMisji)
                .Include(x => x.MisjeDrony).ThenInclude(x => x.Dron).ThenInclude(x => x.TypDrona)
                .ApplySort(request.Sort, mapping)
                .ProjectTo<MisjaDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return misje;
        }
    }
}
