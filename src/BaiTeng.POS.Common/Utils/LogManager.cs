using Abp.Dependency;
using Abp.Domain.Repositories;
using GisqRealEstate.MaintainWeb.Core;
using Microsoft.Extensions.Configuration;

namespace GisqRealEstate.MaintainWeb.Common
{
    /// <summary>
    /// 操作日志管理器
    /// add by fengjq 20190117
    /// </summary>
    public class LogManager : ILogManager, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<LOGIN_LOG, decimal> _loginLogRepo;
        private readonly IRepository<OPERATE_LOG, decimal> _operLogRepo;

        public LogManager(IConfiguration configuration,
            IRepository<LOGIN_LOG, decimal> loginLogRepo,
            IRepository<OPERATE_LOG, decimal> operLogRepo)
        {
            _configuration = configuration;
            _loginLogRepo = loginLogRepo;
            _operLogRepo = operLogRepo;
        }

        /// <summary>
        /// 记录新增的数据
        /// </summary>
        public void Record_A(string ywh, SHTM? xgtm, SHSX xgsx, string xzzd, string xzzdz, string xgr, string qlbsm, string qlbm, string dybsm, string dybm)
        {
            var log = new OPERATE_LOG
            {
                YWH = ywh,
                XGTM = xgtm,
                XGTMMC = xgtm?.GetDescription(),
                XGSX = xgsx,
                XGZD = xzzd,
                XGZDH = xzzdz,
                XGR = xgr,
                DYBM = dybm,
                DYBSM = dybsm,
                QLBM = qlbm,
                QLBSM = qlbsm
            };
            _operLogRepo.Insert(log);
        }

        /// <summary>
        /// 记录更新的数据
        /// </summary>
        public void Record_U(string ywh, SHTM? xgtm, SHSX xgsx, string xgzd, string xgzdq, string xgzdh, string xgr, string qlbsm, string qlbm, string dybsm, string dybm, string bz = null, string xgtmmc = null)
        {
            var log = new OPERATE_LOG
            {
                YWH = ywh,
                XGTM = xgtm,
                XGTMMC = xgtm == null ? xgtmmc : xgtm.GetDescription(),
                XGSX = xgsx,
                XGZD = xgzd,
                XGZDQ = xgzdq,
                XGZDH = xgzdh,
                XGR = xgr,
                DYBM = dybm,
                DYBSM = dybsm,
                QLBM = qlbm,
                QLBSM = qlbsm,
                BZ = bz,
            };
            _operLogRepo.Insert(log);
        }

        /// <summary>
        /// 记录更新的数据
        /// </summary>
        public void Record_U(string ywh, SHTM? xgtm, SHSX xgsx, string gxnr, string xgr, string qlbsm, string qlbm, string dybsm, string dybm)
        {
            Record_D(ywh, xgtm, xgsx, gxnr, xgr, qlbsm, qlbm, dybsm, dybm);
        }


        /// <summary>
        /// 记录删除的数据
        /// </summary>
        public void Record_D(string ywh, SHTM? xgtm, SHSX xgsx, string scnr, string xgr, string qlbsm, string qlbm, string dybsm, string dybm)
        {
            var log = new OPERATE_LOG
            {
                YWH = ywh,
                XGTM = xgtm,
                XGTMMC = xgtm?.GetDescription(),
                XGSX  = xgsx,
                XGZDH = scnr,
                XGR = xgr,
                DYBM = dybm,
                DYBSM = dybsm,
                QLBM = qlbm,
                QLBSM = qlbsm
            };
            _operLogRepo.Insert(log);
        }

        /// <summary>
        /// 记录登录信息
        /// </summary>
        public void Record_L(string userName, string loginIP)
        {
            var log = new LOGIN_LOG
            {
                USERNAME = userName,
                LOGIN_IP = loginIP
            };
            _loginLogRepo.Insert(log);
        }
    }

    public interface ILogManager
    {
        void Record_A(string ywh, SHTM? xgtm, SHSX xgsx, string xzzd, string xzzdz, string xgr, string qlbsm, string qlbm, string dybsm, string dybm);

        void Record_U(string ywh, SHTM? xgtm, SHSX xgsx, string xgzd, string xgzdq, string xgzdh, string xgr, string qlbsm, string qlbm, string dybsm, string dybm, string bz = null, string xgtmmc = null);

        void Record_U(string ywh, SHTM? xgtm, SHSX xgsx, string gxnr, string xgr, string qlbsm, string qlbm, string dybsm, string dybm);

        void Record_D(string ywh, SHTM? xgtm, SHSX xgsx, string scnr, string xgr, string qlbsm, string qlbm, string dybsm, string dybm);

        void Record_L(string userName, string loginIP);
    }
}