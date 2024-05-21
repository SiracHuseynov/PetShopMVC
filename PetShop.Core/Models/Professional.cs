using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Core.Models
{
    public class Professional : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Profession { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
