using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete       
    }
    public class TopicOperationRequirement : IAuthorizationRequirement
    {
        public TopicOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }

        public ResourceOperation ResourceOperation { get;  }
    }
}
