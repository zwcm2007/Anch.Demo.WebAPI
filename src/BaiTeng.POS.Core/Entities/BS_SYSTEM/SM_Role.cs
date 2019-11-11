using Abp.Domain.Entities;
using System.Collections.Generic;

namespace BaiTeng.POS.Core
{
    /// <summary>
    /// SM_Role
    /// </summary>
    public class SM_Role : Entity<string>
    {
        public string RoleName { get; set; }

        public string BZ { get; set; }

        public virtual IList<SM_RoleFunc> RoleFuncs { get; set; }
    }
}