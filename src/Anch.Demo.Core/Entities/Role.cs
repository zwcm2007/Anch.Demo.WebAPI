using Abp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Anch.Demo.Core
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : Entity<string>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}