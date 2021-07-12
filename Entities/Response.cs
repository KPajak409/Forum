using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class Response
    {
        private int Id { get; set;}
        private int Author { get; set;}

        public DateTime Date {get; set;}
        public string Content { get; set;}

        private int TopicId { get; set;}
        public virtual Topic Topic { get; set;}
    }
}
