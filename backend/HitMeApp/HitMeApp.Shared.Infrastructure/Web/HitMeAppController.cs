using Microsoft.AspNetCore.Mvc;

namespace HitMeApp.Shared.Infrastructure.Web
{
    [ApiController]
    [Route("[controller]")]
    public abstract class HitMeAppController : ControllerBase
    {
    }
}
