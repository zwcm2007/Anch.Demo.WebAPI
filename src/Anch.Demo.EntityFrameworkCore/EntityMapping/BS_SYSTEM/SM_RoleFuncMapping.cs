using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// SM_RoleFuncMapping
    /// </summary>
    public class SM_RoleFuncMapping : EntityTypeConfigurationBase<SM_RoleFunc>
    {
        protected override string TableName => "SM_RoleFuncs";

        public override void Configure(EntityTypeBuilder<SM_RoleFunc> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => new { e.FuncID, e.RoleCode });
            // 关系
            builder.HasOne(e => e.Func).WithMany(u => u.RoleFuncs).HasForeignKey(e => e.FuncID);
            builder.HasOne(e => e.Role).WithMany(u => u.RoleFuncs).HasForeignKey(e => e.RoleCode);
            // 属性
            builder.Property(e => e.FuncID).HasColumnName("vFuncID").IsRequired();
            builder.Property(e => e.RoleCode).HasColumnName("vRoleCode").IsRequired();
        }
    }
}