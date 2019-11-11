using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BaiTeng.POS.Web.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize]

    public class BaseController : AbpController
    {
        public BaseController()
        {

        }

    }
}