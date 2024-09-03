using System.ComponentModel.DataAnnotations;

namespace MaLacoste_Footwear.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";

        //If the user checks the Remember Me box, the app
        //uses a persistent cookie to keep the user logged
        //in across multiple sessions.Otherwise, the app
        //uses a session cookie that expires at the end of
        //the session.
        public bool RememberMe { get; set; }
    }
}
