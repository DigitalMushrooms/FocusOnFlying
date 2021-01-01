using System.Collections.Generic;

namespace FocusOnFlying.Application.Common.Models
{
    public class UserDto
    {
        public string SubjectId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
