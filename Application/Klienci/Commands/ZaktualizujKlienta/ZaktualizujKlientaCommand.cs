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

namespace FocusOnFlying.Application.Klienci.Commands.ZaktualizujKlienta
{
    public class ZaktualizujKlientaCommand : IRequest, IMapFrom<Klient>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Nazwa { get; set; }
        public Guid IdKraju { get; set; }
        public string Pesel { get; set; }
        public string Regon { get; set; }
        public string Nip { get; set; }
        public string NumerPaszportu { get; set; }
        public string NumerTelefonu { get; set; }
        public string KodPocztowy { get; set; }
        public string Ulica { get; set; }
        public string NumerDomu { get; set; }
        public string NumerLokalu { get; set; }
        public string Miejscowosc { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ZaktualizujKlientaCommand, Klient>().ReverseMap();
        }
    }

    public class ZaktualizujKlientaCommandHandler : IRequestHandler<ZaktualizujKlientaCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public ZaktualizujKlientaCommandHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ZaktualizujKlientaCommand request, CancellationToken cancellationToken)
        {
            var klientEncja = await _focusOnFlyingContext.Klienci.SingleAsync(x => x.Id == request.Id);

            _mapper.Map(request, klientEncja);

            await _focusOnFlyingContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
