using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Anch.Demo.Application;
using Anch.Demo.Core.Configuration;
using Anch.Demo.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Anch.Demo.Web
{
    [DependsOn(
        typeof(DemoApplicationModule),
        typeof(DemoEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule))]
    public class DemoWebModule : AbpModule
    {
        //private readonly IConfiguration _configuration;
        private readonly IConfigurationRoot _appConfiguration;

        public DemoWebModule(IWebHostEnvironment env/*, IConfiguration configuration*/)
        {
            //_configuration = configuration;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(DemoConsts.ConnectionStringName);
#if DEBUG
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
#endif
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(
                     typeof(DemoApplicationModule).GetAssembly()
                 );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}