using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserProfileDto?> GetUserProfileAsync(string userId);
        Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileDto dto);
        Task<int> GetCountAsync();  
    }
}
