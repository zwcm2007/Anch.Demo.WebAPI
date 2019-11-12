using Abp.AutoMapper;
using Anch.Demo.Core;

namespace Anch.Demo.Application
{
    [AutoMapTo(typeof(User))]
    public class AddUserInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string FactName { get; set; }

        /// <summary>
        /// 移动号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
    }
}