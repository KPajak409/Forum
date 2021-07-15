using Forum.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
