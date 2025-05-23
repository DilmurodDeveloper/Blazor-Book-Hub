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
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest(new { message = "This email address has been registered." });

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
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }

            await userManager.AddToRoleAsync(user, "User");

            return Ok(new { message = "The user has successfully registered." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized(new { message = "Incorrect email or password.." });

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Incorrect email or password." });

            var roles = await userManager.GetRolesAsync(user);
            var token = jwtTokenService.GenerateToken(user, roles);

            return Ok(new
            {
                token,
                user.FullName,
                roles,
                message = "You have successfully logged in."
            });
        }
    }
}
