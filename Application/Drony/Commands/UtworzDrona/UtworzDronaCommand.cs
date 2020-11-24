using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Drony.Commands.UtworzDrona
{
    public class UtworzDronaCommand : IRequest, IMapFrom<Dron>
    {
        public string Producent { get; set; }
        public string Model { get; set; }
        public string NumerSeryjny { get; set; }
        public Guid IdTypuDrona { get; set; }
        public long DataNastepnegoPrzegladu { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dron, UtworzDronaCommand>()
                .ForMember(x => x.DataNastepnegoPrzegladu, x => x.MapFrom(y => y.DataNastepnegoPrzegladu.ToUnixTime()))
                .ReverseMap()
                .ForPath(x => x.DataNastepnegoPrzegladu, x => x.MapFrom(y => y.DataNastepnegoPrzegladu.ToLocalDateTime()));
        }
    }

    public class UtworzDronaCommandHandler : IRequestHandler<UtworzDronaCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public UtworzDronaCommandHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UtworzDronaCommand request, CancellationToken cancellationToken)
        {
            var dronEntity = _mapper.Map<Dron>(request);
            _focusOnFlyingContext.Drony.Add(dronEntity);
            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
