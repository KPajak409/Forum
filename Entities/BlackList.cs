using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class BlackList
    {
        public int Id { get; set;}
        public string Reason { get; set;}
        public DateTime ExpireDate { get; set;}
        public DateTime AcquireDate { get; set;}

        public int? ModId { get; set; }
        public virtual User Mod { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
