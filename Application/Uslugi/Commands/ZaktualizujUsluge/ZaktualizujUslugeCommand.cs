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

namespace FocusOnFlying.Application.Uslugi.Commands.ZaktualizujUsluge
{
    public class ZaktualizujUslugeCommand : IRequest
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<UslugaUpdateDto> Patch { get; set; }
    }

    public class ZaktualizujUslugeCommandHandler : IRequestHandler<ZaktualizujUslugeCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;
        private readonly IMapper _mapper;
        private readonly IValidator<UslugaUpdateDto> _validator;

        public ZaktualizujUslugeCommandHandler(
            IFocusOnFlyingContext focusOnFlyingContext,
            IMapper mapper,
            IValidator<UslugaUpdateDto> validator)
        {
            _focusOnFlyingContext = focusOnFlyingContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Unit> Handle(ZaktualizujUslugeCommand request, CancellationToken cancellationToken)
        {
            Usluga uslugaEntity = await _focusOnFlyingContext.Uslugi
                .Include(x => x.StatusUslugi)
                .SingleAsync(x => x.Id == request.Id);

            var usluga = _mapper.Map<UslugaUpdateDto>(uslugaEntity);

            request.Patch.ApplyTo(usluga);

            ValidationResult validationResult = await _validator.ValidateAsync(usluga);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToList());
            }

            _mapper.Map(usluga, uslugaEntity);

            await _focusOnFlyingContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
