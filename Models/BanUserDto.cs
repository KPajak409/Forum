using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class BanUserDto
    {
        public string Reason { get; set; }
        public int Days { get; set; }
    }
}
