using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Claims;

namespace MaLacoste_Footwear.Infrastructure
{
    [HtmlTargetElement("img", Attributes = "profile-user")]
    public class ProfileImageTagHelper : TagHelper
    {
        private UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor; //this will be used to check if the logged in user is the admin in order to display the admin pic

        public ProfileImageTagHelper(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("profile-user")]
        public string UserName { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {
            AppUser user = await _userManager.FindByNameAsync(UserName);

          
            //this first if statement displays the chosen pic by the user.
            if (user is not null && user.AvatarImage is not null && user.AvatarImage.Length > 0)
            {
                string mimeType = "image/jpeg"; // Assuming the image is a JPEG
                string base64 = Convert.ToBase64String(user.AvatarImage);
                string filename = string.Format("data:{0};base64,{1}", mimeType, base64);
                output.Attributes.SetAttribute("src", filename);
            }
            else //this else statement displays the blank profile if the user did not choose any.
            {
                string defaultImagePath = "/images/user.jpg";
                output.Attributes.SetAttribute("src", defaultImagePath);
            }

            output.Attributes.SetAttribute("class", "img-thumbnail rounded-circle");
            output.Attributes.SetAttribute("style", "height:30px");

            //this one code checks if the user logged in is the admin & if yes it displays the admin pic without him choosing one 
            var httpContext = _httpContextAccessor.HttpContext;
            if (IsCurrentUserAdmin(httpContext.User))
            {
                string adminImagePath = "/images/Admin.jpg"; // Path to the admin image
                output.Attributes.SetAttribute("src", adminImagePath);
            }
        }


        //displays admin pic if admin is logged in
        private bool IsCurrentUserAdmin(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated && user.Identity.Name == "Admin";
        }
    }
}

