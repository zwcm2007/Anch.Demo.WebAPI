using Anch.Demo.Application;
using Microsoft.AspNetCore.Mvc;

namespace Anch.Demo.Web.Controllers
{
    /// <summary>
    /// 客户控制器
    /// </summary>
    [ApiVersion("1.0")]
    //[Authorize]
    public class ClientController : BaseController
    {
        private readonly IClientAppService _clientAppService;

        public ClientController(IClientAppService clientAppService)
        {
            _clientAppService = clientAppService;
        }

        /// <summary>
        /// 查询客户
        /// </summary>
        /// <returns></returns>
        // GET api/v1/client
        [HttpGet()]
        public void Get()
        {
            _clientAppService.GetCustomers();
        }


        /// <summary>
        /// 客户注册
        /// </summary>
        /// <returns></returns>
        // POST api/v1/client
        [HttpPost()]
        public void POST([FromBody] AddCustomerInput input)
        {
            _clientAppService.AddCustomer(input);
        }

        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        // PUT: api/v1/client/1
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateCustomerInput input)
        {
            _clientAppService.UpdateCustomer(id, input);
        }


        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/v1/role/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _clientAppService.RemoveCustomer(id);
        }
    }
}