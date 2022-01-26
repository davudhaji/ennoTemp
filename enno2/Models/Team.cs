using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace enno2.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250),Required]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }

        public string Position { get; set; }
    }
}
