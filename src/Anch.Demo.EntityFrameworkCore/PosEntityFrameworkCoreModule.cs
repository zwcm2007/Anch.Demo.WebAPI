//using Abp.EntityFramework.Configuration;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using System.Reflection;
using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Castle.MicroKernel.Registration;

namespace Anch.Demo.EntityFrameworkCore
{
    [DependsOn(
        typeof(PosCoreModule),
        typeof(AbpEntityFrameworkCoreModule)
        //typeof(AbpDapperModule)
        )]
    public class PosEntityFrameworkCoreModule : AbpModule
    {
        public PosEntityFrameworkCoreModule()
        {
        }

        public override void Initialize()
        {
            IocManager.IocContainer.Register(
                Component.For<ISqlExecuter<BusinessDbContext>>().ImplementedBy<SqlExecuter<BusinessDbContext>>().LifestyleTransient(),
                Component.For<ISqlExecuter<SystemDbContext>>().ImplementedBy<SqlExecuter<SystemDbContext>>().LifestyleTransient(),
                Component.For<ISqlExecuter<ClientDbContext>>().ImplementedBy<SqlExecuter<ClientDbContext>>().LifestyleTransient()
            );
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //// add by fenqjq 20180921
            //DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(MaintainEntityFrameworkModule).GetAssembly() });
        }

        public override void PostInitialize()
        {
            //if (!SkipDbSeed)
            //{
            //    SeedHelper.SeedHostDb(IocManager);
            //}
        }
    }
}