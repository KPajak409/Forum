using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }

        public DateTime Date { get; set;}
        public string Content { get; set;}

        public int CategoryId { get; set;}
        public virtual Category Category { get; set;}

        public virtual List<Response> Responses { get; set;}
    }
}
