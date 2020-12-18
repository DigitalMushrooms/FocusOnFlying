using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Queries.PobierzMisjeUslugi
{
    public class PobierzMisjeUslugiQuery : IRequest<Common.Models.PagedResult<MisjaDto>>, ISortowalne, IStronnicowalne
    {
        [OpenApiIgnore]
        public Guid Id { get; set; }
        public List<string> Statusy { get; set; } = new List<string>();
        public string Sort { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }
    }

    public class PobierzMisjeUslugiQueryHandler : IRequestHandler<PobierzMisjeUslugiQuery, Common.Models.PagedResult<MisjaDto>>
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

        public async Task<Common.Models.PagedResult<MisjaDto>> Handle(PobierzMisjeUslugiQuery request, CancellationToken cancellationToken)
        {
            var mapping = _propertyMappingService.GetPropertyMapping<MisjaDto, Misja>();

            IQueryable<Misja> query = _focusOnFlyingContext.Misje
                .Include(x => x.StatusMisji)
                .Where(x => x.IdUslugi == request.Id);

            if (request.Statusy.Any())
            {
                query = query.Where(x => request.Statusy.Contains(x.StatusMisji.Nazwa));
            }

            Common.Models.PagedResult<MisjaDto> misje = await query
                .ApplySort(request.Sort, mapping)
                .ProjectTo<MisjaDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return misje;
        }
    }
}
