using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Services;

namespace Forum.Controllers
{
    [Route("/api/role")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RoleApiController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleApiController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult GetRoles()
        {
            var roles = _roleService.Get();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public ActionResult GetRoleById([FromRoute] int id)
        {
            var role = _roleService.GetById(id);
            return Ok(role);
        }
        [HttpPost]
        public ActionResult CreateRole([FromBody] string name)
        {
            var roleId = _roleService.Create(name);
            return Created($"/api/role/{roleId}", null);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateRole([FromRoute] int id, [FromBody] string name)
        {
            _roleService.Update(id, name);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteRole([FromRoute] int id)
        {
            _roleService.Delete(id);
            return Ok();
        }
    }
}
