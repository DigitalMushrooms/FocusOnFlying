using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.StatusyMisji.Queries.PobierzStatusyMisji;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.StatusyMisji.Queries.PobierzStatusMisji
{
    public class PobierzStatusMisjiQuery : IRequest<StatusMisjiDto>
    {
        public string Nazwa { get; set; }
    }

    public class PobierzStatusMisjiQueryHandler : IRequestHandler<PobierzStatusMisjiQuery, StatusMisjiDto>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzStatusMisjiQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<StatusMisjiDto> Handle(PobierzStatusMisjiQuery request, CancellationToken cancellationToken)
        {
            StatusMisjiDto statusMisji = await _focusOnFlyingContext.StatusyMisji
                .ProjectTo<StatusMisjiDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Nazwa.ToLower() == request.Nazwa.ToLower().Trim());
            return statusMisji;
        }
    }
}
