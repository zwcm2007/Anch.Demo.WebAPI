using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// UserMapping
    /// </summary>
    public class UserMapping : EntityTypeConfigurationBase<User>
    {
        protected override string TableName => "User";

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => e.Id);
            // 关系
            // 属性
            builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(e => e.UserName).IsRequired();
            builder.Property(e => e.Password).IsRequired();
        }
    }
}