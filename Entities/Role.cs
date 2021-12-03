using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class Role : IdentityRole<int>
    {
        public override int Id { get; set;}
        public override string Name { get; set; }
        public string Description { get; set; }
    }
}
