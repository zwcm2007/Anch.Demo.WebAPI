using Abp.Domain.Entities;
using System.Collections.Generic;

namespace BaiTeng.POS.Core
{
    /// <summary>
    /// SM_Function
    /// </summary>
    public class SM_Function : Entity<string>
    {
        public string FuncCode { get; set; }

        public string FuncName { get; set; }

        public virtual IList<SM_RoleFunc> RoleFuncs { get; set; }
    }
}