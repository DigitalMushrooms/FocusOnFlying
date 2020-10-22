using FluentValidation;
using FocusOnFlying.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Klienci.Commands.UtworzKlienta
{
    public class UtworzKlientaCommandValidator : AbstractValidator<UtworzKlientaCommand>
    {
        private readonly IFocusOnFlyingContext _focusOnFlyingContext;

        public UtworzKlientaCommandValidator(IFocusOnFlyingContext focusOnFlyingContext)
        {
            _focusOnFlyingContext = focusOnFlyingContext;

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
                .MustAsync(IdKrajuMusiIstniecWBazieDanych)
                .WithMessage("IdKraju nie istnieje w bazie danych.");
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
                .Matches("^[0-9]{9}$")
                .When(x => x.Regon.Length == 9, ApplyConditionTo.CurrentValidator)
                .WithMessage("Regon musi składać się z samych cyfr.")
                .Matches("^[0-9]{14}$")
                .When(x => x.Regon.Length == 14, ApplyConditionTo.CurrentValidator)
                .WithMessage("Regon musi składać się z samych cyfr.")
                .Must(RegonMusiPosiadacPoprawnaSumeKontrolna)
                .WithMessage("Regon posiada błędną sumę kontrolną.")
                .When(x => !string.IsNullOrEmpty(x.Pesel));
            RuleFor(x => x.Nip)
                .NotEmpty()
                .WithMessage("Nip jest polem obowiązkowym.")
                .Length(10)
                .WithMessage("Nip musi mieć długość 10 cyfr.")
                .Matches("^[0-9]{10}$")
                .WithMessage("Nip musi składać się z samych cyfr.")
                .Must(NipMusiPosiadacPoprawnaSumeKontrolna)
                .WithMessage("Nip posiada błędną sumę kontrolną.")
                .When(x => !string.IsNullOrEmpty(x.Pesel));
            RuleFor(x => x.NumerPaszportu)
                .NotEmpty()
                .WithMessage("Numer paszportu jest polem obowiązkowym.")
                .WhenAsync(GdyKrajNieJestPolska);
            RuleFor(x => x.NumerTelefonu)
                .NotEmpty()
                .WithMessage("Numer telefonu jest polem obowiązkowym.")
                .When(x => string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.KodPocztowy)
                .NotEmpty()
                .WithMessage("Kod pocztowy jest polem obowiązkowym.");
            RuleFor(x => x.Ulica);
            RuleFor(x => x.NumerDomu)
                .NotEmpty()
                .WithMessage("Numer domu jest polem obowiązkowym.");
            RuleFor(x => x.NumerLokalu);
            RuleFor(x => x.Miejscowosc)
                .NotEmpty()
                .WithMessage("Numer domu jest polem obowiązkowym.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email jest polem obowiązkowym.")
                .EmailAddress()
                .WithMessage("Email jest niepoprawny.")
                .When(x => string.IsNullOrEmpty(x.NumerTelefonu));
        }

        private async Task<bool> GdyKrajNieJestPolska(UtworzKlientaCommand klient, CancellationToken cancellationToken)
        {
            return (await _focusOnFlyingContext.Kraje.SingleOrDefaultAsync(x => x.Id == klient.IdKraju)).Skrot != "PL";
        }

        private async Task<bool> IdKrajuMusiIstniecWBazieDanych(Guid idKraju, CancellationToken cancellationToken)
        {
            return await _focusOnFlyingContext.Kraje.SingleOrDefaultAsync(x => x.Id == idKraju, cancellationToken) != null;
        }

        private bool NipMusiPosiadacPoprawnaSumeKontrolna(string nip)
        {
            List<int> cyfryNipu = nip.ToCharArray().Select(cyfra => int.Parse(cyfra.ToString())).ToList();
            int suma = 6 * cyfryNipu[0] + 5 * cyfryNipu[1] + 7 * cyfryNipu[2] + 2 * cyfryNipu[3] + 3 * cyfryNipu[4] +
                    4 * cyfryNipu[5] + 5 * cyfryNipu[6] + 6 * cyfryNipu[7] + 7 * cyfryNipu[8];
            int sumaKontrolna = suma % 11;

            return sumaKontrolna == cyfryNipu[9];
        }

        private bool RegonMusiPosiadacPoprawnaSumeKontrolna(string regon)
        {
            List<int> cyfryRegonu = regon.ToCharArray().Select(cyfra => int.Parse(cyfra.ToString())).ToList();
            if (regon.Length == 9)
            {
                int suma = 8 * cyfryRegonu[0] + 9 * cyfryRegonu[1] + 2 * cyfryRegonu[2] + 3 * cyfryRegonu[3] + 4 * cyfryRegonu[4] +
                    5 * cyfryRegonu[5] + 6 * cyfryRegonu[6] + 7 * cyfryRegonu[7];
                int sumaKontrolna = suma % 11;

                return sumaKontrolna == cyfryRegonu[8];
            }
            else
            {
                int suma = 2 * cyfryRegonu[0] + 4 * cyfryRegonu[1] + 8 * cyfryRegonu[2] + 5 * cyfryRegonu[3] + 0 * cyfryRegonu[4] +
                    9 * cyfryRegonu[5] + 7 * cyfryRegonu[6] + 3 * cyfryRegonu[7] + 6 * cyfryRegonu[8] + 1 * cyfryRegonu[9] +
                    2 * cyfryRegonu[10] + 4 * cyfryRegonu[11] + 8 * cyfryRegonu[12];
                int sumaKontrolna = suma % 11;

                return sumaKontrolna == cyfryRegonu[13];
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
            int sumaKontrolna = suma % 10;

            return sumaKontrolna == cyfryPeselu[10];
        }
    }
}
