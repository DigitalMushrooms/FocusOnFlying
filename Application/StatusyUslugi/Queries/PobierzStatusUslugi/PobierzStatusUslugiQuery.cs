using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusUslugi
{
    public class PobierzStatusUslugiQuery : IRequest<StatusUslugiDto>
    {
        public string Nazwa { get; set; }
    }

    public class PobierzStatusUslugiQueryHandler : IRequestHandler<PobierzStatusUslugiQuery, StatusUslugiDto>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzStatusUslugiQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<StatusUslugiDto> Handle(PobierzStatusUslugiQuery request, CancellationToken cancellationToken)
        {
            StatusUslugiDto statusMisji = await _focusOnFlyingContext.StatusyUslugi
                .ProjectTo<StatusUslugiDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Nazwa.ToLower() == request.Nazwa.ToLower().Trim());
            return statusMisji;
        }
    }
}
