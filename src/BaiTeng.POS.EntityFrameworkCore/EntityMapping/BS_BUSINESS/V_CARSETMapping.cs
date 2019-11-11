using BaiTeng.POS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaiTeng.POS.EntityFrameworkCore
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