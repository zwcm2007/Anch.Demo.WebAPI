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
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// UserRoles
        /// </summary>
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}