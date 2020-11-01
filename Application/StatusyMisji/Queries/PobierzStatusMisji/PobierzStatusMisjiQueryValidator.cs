using FluentValidation;

namespace FocusOnFlying.Application.StatusyMisji.Queries.PobierzStatusMisji
{
    public class PobierzStatusMisjiQueryValidator : AbstractValidator<PobierzStatusMisjiQuery>
    {
        public PobierzStatusMisjiQueryValidator()
        {
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .WithMessage("Nazwa nie może być pusta.");
        }
    }
}
