using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace wypozyczalnia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Wymaga autoryzacji
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProtectedData()
        {
            return Ok(new { message = "This is protected data." });
        }
    }
}
