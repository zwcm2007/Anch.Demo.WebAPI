using BaiTeng.POS.Application;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BaiTeng.POS.Web.Controllers
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    [ApiVersion("1.0")]
    //[Authorize]
    public class UserController : BaseController
    {
        private readonly IUserAppService _accountAppService;

        public UserController(IUserAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // POST api/v1/user/authenticate
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginInput input)
        {
            LoginOutput ret = await _accountAppService.CheckLogin(input);
            if (!ret.Succeeded) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(PosConsts.Secret);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddHours(12);   ///authTime.AddSeconds(30);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer, "http://localhost:23648"),
                    new Claim(JwtClaimTypes.Id, ret.UserInfo.Id),
                    new Claim(JwtClaimTypes.Name, ret.UserInfo.UserName),
                    //new Claim(JwtClaimTypes.PhoneNumber, ret.UserInfo.telephone)
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    uid = ret.UserInfo.Id,
                    name = ret.UserInfo.UserName,
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            });
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        //GET api/v1/user
        [HttpGet()]
        public SearchUsersOutput Get([FromQuery] SearchUsersInput input)
        {
            var output = _accountAppService.SearchAllUsers(input);
            return output;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        // POST api/v1/user
        //[HttpPost()]
        //public void Post([FromBody] AddUserInput input)
        //{
        //    _accountAppService.AddUser(input);
        //}

        ///// <summary>
        ///// 获取账号信息
        ///// </summary>
        ///// <param name="id">账号Id</param>
        ///// <returns></returns>
        //// GET api/v1/account/1
        //[HttpGet("{id}")]
        //public UserInfoOutput GetUser(string id)
        //{
        //    var output = _accountAppService.GetUserInfo(id);

        //    return output;
        //}
    }
}