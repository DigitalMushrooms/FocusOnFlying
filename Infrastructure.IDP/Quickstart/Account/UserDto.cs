using System.Collections.Generic;
using System.Security.Claims;

namespace FocusOnFlying.Infrastructure.IDP.Quickstart.Account
{
    public class UserDto
    {
        public string SubjectId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
