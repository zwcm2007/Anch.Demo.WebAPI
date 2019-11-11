using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;
using GisqRealEstate.MaintainWeb.Core;
using GisqRealEstate.MaintainWeb.EntityFramework;

namespace GisqRealEstate.MaintainWeb.Application
{
    /// <summary>
    /// 登录成功事件处理
    /// </summary>
    public class LoginSucceedEventHandler : IEventHandler<LoginSucceedEventData>, ITransientDependency
    {
        private readonly IRepository<LOGIN_LOG, decimal> _loginLogRepo;
        private readonly SqlExecuter _sqlExecuter;

        /// <summary>
        /// ctor
        /// </summary>
        public LoginSucceedEventHandler(IRepository<LOGIN_LOG, decimal> loginLogRepo,
            SqlExecuter sqlExecuter)
        {
            _loginLogRepo = loginLogRepo;
            _sqlExecuter = sqlExecuter;
        }

        public void HandleEvent(LoginSucceedEventData eventData)
        {
            var log = new LOGIN_LOG
            {
                USERNAME = eventData.UserName,
                LOGIN_IP = "111.111.111.111",
                SERVER_IP = "111.111.111.111"

            };
            _loginLogRepo.Insert(log);
        }
    }

    public class LoginSucceedEventData : EventData
    {
        /// <summary>
        /// 用戶ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用戶名
        /// </summary>
        public string UserName { get; set; }
    }
}