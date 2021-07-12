using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class BlackList
    {
        private int Id { get; set;}
        private int ModId { get; set;}
        private int UserId { get; set;}

        public string Description { get; set;}
        public DateTime ExpireDate { get; set;}
        public DateTime AcquireDate { get; set;}
    }
}
