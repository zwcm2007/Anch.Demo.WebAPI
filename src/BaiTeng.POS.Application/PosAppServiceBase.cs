﻿using Abp.Application.Services;
using Abp.Events.Bus;
using Abp.ObjectMapping;
using Castle.Core.Logging;

namespace BaiTeng.POS.Application
{
    /// <summary>
    /// 应用服务基类
    /// </summary>
    public abstract class PosAppServiceBase : ApplicationService
    {
        //public IEventBus EventBus { get; set; }

        protected readonly IObjectMapper _objectMapper;

        public PosAppServiceBase(IObjectMapper objectMapper = null)
        {
            Logger = NullLogger.Instance;
            _objectMapper = objectMapper;
        }
    }
}