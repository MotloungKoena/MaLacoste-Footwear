using Microsoft.AspNetCore.Identity;

namespace MaLacoste_Footwear.Models
{
    public class AppUser : IdentityUser
    {
        public byte[] AvatarImage { get; set; }
    }
}
