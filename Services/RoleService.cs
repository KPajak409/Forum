using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface IRoleService
    {
        public void CreateRole(int userId, int roleId);
        public void ChangeRole(int userId, int roleId);
    }
    public class RoleService : IRoleService
    {
        public void CreateRole(int userId, int roleId)
        {

        }
        public void ChangeRole(int userId, int roleId)
        {

        }
    }
}
