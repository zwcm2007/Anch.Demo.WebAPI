using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// BT_CustomerMapping
    /// </summary>
    public class BT_CustomerMapping : EntityTypeConfigurationBase<BT_Customer>
    {
        protected override string TableName => "BT_Customers";

        public override void Configure(EntityTypeBuilder<BT_Customer> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => e.Id);
            // 关系

            // 属性
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}