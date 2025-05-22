using BlazorBookHub.Shared.Models;
using System.Threading.Tasks;

namespace BlazorBookHub.Server.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetUserProfileAsync(string userId);

        Task<bool> UpdateUserProfileAsync(string userId, UpdateUserProfileDto dto);
    }
}
