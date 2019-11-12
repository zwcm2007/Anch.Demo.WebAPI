using Abp.Application.Services.Dto;

namespace Anch.Demo.Application
{
    /// <summary>
    /// 查询用户输入
    /// </summary>
    public class SearchUsersInput : IPagedResultRequest
    {
        public string UserName { get; set; }
        public bool IsExactMatch { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; } = 20;
    }
}