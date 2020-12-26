using HitMeApp.Shared.Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;

namespace HitMeApp.Users.Controllers
{
    public class UsersController : HitMeAppController
    {
        [HttpGet]
        public IActionResult Browse()
            => Ok(new[] { "User 1", "User 2" });
    }
}
