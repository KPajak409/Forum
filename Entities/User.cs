using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Entities
{
    public class User
    {
        public int Id { get; set;}
        public string Email { get; set; }
        public string HashedPassword { get; set;}
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual List<Topic> Topics { get; set; }
        
    }
}
