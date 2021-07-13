using Forum.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Topic> Topics { get; set; }
    }
}
