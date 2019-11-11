using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BaiTeng.POS.Core;

namespace BaiTeng.POS.Application
{
    [DependsOn(typeof(PosCoreModule),
       //typeof(MaintainDomainServiceModule),
       //typeof(MaintainCommonModule),
       typeof(AbpAutoMapperModule))]
    public class ArchivesApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(ArchivesApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            // Scan the assembly for classes which inherit from AutoMapper.Profile
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}