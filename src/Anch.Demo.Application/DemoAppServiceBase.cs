using Abp.Application.Services;
using Abp.Events.Bus;
using Abp.ObjectMapping;
using Castle.Core.Logging;

namespace Anch.Demo.Application
{
    /// <summary>
    /// 应用服务基类
    /// </summary>
    public abstract class DemoAppServiceBase : ApplicationService
    {
        //public IEventBus EventBus { get; set; }

        protected readonly IObjectMapper _objectMapper;

        public DemoAppServiceBase(IObjectMapper objectMapper = null)
        {
            Logger = NullLogger.Instance;
            _objectMapper = objectMapper;
        }
    }
}