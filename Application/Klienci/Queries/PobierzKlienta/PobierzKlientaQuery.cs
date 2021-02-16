using AutoMapper;
using AutoMapper.QueryableExtensions;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Klienci.Queries.PobierzKlienta
{
    public class PobierzKlientaQuery : IRequest<KlientDto>
    {
        public Guid Id { get; set; }
    }

    public class PobierzKlientaQueryHandler : IRequestHandler<PobierzKlientaQuery, KlientDto>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public PobierzKlientaQueryHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<KlientDto> Handle(PobierzKlientaQuery request, CancellationToken cancellationToken)
        {
            var klient = await _focusOnFlyingContext.Klienci
                .ProjectTo<KlientDto>(_mapper.ConfigurationProvider)
                .SingleAsync(x => x.Id == request.Id);

            return klient;
        }
    }
}
