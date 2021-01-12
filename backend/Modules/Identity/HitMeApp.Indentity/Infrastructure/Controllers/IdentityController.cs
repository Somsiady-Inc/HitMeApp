using System;
using System.Threading.Tasks;
using HitMeApp.Indentity.Contract.Clients;
using HitMeApp.Indentity.Contract.Commands;
using HitMeApp.Indentity.Contract.Queries;
using HitMeApp.Shared.Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;

namespace HitMeApp.Indentity.Infrastructure.Controllers
{
    public class IdentityController : HitMeAppController
    {
        private readonly IIdentityModuleClient _identityModuleClient;

        public IdentityController(IIdentityModuleClient userModuleClient)
        {
            _identityModuleClient = userModuleClient;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _identityModuleClient.Query(new GetUserById(id)));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            var id = await _identityModuleClient.Command(registerUser);
            var routeValuesAndContent = new { id };
            return CreatedAtAction(nameof(Get), routeValuesAndContent, routeValuesAndContent);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangeUserPassword changeUserPassword)
        {
            changeUserPassword.Id = id;
            await _identityModuleClient.Command(changeUserPassword);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginUser loginUser)
        {
            await _identityModuleClient.Command(loginUser);
            return Ok();
        }
    }
}
