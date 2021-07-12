using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class Category
    {
        private int Id { get; set;}

        public string Name { get; set;}
        public string Description { get; set;}

        public List<Topic> Topics { get; set;}
    }
}
