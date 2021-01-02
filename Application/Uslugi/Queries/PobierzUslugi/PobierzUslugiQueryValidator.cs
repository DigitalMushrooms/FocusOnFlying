using FluentValidation;
using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Common.Models;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Uslugi.Queries.PobierzUslugi
{
    public class PobierzUslugiQueryValidator : AbstractValidator<PobierzUslugiQuery>
    {
        private readonly ICurrentUserService _currentUserService;

        public PobierzUslugiQueryValidator(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;

            RuleFor(x => x)
                .MustAsync(MusiPosiadacOdpowiedniaRole);
        }

        private async Task<bool> MusiPosiadacOdpowiedniaRole(PobierzUslugiQuery query, CancellationToken cancellationToken)
        {
            UserDto user = await _currentUserService.PobierzInformacje();

            if (user.Claims.Any(x => x.Value == "USLUGA_PODGLAD"))
                return true;

            throw new AuthenticationException();
        }
    }
}
