using AutoMapper;
using BlazorBookHub.Server.Interfaces;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookHub.Server.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName!,
                Email = u.Email!
            }).ToList();

            return userDtos;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.AvatarUrl = dto.AvatarUrl;
            user.BirthDate = dto.BirthDate;
            user.Gender = dto.Gender;

            user.UserName = dto.Email; 

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<int> GetCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }
    }
}
