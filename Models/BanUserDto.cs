using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class BanUserDto
    {
        public int? Id { get; set; }
        public string Reason { get; set; }
        public int Days { get; set; }
        public int ModId { get; set; }
    }
}
