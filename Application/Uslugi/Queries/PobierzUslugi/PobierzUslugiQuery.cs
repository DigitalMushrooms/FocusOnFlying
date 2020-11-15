using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Queries.PobierzUslugi
{
    public class PobierzUslugiQuery : IRequest<List<UslugaDto>>
    {
    }

    public class PobierzUslugiQueryHandler : IRequestHandler<PobierzUslugiQuery, List<UslugaDto>>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzUslugiQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<List<UslugaDto>> Handle(PobierzUslugiQuery request, CancellationToken cancellationToken)
        {
            var uslugi = await _focusOnFlyingContext.Uslugi
                .Include(x => x.Misje).ThenInclude(x => x.StatusMisji)
                .Include(x => x.Misje).ThenInclude(x => x.TypMisji)
                .ProjectTo<UslugaDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return uslugi;
        }
    }
}
