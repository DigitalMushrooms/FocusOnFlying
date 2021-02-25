using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Queries.PobierzUslugi
{
    public class PobierzUslugiQuery : IRequest<PagedResult<UslugaDto>>, IStronnicowalne, ISortowalne
    {
        public long? DataPrzyjeciaZleceniaOd { get; set; }
        public long? DataPrzyjeciaZleceniaDo { get; set; }
        public Guid? IdKlienta { get; set; }
        public Guid? IdStatusuUslugi { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }
        public string Sort { get; set; }
    }

    public class PobierzUslugiQueryHandler : IRequestHandler<PobierzUslugiQuery, PagedResult<UslugaDto>>
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

        public async Task<PagedResult<UslugaDto>> Handle(PobierzUslugiQuery request, CancellationToken cancellationToken)
        {
            var mapping = _propertyMappingService.GetPropertyMapping<UslugaDto, Usluga>();

            var query = _focusOnFlyingContext.Uslugi.AsQueryable();

            if (request.DataPrzyjeciaZleceniaOd.HasValue)
            {
                query = query.Where(x => x.DataPrzyjeciaZlecenia >= request.DataPrzyjeciaZleceniaOd.ToLocalDateTime());
            }
            if (request.DataPrzyjeciaZleceniaDo.HasValue)
            {
                query = query.Where(x => x.DataPrzyjeciaZlecenia >= request.DataPrzyjeciaZleceniaOd.ToLocalDateTime());
            }
            if (request.IdStatusuUslugi.HasValue)
            {
                query = query.Where(x => x.StatusUslugi.Id == request.IdStatusuUslugi);
            }
            if (request.IdKlienta.HasValue)
            {
                query = query.Where(x => x.Klient.Id == request.IdKlienta);
            }

            PagedResult<UslugaDto> uslugi = await query
                .Include(x => x.Faktura)
                .Include(x => x.Klient).ThenInclude(x => x.Kraj)
                .Include(x => x.StatusUslugi)
                .Include(x => x.Misje).ThenInclude(x => x.StatusMisji)
                .Include(x => x.Misje).ThenInclude(x => x.TypMisji)
                .Include(x => x.Misje).ThenInclude(x => x.MisjeDrony).ThenInclude(x => x.Dron).ThenInclude(x => x.TypDrona)
                .ApplySort(request.Sort, mapping)
                .ProjectTo<UslugaDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return uslugi;
        }
    }
}
