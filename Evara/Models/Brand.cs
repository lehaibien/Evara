using System.ComponentModel.DataAnnotations;

namespace Evara.Models
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }
        public string Name { get; set; }
    }
}
