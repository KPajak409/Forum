using Forum.Entities;
using Microsoft.AspNetCore.Identity;
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

        public void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            if (!_dbContext.Categories.Any())
                SeedCategories();
        }
        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                Role role = new Role();
                role.Name = "User";
                role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                Role role = new Role();
                role.Name = "Administrator";
                role.Description = "Perform all the operations.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                Role role = new Role();
                role.Name = "Moderator";
                role.Description = "Perform most of the operations.";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                User user = new User();
                user.UserName = "admin";
                user.Email = "admin@localhost";        
                IdentityResult result = userManager.CreateAsync(user, "User123@").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
            if (userManager.FindByNameAsync("moderator").Result == null)
            {
                User user = new User();
                user.UserName = "moderator";
                user.Email = "moderator@localhost";
                IdentityResult result = userManager.CreateAsync(user, "User123@").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Moderator").Wait();
            }
            if (userManager.FindByNameAsync("LinuxEnjoyer").Result == null)
            {
                User user = new User();
                user.Email = "casual@wp.pl";
                user.UserName = "LinuxEnjoyer";
                user.Name = "Janusz";
                user.SurName = "Nowak";
                user.DateOfBirth = DateTime.Parse("1998-02-21");
                IdentityResult result = userManager.CreateAsync(user, "User123@").Result;
                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "User").Wait();
            }
            if (userManager.FindByNameAsync("user").Result == null)
            {
                User user = new User();
                user.Email = "Cutie@gmail.com";
                user.UserName = "Cuuutie";
                user.DateOfBirth = DateTime.Parse("2000-05-19");
                IdentityResult result = userManager.CreateAsync(user, "User123@").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        private void SeedCategories()
        {

            var author = _dbContext.Users.FirstOrDefault(u => u.UserName == "LinuxEnjoyer");
            var author2 = _dbContext.Users.FirstOrDefault(u => u.UserName == "Cuuutie");

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
                                    Content = "What's going on, why is there no Ubunto 12 Update but this OS is getting an update."
                                },

                                new Response()
                                {
                                    Author = author2,
                                    Date = DateTime.Today,
                                    Content = ":(",
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
                                    Content = "It should be nerfed"
                                }
                            }
                        }
                    }
                }
            };
            _dbContext.AddRange(categories);
            _dbContext.SaveChanges();
            
        }
    }
}
