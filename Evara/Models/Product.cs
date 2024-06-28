using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evara.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        [ValidateNever]
        public Brand Brand { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
