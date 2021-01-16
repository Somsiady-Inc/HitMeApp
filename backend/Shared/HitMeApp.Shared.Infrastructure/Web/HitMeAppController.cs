using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HitMeApp.Shared.Infrastructure.Web
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class HitMeAppController : ControllerBase
    {
    }
}
