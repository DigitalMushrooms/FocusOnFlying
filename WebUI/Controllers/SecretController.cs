using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/secret")]
    public class SecretController : ControllerBase
    {
        [Route("index")]
        [Authorize]
        public string Index()
        {
            return "sercret message";
        }
    }
}
