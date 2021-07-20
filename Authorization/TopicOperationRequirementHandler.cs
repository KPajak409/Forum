using Forum.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Authorization
{
    public class TopicOperationRequirementHandler : AuthorizationHandler<TopicOperationRequirement, Topic>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TopicOperationRequirement requirement, Topic resource)
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
