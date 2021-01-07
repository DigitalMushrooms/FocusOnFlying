using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Commands.UtworzFaktureUslugi
{
    public class UtworzFaktureUslugiCommand : IRequest, IMapFrom<Faktura>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string NumerFaktury { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscBrutto { get; set; }
        public bool ZaplaconaFaktura { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UtworzFaktureUslugiCommand, Faktura>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }

    public class UtworzFaktureUslugiCommandHandler : IRequestHandler<UtworzFaktureUslugiCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public UtworzFaktureUslugiCommandHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UtworzFaktureUslugiCommand request, CancellationToken cancellationToken)
        {
            Usluga usluga = await _focusOnFlyingContext.Uslugi.SingleAsync(x => x.Id == request.Id);
            Faktura faktura = _mapper.Map<Faktura>(request);
            
            usluga.Faktura = faktura;

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
