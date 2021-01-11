using FocusOnFlying.Application.Common.Models;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Login { get; }
        string Id { get; }
        Task<UserDto> PobierzInformacje();
    }
}
