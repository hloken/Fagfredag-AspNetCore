using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreAuthenticationDemo.Controllers
{
    [Route("api/test")]
    public class TestApiController : Controller
    {
        [Authorize]
        public string Get()
        {
            return "I am authorized";
        }
    }
}
