using System.Security.Claims;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  

namespace BlazorBookHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            return Ok(new UserProfileDto
            {
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                AvatarUrl = user.AvatarUrl,
                BirthDate = user.BirthDate,
                Gender = user.Gender
            });
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UserProfileDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            user.AvatarUrl = model.AvatarUrl;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Profile updated");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName ?? string.Empty,
                    Email = u.Email ?? string.Empty
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
