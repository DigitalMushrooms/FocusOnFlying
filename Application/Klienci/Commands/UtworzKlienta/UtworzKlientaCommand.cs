using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Klienci.Commands.UtworzKlienta
{
    public class UtworzKlientaCommand : IRequest, IMapFrom<Klient>
    {
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
            profile.CreateMap<UtworzKlientaCommand, Klient>();
            profile.CreateMap<Klient, UtworzKlientaCommand>();
        }
    }

    public class UtworzKlientaCommandHandler : IRequestHandler<UtworzKlientaCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public UtworzKlientaCommandHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UtworzKlientaCommand request, CancellationToken cancellationToken)
        {
            var klient = _mapper.Map<Klient>(request);
            _focusOnFlyingContext.Klienci.Add(klient);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
