using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace FocusOnFlying.Application.Klienci.Commands.UtworzKlienta
{
    public class UtworzKlientaCommandValidator : AbstractValidator<UtworzKlientaCommand>
    {
        public UtworzKlientaCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Imie)
                .NotEmpty()
                .WithMessage("Imię jest polem obowiązkowym.")
                .When(x => !string.IsNullOrEmpty(x.Pesel));
            RuleFor(x => x.Nazwisko)
                .NotEmpty()
                .WithMessage("Nazwisko jest polem obowiązkowym.")
                .When(x => !string.IsNullOrEmpty(x.Pesel));
            RuleFor(x => x.IdKraju)
                .NotNull()
                .WithMessage("Kraj jest polem obowiązkowym.");
            RuleFor(x => x.Pesel)
                .NotEmpty()
                .WithMessage("Pesel jest polem obowiązkowym.")
                .Length(11)
                .WithMessage("Pesel musi mieć długość 11 cyfr.")
                .Matches("^[0-9]{11}$")
                .WithMessage("Pesel musi składać się z samych cyfr.")
                .Must(PeselMusiPosiadacPoprawnaSumeKontrolna)
                .WithMessage("Pesel posiada błędną sumę kontrolną.")
                .When(x => !string.IsNullOrEmpty(x.Imie));
            RuleFor(x => x.Regon)
                .NotEmpty()
                .WithMessage("Regon jest polem obowiązkowym.")
                .Must(RegonMusiPosiadacOkreslonaDlugosc)
                .WithMessage("Regon musi mieć długość 9 lub 14 cyfr.")
                .Must(RegonMusiPosiadacPoprawnaSumeKontrolna)
                .WithMessage("Regon posiada błędną sumę kontrolną.")
                .When(x => !string.IsNullOrEmpty(x.Pesel));
        }

        private bool RegonMusiPosiadacPoprawnaSumeKontrolna(string regon)
        {
            List<int> cyfryRegonu = regon.ToCharArray().Select(cyfra => int.Parse(cyfra.ToString())).ToList();
            if (regon.Length == 9)
            {
                int suma = 8 * cyfryRegonu[0] + 9 * cyfryRegonu[1] + 2 * cyfryRegonu[2] + 3 * cyfryRegonu[3] + 4 * cyfryRegonu[4] +
                    5 * cyfryRegonu[5] + 6 * cyfryRegonu[6] + 7 * cyfryRegonu[7];
                int resztaZDzielenia = suma % 11;

                return resztaZDzielenia == cyfryRegonu[8];
            }
            else
            {
                int suma = 2 * cyfryRegonu[0] + 4 * cyfryRegonu[1] + 8 * cyfryRegonu[2] + 5 * cyfryRegonu[3] + 0 * cyfryRegonu[4] +
                    9 * cyfryRegonu[5] + 7 * cyfryRegonu[6] + 3 * cyfryRegonu[7] + 6 * cyfryRegonu[8] + 1 * cyfryRegonu[9] +
                    2 * cyfryRegonu[10] + 4 * cyfryRegonu[11] + 8 * cyfryRegonu[12];
                int resztaZDzielenia = suma % 11;

                return resztaZDzielenia == cyfryRegonu[13];
            }
        }

        private bool RegonMusiPosiadacOkreslonaDlugosc(string regon)
        {
            return regon.Length == 9 || regon.Length == 14;
        }

        private bool PeselMusiPosiadacPoprawnaSumeKontrolna(string pesel)
        {
            List<int> cyfryPeselu = pesel.ToCharArray().Select(cyfra => int.Parse(cyfra.ToString())).ToList();
            int suma = 9 * cyfryPeselu[0] + 7 * cyfryPeselu[1] + 3 * cyfryPeselu[2] + 1 * cyfryPeselu[3] + 9 * cyfryPeselu[4] +
                7 * cyfryPeselu[5] + 3 * cyfryPeselu[6] + 1 * cyfryPeselu[7] + 9 * cyfryPeselu[8] + 7 * cyfryPeselu[9];
            int resztaZDzielenia = suma % 10;

            return resztaZDzielenia == cyfryPeselu[10];
        }
    }
}
