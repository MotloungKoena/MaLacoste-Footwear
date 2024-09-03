using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaLacoste_Footwear.Models
{
    public class Sneaker
    {
        // EF will instruct the database to automatically generate this value, since it is the primary key and it is the first on the list
        public int SneakerId { get; set; }

        [Required(ErrorMessage ="Please enter a sneaker code.")]
        public string Code { get; set; }

        [Required(ErrorMessage ="Please enter a sneaker name.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter a sneaker price.")]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

        public decimal DiscountPercent => .50M; // A discount of 50% is hard-coded for all products
        public decimal DiscountAmount => Price * DiscountPercent;
        public decimal DiscountPrice => Price - DiscountAmount;
    
        //Slung
        public string Slug
        {
            get
            {
                if (Name == null)
                {
                    return "";
                }
                else
                {
                    return Name.Replace(' ', '-');
                }
            }
        }

        [Required(ErrorMessage ="Please select a brand")]
        public int BrandId { get; set; } //foreign key property
        
        //Navigation properties
        public Brand Brand { get; set; }
    }
}
