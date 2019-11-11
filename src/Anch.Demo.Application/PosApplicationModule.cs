using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Anch.Demo.Core;

namespace Anch.Demo.Application
{
    [DependsOn(typeof(DemoCoreModule),
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