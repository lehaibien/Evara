using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Evara.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
