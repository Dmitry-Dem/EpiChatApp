using EpiChatApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EpiChatApp.Repositories
{
    public interface IUserRepository
    {
        AppUser? GetUser(string userId);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<IdentityResult> UpdateUserAsync(AppUser user);
        Task<AppUser?> FindUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<SignInResult> PasswordSignInAsync(AppUser user, string password, bool isPersistent, bool lockoutOnFailure);
    }
}
