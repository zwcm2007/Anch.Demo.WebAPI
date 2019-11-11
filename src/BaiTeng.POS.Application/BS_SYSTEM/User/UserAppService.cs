using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using BaiTeng.POS.Core;
using BaiTeng.POS.EntityFrameworkCore;
using GisqRealEstate.MaintainWeb.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTeng.POS.Application
{
    /// <summary>
    /// 账号应用服务
    /// </summary>
    public class UserAppService : PosAppServiceBase, IUserAppService
    {
        private readonly IRepository<SM_Account, string> _sm_accountRepo;
        private readonly IRepository<SM_Function, string> _sm_functionRepo;
        private readonly IRepository<SM_Role, string> _sm_roleRepo;
        private readonly IRepository<SM_User, string> _sm_userRepo;
        private readonly ISqlExecuter<SystemDbContext> _sqlExecuter;

        public UserAppService(
            IRepository<SM_Account, string> sm_accountRepo,
            IRepository<SM_Function, string> sm_functionRepo,
            IRepository<SM_Role, string> sm_roleRepo,
            IRepository<SM_User, string> sm_userRepo,
            ISqlExecuter<SystemDbContext> sqlExecuter,
            IObjectMapper objectMapper)
            : base(objectMapper)
        {
            _sm_accountRepo = sm_accountRepo;
            _sm_functionRepo = sm_functionRepo;
            _sm_roleRepo = sm_roleRepo;
            _sm_userRepo = sm_userRepo;
            _sqlExecuter = sqlExecuter;
        }

        #region 登录验证

        public async Task<LoginOutput> CheckLogin(LoginInput input)
        {
            var output = new LoginOutput();

            var user = await _sm_userRepo.FirstOrDefaultAsync(u => u.UserName == input.UserName);
            if (user == null)
            {
                output.Succeeded = false;
                output.Message = "用户名不存在";
                return output;
            }

            if (user.Password != input.Password)
            {
                output.Succeeded = false;
                output.Message = "密码有误";
                return output;
            }

            output.Succeeded = true;
            output.Message = "";
            // 记录登录日志
            //var clientIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //clientIP = clientIP == "::1" ? "127.0.0.1" : clientIP;

            return output;
        }

        #endregion 登录验证

        public void GetTest()
        {
            var cccc = _sm_userRepo.GetAll().ToList();

            //var aab = _sm_accountRepo.GetAll().ToList();

            //var role = _sm_roleRepo.GetAllIncluding(r => r.RoleFuncs).ToList();

            //var list = _sqlExecuter.QueryList<SM_Account>("select * from SM_Account");

            //var list = _sqlExecuter.Query<SM_Account>("select * from SM_Account").ToList();

            //var aaas = _sm_functionRepo.GetAll().ToList();

            //var bbbbs = _sm_accountRepo.GetAll().ToList();
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SearchUsersOutput SearchAllUsers(SearchUsersInput input)
        {
            var predicate = PredicateBuilder.True<SM_User>()
             .AndIf(input.UserName != null, u => input.IsExactMatch ? u.UserName == input.UserName : u.UserName.Contains(input.UserName));

            var query = _sm_userRepo.GetAll().Where(predicate)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var users = query.ToList();

            var output = new SearchUsersOutput()
            {
                Users = _objectMapper.Map<List<UserDto>>(users),
                TotalCount = query.Count()
            };

            return output;
        }
    }
}