using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.CustomRequirements
{
    public class PublishRightsHandler : AuthorizationHandler<PublishRightsRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                 PublishRightsRequirements requirement)
        {
            if (context.User.IsInRole("Administrator"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else if (context.User.IsInRole("Publisher"))
            {
                var userPublishClaims = context.User.Claims.Where(c => c.Type == "PublishRights");
                foreach (var claim in userPublishClaims)
                {
                    foreach (var publishRight in requirement.PublishRights)
                    {
                        if (claim.Value == publishRight)
                        {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                    }
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
