using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Anch.Demo.Web
{
    /// <summary>
    /// 自定义异常过滤器
    /// </summary>
    public class CustomApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UserFriendlyException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                var response = new AjaxResponse()
                {
                    Success = false,
                    Error = new ErrorInfo { Message = context.Exception.Message }
                };

                context.Result = new UserFriendlyResult(response);
                context.ExceptionHandled = true;
            }
        }
    }

    public class UserFriendlyResult : ObjectResult
    {
        public UserFriendlyResult(object value)
            : base(value)
        {
            StatusCode = (int)HttpStatusCode.OK;
        }
    }
}