using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// V_CARSETMapping
    /// </summary>
    public class V_CARSETMapping : EntityTypeConfigurationBase<V_CARSET>
    {
        public override void Configure(EntityTypeBuilder<V_CARSET> builder)
        {
            base.Configure(builder);
            //
            // 属性
            builder.HasNoKey();
            builder.ToView("V_CARSET");
            builder.Property(q => q.LBDM).HasColumnName("LBDM");
            builder.Property(q => q.LBMC).HasColumnName("LBMC");
        }
    }
}