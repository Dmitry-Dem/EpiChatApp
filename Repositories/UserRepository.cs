using EpiChatApp.Data;
using EpiChatApp.Models;
using EpiChatApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EpiChatApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;

        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        public UserRepository(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<AppUser?> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<SignInResult> PasswordSignInAsync(AppUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
        public async Task<IdentityResult> UpdateUserAsync(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
		public AppUser? GetUser(string userId)
		{
            return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
		}
	}
}
