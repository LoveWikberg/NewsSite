using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.CustomRequirements
{
    public class PublishRightsRequirements : IAuthorizationRequirement
    {
        public string[] PublishRights { get; private set; }

        public PublishRightsRequirements(params string[] publishRights)
        {
            PublishRights = publishRights;
        }
    }
}
