using AutoMapper;
using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Services
{
    public class UserService(UserManager<ApplicationUser> userManager, IMapper mapper) : IUserService
    {
        public async Task<UserProfileDto?> GetUserProfileAsync(string userId)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            return mapper.Map<UserProfileDto>(user);
        }

        public async Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileDto dto)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.AvatarUrl = dto.AvatarUrl;
            user.BirthDate = dto.BirthDate;
            user.Gender = dto.Gender;

            user.UserName = dto.Email; 

            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
