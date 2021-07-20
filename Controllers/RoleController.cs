using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("/api/role")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RoleController
    {
        public ActionResult GetRoles()
        {
            throw new NotImplementedException();
        }

        public ActionResult CreateRole(string name)
        {
            throw new NotImplementedException();
        }

        public ActionResult UpdateRole()
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteRole()
        {
            throw new NotImplementedException();
        }
    }
}
