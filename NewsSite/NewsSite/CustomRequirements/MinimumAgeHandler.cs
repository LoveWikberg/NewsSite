using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.CustomRequirements
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                  MinimumAgeRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "Age"))
            {
                var result = int.TryParse(context.User.FindFirst("Age").Value, out int age);
                if (result && age >= requirement.MinimumAge)
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
