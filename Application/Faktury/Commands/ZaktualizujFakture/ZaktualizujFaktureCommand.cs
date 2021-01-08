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
using ValidationException = FocusOnFlying.Application.Common.Exceptions.ValidationException;

namespace FocusOnFlying.Application.Faktury.Commands.ZaktualizujFakture
{
    public class ZaktualizujFaktureCommand : IRequest
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<FakturaUpdateDto> Patch { get; set; }
    }

    public class ZaktualizujFaktureCommandHandler : IRequestHandler<ZaktualizujFaktureCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IValidator<FakturaUpdateDto> _validator;

        public ZaktualizujFaktureCommandHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IValidator<FakturaUpdateDto> validator)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Unit> Handle(ZaktualizujFaktureCommand request, CancellationToken cancellationToken)
        {
            Faktura fakturaEntity = await _focusOnFlyingContext.Faktury.SingleAsync(x => x.Id == request.Id);
            var faktura = _mapper.Map<FakturaUpdateDto>(fakturaEntity);

            request.Patch.ApplyTo(faktura);

            ValidationResult validationResult = await _validator.ValidateAsync(faktura);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToList());
            }

            _mapper.Map(faktura, fakturaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
