using Anch.Demo.Application;
using Microsoft.AspNetCore.Mvc;

namespace Anch.Demo.Web.Controllers
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    [ApiVersion("1.0")]
    //[Authorize]
    public class TestController : BaseController
    {
        private readonly ITestAppService _testAppService;

        public TestController(ITestAppService testAppService)
        {
            _testAppService = testAppService;
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        //GET api/v1/test
        [HttpGet()]
        public void Get()
        {
            _testAppService.GetTest();
        }
    }
}