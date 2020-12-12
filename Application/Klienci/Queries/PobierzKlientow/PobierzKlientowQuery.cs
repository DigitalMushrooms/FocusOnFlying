using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Klienci.Queries.PobierzKlientow
{
    public class PobierzKlientowQuery : IRequest<PagedResult<KlientDto>>, ISzukalne, ISortowalne, IStronnicowalne
    {
        public string Fraza { get; set; }
        public string Pesel { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Sort { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }
    }

    public class PobierzKlientowQueryHandler : IRequestHandler<PobierzKlientowQuery, PagedResult<KlientDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public PobierzKlientowQueryHandler(
            IFocusOnFlyingContext focusOnFlyingContext, 
            IMapper mapper,
            IPropertyMappingService propertyMappingService)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<PagedResult<KlientDto>> Handle(PobierzKlientowQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Klient> query = _focusOnFlyingContext.Klienci.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Fraza))
            {
                var fraza = request.Fraza.ToLower();
                query = query.Where(x => x.Imie.ToLower().Contains(fraza) ||
                                         x.Nazwisko.ToLower().Contains(fraza) ||
                                         x.Nazwa.ToLower().Contains(fraza) ||
                                         x.NumerPaszportu.ToLower().Contains(fraza) ||
                                         x.NumerTelefonu.ToLower().Contains(fraza) ||
                                         x.KodPocztowy.ToLower().Contains(fraza) ||
                                         x.Miejscowosc.ToLower().Contains(fraza) ||
                                         x.Ulica.ToLower().Contains(fraza) ||
                                         x.NumerDomu.ToLower().Contains(fraza) ||
                                         x.NumerLokalu.ToLower().Contains(fraza) ||
                                         x.Email.ToLower().Contains(fraza) ||
                                         x.Kraj.NazwaKraju.ToLower().Contains(fraza));
                query = query.Where(x => x.Kraj.NazwaKraju.ToLower().Contains(fraza));
            }
            if(!string.IsNullOrWhiteSpace(request.Pesel))
            {
                query = query.Where(x => x.Pesel.Contains(request.Pesel));
            }
            if (!string.IsNullOrWhiteSpace(request.Nip))
            {
                query = query.Where(x => x.Nip.Contains(request.Nip));
            }
            if (!string.IsNullOrWhiteSpace(request.Regon))
            {
                query = query.Where(x => x.Regon.Contains(request.Regon));
            }

            var mapping = _propertyMappingService.GetPropertyMapping<KlientDto, Klient>();
            
            var klienci = await query
                .ApplySort(request.Sort, mapping)
                .ProjectTo<KlientDto>(_mapper.ConfigurationProvider)
                .GetPagedAsync(request.Offset, request.Rows);

            return klienci;
        }
    }
}
