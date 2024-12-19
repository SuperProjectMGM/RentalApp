using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Repositories;
using wypozyczalnia.Server.Interfaces;
using wypozyczalnia.Server.Repositories;

namespace wypozyczalnia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthInterface _authRepository;
        public AuthController(UserManager<IdentityUser> userManager, IAuthInterface userRepository)
        private readonly IAuthInterface _authRepository;
        public AuthController(UserManager<IdentityUser> userManager, IAuthInterface userRepository)
        {
            _userManager = userManager;
            _authRepository = userRepository;
            _authRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authRepository.CreateNewUser(model);
            foreach (var error in result.Errors)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                return BadRequest($"User creation failed: {errorMessages}");
                var errorMessages = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                return BadRequest($"User creation failed: {errorMessages}");
            }
            return Ok();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
            {
                return BadRequest(ModelState);
            }
            var token = await _authRepository.CheckLogin(model);
            return token == null ? Unauthorized() : Ok(new { Token = token});
            }
            var token = await _authRepository.CheckLogin(model);
            return token == null ? Unauthorized() : Ok(new { Token = token});
        }
    }
}
