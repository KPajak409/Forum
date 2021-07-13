using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class Response
    {
        public int Id { get; set;}
        public int? AuthorId { get; set;}
        public virtual User Author { get; set;}

        public DateTime Date {get; set;}
        public string Content { get; set;}

        public int TopicId { get; set;}
        public virtual Topic Topic { get; set;}
    }
}
