using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// UserRoleMapping
    /// </summary>
    public class UserRoleMapping : EntityTypeConfigurationBase<UserRole>
    {
        protected override string TableName => "User_Role";

        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => new { e.UserID, e.RoleID });
            // 关系
            builder.HasOne(e => e.User).WithMany(u => u.UserRoles).HasForeignKey(e => e.UserID);
            builder.HasOne(e => e.Role).WithMany(r => r.UserRoles).HasForeignKey(e => e.RoleID);
            // 属性
        }
    }
}