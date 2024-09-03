using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Identity;

namespace MaLacoste_Footwear.Infrastructure
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        private static readonly string[] _allowedDomains = new[] { "lacoste.co.za", "motloung.com", "gatsheni.com" };
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            if (_allowedDomains.Any(domain=>user.Email.ToLower().EndsWith($"@{domain}")))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailDomainError",
                    Description = "Only lacoste.co.za , motloung.com , gatsheni.com email addresses are allowed"
                }));
            }
        }
    }
}
