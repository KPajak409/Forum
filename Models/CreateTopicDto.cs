using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class CreateTopicDto
    {
        [Required]
        [MaxLength(100)]
        public string Content { get; set; }

    }
}
