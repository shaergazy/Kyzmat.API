using Kyzmat.BLL.DTO;
using Kyzmat.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kyzmat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Authenticates a user with username and password.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
                return Unauthorized(new { message = "Invalid login or password" });

            var result = await _signInManager.PasswordSignInAsync(
                user,
                dto.Password,
                isPersistent: true,
                lockoutOnFailure: true 
            );

            if (result.Succeeded)
            {
                return Ok(new { message = "Login successful" });
            }
            if (result.IsLockedOut)
            {
                return Unauthorized(new { message = "Account locked due to multiple failed attempts" });
            }

            return Unauthorized(new { message = "Invalid login or password" });
        }

        /// <summary>
        /// Logs out the currently authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }
    }
}
