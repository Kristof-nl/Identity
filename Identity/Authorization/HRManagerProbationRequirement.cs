using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Identity.Authorization
{
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public HRManagerProbationRequirement(int probationMonths)
        {
            ProbotionMonths = probationMonths;
        }

        public int ProbotionMonths { get; }
    }

    public class HRManagerProbationRequirementsHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
           if(!context.User.HasClaim(x => x.Type == "EmploymentDate"))
                return Task.CompletedTask;

            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);
            var period = DateTime.Now - empDate;

            if (period.Days > 30 * requirement.ProbotionMonths)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

}
