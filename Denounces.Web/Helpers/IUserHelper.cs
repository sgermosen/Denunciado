namespace Denounces.Web.Helpers
{
    using Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUserHelper
    {
         string RemoveCharacters(string param);
         
        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(ApplicationUser user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> UpdateEmailAsync(ApplicationUser user, string email, string token);


        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(ApplicationUser user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(ApplicationUser user, string roleName);

        Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName);

        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);

        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<string> GenerateEmailChangeTokenAsync(ApplicationUser user, string email);

        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password);

        Task<ApplicationUser> AddClaim(ApplicationUser user, Claim claim);

        Task<List<ApplicationUser>> GetAllUsersAsync(long ownerId);
        Task<List<ApplicationUser>> GetAllUsersNoLockoutAsync(long ownerId);

        Task RemoveUserFromRoleAsync(ApplicationUser user, string roleName);

        Task DeleteUserAsync(ApplicationUser user);
        Task<bool> DisabledUserAsync(ApplicationUser user);

    }
}
