using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class Topic
    {
        private int Id { get; set;}
        private int Author { get; set;}

        public DateTime Date { get; set;}
        public string Content { get; set;}

        private int CategoryId { get; set;}
        public virtual Category Category { get; set;}

        public List<Response> Responses { get; set;}
    }
}
