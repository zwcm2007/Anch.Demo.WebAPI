using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Anch.Demo.Core;
using Anch.Demo.EntityFrameworkCore;
using GisqRealEstate.MaintainWeb.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anch.Demo.Application
{
    /// <summary>
    /// 用户应用服务
    /// </summary>
    public class UserAppService : DemoAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, int> _userRepo;
        private readonly IRepository<Role, string> _roleRepo;
        private readonly ISqlExecuter<DemoDbContext> _sqlExecuter;

        public UserAppService(IRepository<User, int> userRepo,
            IRepository<Role, string> roleRepo,
            ISqlExecuter<DemoDbContext> sqlExecuter,
            IObjectMapper objectMapper)
            : base(objectMapper)
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
            _sqlExecuter = sqlExecuter;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        public void AddUser(AddUserInput input)
        {
            User user = _objectMapper.Map<User>(input);
     
            _userRepo.InsertAndGetId(user);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="newPwd">新密码</param>
        /// <param name=""></param>
        public void ChangePassword(int userId, string newPwd)
        {
            var user = _userRepo.Get(userId);

            user.ChangePassword(newPwd);

            _userRepo.Update(user);
        }

        #region 登录验证

        public async Task<LoginOutput> CheckLogin(LoginInput input)
        {
            var output = new LoginOutput();

            var user = await _userRepo.FirstOrDefaultAsync(u => u.UserName == input.UserName);
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

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SearchUsersOutput SearchAllUsers(SearchUsersInput input)
        {
            var predicate = PredicateBuilder.True<User>()
             .AndIf(input.UserName != null, u => input.IsExactMatch ? u.UserName == input.UserName : u.UserName.Contains(input.UserName));

            var query = _userRepo.GetAll().Where(predicate)
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