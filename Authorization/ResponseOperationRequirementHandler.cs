using Forum.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Authorization
{
    public class ResponseOperationRequirementHandler : AuthorizationHandler<ResponseOperationRequirement, Response>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResponseOperationRequirement requirement, Response resource)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
                context.Succeed(requirement);

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (resource.AuthorId == int.Parse(userId) ||
                context.User.IsInRole("Moderator") ||
                context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
