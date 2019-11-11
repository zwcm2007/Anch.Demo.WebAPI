using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// SM_AccountMapping
    /// </summary>
    public class SM_AccountMapping : EntityTypeConfigurationBase<SM_Account>
    {
        protected override string TableName => "SM_Account";

        public override void Configure(EntityTypeBuilder<SM_Account> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => e.Id);
            // 关系
            // 属性
            builder.Property(e => e.Id).HasColumnName("vActID");
        }
    }
}