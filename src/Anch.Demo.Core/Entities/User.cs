﻿using Abp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Anch.Demo.Core
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : Entity<int>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; } = "123456";

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string FactName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// UserRoles
        /// </summary>
        public virtual IList<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword"></param>
        public void ChangePassword(string newPassword)
        {
            if (newPassword == Password)
            {
                throw new InvalidOperationException("新密码和原密码相同");
            }
            Password = newPassword;
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="newPassword"></param>
        public bool CheckPassword(string password)
        {
            if (Password != password)
            {
                return false;
            }
            return true;
        }
    }
}