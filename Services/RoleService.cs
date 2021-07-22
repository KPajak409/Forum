using Forum.Entities;
using Forum.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> Get();
        Role GetById(int roleId);
        int Create(string roleName);
        void Update(int roleId, string roleName);
        void Delete(int roleId);
    }
    public class RoleService : IRoleService
    {
        private readonly ForumDbContext _dbContext;
        public RoleService(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Role> Get()
        {
            var roles = _dbContext
                .Roles
                .ToList();
            return roles;
        }

        public Role GetById(int roleId)
        {
            var role = _dbContext
                .Roles
                .FirstOrDefault(r => r.Id == roleId);
            if (role is null)
                throw new NotFoundException($"No role with id = {roleId}");

            return role;
        }
        public int Create(string roleName)
        {
            var role = new Role() { Name = roleName };
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();

            return role.Id;
        }
        public void Update(int roleId, string roleName)
        {
            var role = GetById(roleId);
            role.Name = roleName;
            _dbContext.SaveChanges();
        }

        public void Delete(int roleId)
        {
            var role = GetById(roleId);
            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();          
        }
    }
}
