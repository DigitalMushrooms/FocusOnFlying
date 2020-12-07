using AutoMapper;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje
{
    public class ZaktualizujMisjeCommand : IRequest, IMapFrom<Misja>
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<MisjaUpdateDto> Patch { get; set; }

        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<ZaktualizujMisjeCommand, Misja>()
        //        .ForMember(d => d.Id, o => o.Ignore())
        //        .ForMember(d => d.Idm, o => o.Ignore());
        //}
    }

    public class ZaktualizujMisjeCommandHandler : IRequestHandler<ZaktualizujMisjeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;

        public ZaktualizujMisjeCommandHandler(IFocusOnFlyingContext focusOnFlyingContext, IMapper mapper)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ZaktualizujMisjeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
