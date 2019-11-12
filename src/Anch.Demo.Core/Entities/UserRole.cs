using Abp.Domain.Entities;

namespace Anch.Demo.Core
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    public class UserRole : Entity
    {
        public string UserID { get; set; }

        public User User { get; set; }

        public string RoleID { get; set; }

        public Role Role { get; set; }
    }
}