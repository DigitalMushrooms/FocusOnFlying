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

namespace FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje
{
    public class ZaktualizujMisjeCommand : IRequest
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<MisjaUpdateDto> Patch { get; set; }
    }

    public class ZaktualizujMisjeCommandHandler : IRequestHandler<ZaktualizujMisjeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IValidator<MisjaUpdateDto> _validator;

        public ZaktualizujMisjeCommandHandler(
            IFocusOnFlyingContext focusOnFlyingContext, 
            IMapper mapper,
            IValidator<MisjaUpdateDto> validator)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Unit> Handle(ZaktualizujMisjeCommand request, CancellationToken cancellationToken)
        {
            Misja misjaEntity = await _focusOnFlyingContext.Misje
                .Include(x => x.MisjeDrony)
                .SingleAsync(x => x.Id == request.Id);
            var misja = _mapper.Map<MisjaUpdateDto>(misjaEntity);

            request.Patch.ApplyTo(misja);

            ValidationResult validationResult = await _validator.ValidateAsync(misja);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToList());
            }

            _mapper.Map(misja, misjaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
