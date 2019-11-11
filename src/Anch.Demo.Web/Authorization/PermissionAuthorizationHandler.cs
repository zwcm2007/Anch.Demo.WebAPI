using GisqRealEstate.MaintainWeb.Application;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace GisqRealEstate.MaintainWeb.Web.Host.Authorization
{
    /// <summary>
    /// 权限授权处理
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IAccountAppService _accountAppService;

        public PermissionAuthorizationHandler(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                var userIdClaim = context.User.FindFirst(c => c.Type == JwtClaimTypes.Id);
                if (userIdClaim != null)
                {
                    if (_accountAppService.CheckPermission(int.Parse(userIdClaim.Value), requirement.Name))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}