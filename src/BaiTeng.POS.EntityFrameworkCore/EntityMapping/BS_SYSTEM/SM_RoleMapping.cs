using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BaiTeng.POS.Core;
using Microsoft.EntityFrameworkCore;

namespace BaiTeng.POS.EntityFrameworkCore
{
    /// <summary>
    /// SM_RoleMapping
    /// </summary>
    public class SM_RoleMapping : EntityTypeConfigurationBase<SM_Role>
    {
        protected override string TableName => "SM_Role";

        public override void Configure(EntityTypeBuilder<SM_Role> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => e.Id);
            // 关系
            // 属性
            builder.Property(e => e.Id).HasColumnName("vRoleCode");
            builder.Property(e => e.RoleName).HasColumnName("vRoleName").IsRequired();
        }
    }
}