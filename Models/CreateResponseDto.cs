using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class CreateResponseDto
    {
        [Required]
        public string Content { get; set; }
    }
}
