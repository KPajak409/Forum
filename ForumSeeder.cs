using Forum.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum
{
    public class ForumSeeder
    {
        private readonly ForumDbContext _dbContext;

        public ForumSeeder(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _dbContext.Categories.AddRange(categories);
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Category> GetCategories()
        {
            var author = new User()
            {
                Email = "Kuciapczek@wp.pl",
                Username = "AverageLinuxEnjoyer",
                Name = "Janusz",
                SurName = "Nowak",
                DateOfBirth = DateTime.Parse("1998-02-21"),
            };
            var author2 = new User()
            {
                Email = "CutieWaifu@gmail.com",
                Username = "OwO",
                DateOfBirth = DateTime.Parse("2000-05-19")
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "IT",
                    Description = "Category about software, hardware and anything it related",
                    Topics = new List<Topic>()
                    {
                        new Topic()
                        {
                            Author = author,
                            Date = DateTime.Today,
                            Content = "Windows 11",
                            Responses = new List<Response>()
                            {
                                new Response()
                                {
                                    Author = author,
                                    Date = DateTime.Today,
                                    Content = "Wtf is this, why is there no Ubunto 12 Update but this shitty OS is getting an update, gonna kill Bill Gates myself WhatTheDuck."
                                },

                                new Response()
                                {
                                    Author = author2,
                                    Date = DateTime.Today,
                                    Content = "Yumi is op on Windows 11.",
                                }
                            }
                        },
                        new Topic()
                        {
                            Author = author,
                            Date = DateTime.Today,
                            Content = "Why Linux is better then Windows",
                        }
                    }

                },
                new Category()
                {
                    Name = "League of Legends",
                    Description = "Everything you need to know about the most popular MOBA game",
                    Topics = new List<Topic>()
                    {
                        new Topic()
                        {
                            Author = author2,
                            Date = DateTime.Today,
                            Content = "Yuumi broken champion",
                            Responses = new List<Response>()
                            {
                                new Response()
                                {
                                    Author = author2,
                                    Date = DateTime.Today,
                                    Content = "Lol yuumi is so broken check this clip"
                                },
                                new Response()
                                {
                                    Author = author,
                                    Date = DateTime.Today,
                                    Content = "Serio, nerf it"
                                }
                            }
                        }
                    }
                }
            };
            return categories;
            
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>
            {
                new Role()
                {
                    Name = "Admin"
                },

                new Role()
                {
                    Name = "Moderator"
                },

                new Role()
                {
                    Name = "User"
                }
            };
            return roles;        
        }
    }
}
