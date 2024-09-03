using System.ComponentModel.DataAnnotations;

namespace MaLacoste_Footwear.Models.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
