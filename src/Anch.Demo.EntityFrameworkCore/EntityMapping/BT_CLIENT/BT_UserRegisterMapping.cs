using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// BT_UserRegisterMapping
    /// </summary>
    public class BT_UserRegisterMapping : EntityTypeConfigurationBase<BT_UserRegister>
    {
        public override void Configure(EntityTypeBuilder<BT_UserRegister> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => e.Id);
            // 关系
            // 属性
            builder.Property(e => e.UserID).IsRequired();
            builder.Property(e => e.CustomerId).IsRequired();
            builder.Property(e => e.RegisterTime).IsRequired();
            builder.Property(e => e.MachineID).IsRequired();
            builder.Property(e => e.FunctionsMOD).IsRequired();
        }
    }
}