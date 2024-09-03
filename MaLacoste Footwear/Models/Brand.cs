using System.ComponentModel.DataAnnotations;

namespace MaLacoste_Footwear.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Required(ErrorMessage ="Please enter a brand name.")]
        public string BrandName { get; set; }

        //Navigation property
        public List<Sneaker> Sneakers { get; set; }
    }
}
