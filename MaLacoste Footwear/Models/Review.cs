using System.ComponentModel.DataAnnotations;

namespace MaLacoste_Footwear.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
    }
}
