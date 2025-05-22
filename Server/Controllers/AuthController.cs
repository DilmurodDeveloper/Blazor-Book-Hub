using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using BlazorBookHub.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBookHub.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    JwtTokenService jwtTokenService
) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                BirthDate = model.BirthDate,
                Gender = model.Gender
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await userManager.AddToRoleAsync(user, "User");

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Unauthorized();

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) return Unauthorized();

            var roles = await userManager.GetRolesAsync(user);
            var token = jwtTokenService.GenerateToken(user, roles);
            return Ok(new { token, user.FullName, roles });
        }
    }
}
