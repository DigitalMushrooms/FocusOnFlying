using FluentValidation;

namespace FocusOnFlying.Application.Klienci.Commands.UtworzKlienta
{
    class UtworzKlientaCommandValidator : AbstractValidator<UtworzKlientaCommand>
    {
        public UtworzKlientaCommandValidator()
        {
            RuleFor(x => x.Imie)
                .NotEmpty()
                .WithMessage("Imię jest polem obowiązkowym.");
            RuleFor(x => x.Nazwisko)
                .NotEmpty()
                .WithMessage("Nazwisko jest polem obowiązkowym.");
        }
    }
}
