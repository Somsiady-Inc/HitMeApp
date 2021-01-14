using System;
using System.Threading.Tasks;
using HitMeApp.Shared.Infrastructure.Web;
using HitMeApp.Users.Contract.Clients;
using HitMeApp.Users.Contract.Commands;
using Microsoft.AspNetCore.Mvc;

namespace HitMeApp.Users.Infrastructure.Controllers
{
    public class UsersController : HitMeAppController
    {
        private readonly IUserModuleClient _userModuleClient;

        public UsersController(IUserModuleClient userModuleClient)
        {
            _userModuleClient = userModuleClient;
        }

        [HttpPut("{id:guid}/personal-info")]
        public async Task<IActionResult> Put(Guid id, ChangePersonalInfo command)
        {
            command.UserId = id;
            var updatedUser = await _userModuleClient.Command(command);
            return Ok(updatedUser);
        }
    }
}
