using Anch.Demo.Application;
using Microsoft.AspNetCore.Mvc;

namespace Anch.Demo.Web.Controllers
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    [ApiVersion("1.0")]
    //[Authorize]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        //GET api/v1/user
        [HttpGet()]
        public SearchUsersOutput Get([FromQuery] SearchUsersInput input)
        {
            var output = _userAppService.SearchUsers(input);
            return output;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        // POST api/v1/user
        [HttpPost()]
        public void Post([FromBody] AddUserInput input)
        {
            _userAppService.AddUser(input);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        // PUT api/v1/user/id
        [HttpPut()]
        public void Put(int id, [FromBody] string password)
        {
            _userAppService.ChangePassword(id, password);
        }
    }
}