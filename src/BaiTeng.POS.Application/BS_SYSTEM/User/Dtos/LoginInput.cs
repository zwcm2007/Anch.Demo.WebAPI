using System.ComponentModel.DataAnnotations;

namespace BaiTeng.POS.Application
{
    /// <summary>
    /// 用户登录输入
    /// </summary>
    public class LoginInput
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}