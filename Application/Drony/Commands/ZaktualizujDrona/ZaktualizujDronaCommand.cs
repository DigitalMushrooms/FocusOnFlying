using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Drony.Commands.ZaktualizujDrona
{
    public class ZaktualizujDronaCommand : IRequest
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<DronUpdateDto> Patch { get; set; }
    }

    public class ZaktualizujDronaCommandHandler : IRequestHandler<ZaktualizujDronaCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IValidator<DronUpdateDto> _validator;

        public ZaktualizujDronaCommandHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IValidator<DronUpdateDto> validator)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _validator = validator;
        }
        
        public async Task<Unit> Handle(ZaktualizujDronaCommand request, CancellationToken cancellationToken)
        {
            Dron dronEntity = await _focusOnFlyingContext.Drony.SingleAsync(x => x.Id == request.Id);

            var dron = _mapper.Map<DronUpdateDto>(dronEntity);

            request.Patch.ApplyTo(dron);

            ValidationResult validationResult = await _validator.ValidateAsync(dron);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToList());
            }

            _mapper.Map(dron, dronEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
