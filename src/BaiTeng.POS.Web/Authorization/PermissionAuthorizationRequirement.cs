using Microsoft.AspNetCore.Authorization;

namespace GisqRealEstate.MaintainWeb.Web.Host.Authorization
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 权限名称
        /// </summary>

        public string Name { get; set; }
    }
}