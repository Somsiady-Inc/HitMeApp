using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Contract;
using HitMeApp.Users.Contract.Commands;
using HitMeApp.Users.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HitMeApp.Users.Controllers
{
    public class UsersController : HitMeAppController
    {
        private readonly IUserModuleClient _userModuleClient;

        public UsersController(IUserModuleClient userModuleClient)
        {
            _userModuleClient = userModuleClient;
        }

        [HttpGet]
        public async Task<IActionResult> Browse()
            => Ok(await _userModuleClient.Query(new BrowseAllUsers()));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            await _userModuleClient.Command(registerUser);
            return Ok();
        }
    }
}
