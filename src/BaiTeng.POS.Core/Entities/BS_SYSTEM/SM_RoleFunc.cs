using Abp.Domain.Entities;

namespace BaiTeng.POS.Core
{
    /// <summary>
    /// SM_RoleFunc
    /// </summary>
    public class SM_RoleFunc : Entity
    {
        public string RoleCode { get; set; }

        public SM_Role Role { get; set; }

        public string FuncID { get; set; }

        public SM_Function Func { get; set; }
    }
}