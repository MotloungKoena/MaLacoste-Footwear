using System.ComponentModel.DataAnnotations;

namespace MaLacoste_Footwear.Models
{
    public class Gender
    {
        public int Id { get; set; }
        [Display(Name = "Gender")]
        public string Name { get; set; }
    }
}
