using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// RoleMapping
    /// </summary>
    public class RoleMapping : EntityTypeConfigurationBase<Role>
    {
        protected override string TableName => "Role";

        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => e.Id);
            // 关系
            // 属性
            builder.Property(e => e.Id).HasColumnName("Id").HasMaxLength(32);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.CreateTime).IsRequired()
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}