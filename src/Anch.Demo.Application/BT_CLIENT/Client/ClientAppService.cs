using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Anch.Demo.Core;
using Anch.Demo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anch.Demo.Application
{
    /// <summary>
    /// 客户应用服务
    /// </summary>
    public class ClientAppService : PosAppServiceBase, IClientAppService
    {
        private readonly IRepository<BT_Customer, int> _customerRepo;
        private readonly IRepository<BT_UserRegister, int> _userRegisterRepo;
        private readonly ISqlExecuter<ClientDbContext> _sqlExecuter;

        public ClientAppService(
            IRepository<BT_Customer, int> customerRepo,
            IRepository<BT_UserRegister, int> userRegisterRepo,
            ISqlExecuter<ClientDbContext> sqlExecuter,
            IObjectMapper objectMapper)
            : base(objectMapper)
        {
            _sqlExecuter = sqlExecuter;
            _customerRepo = customerRepo;
            _userRegisterRepo = userRegisterRepo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        public void AddCustomer(AddCustomerInput input)
        {

            var customer = _objectMapper.Map<BT_Customer>(input);

            //var customer = new BT_Customer
            //{
            //    CustomerId = "BK-0004",
            //    CustomerName = input.CustomerName,
            //    CreateDate = DateTime.Now,
            //    OnlineUser = 0,
            //    ServiceUrl = input.ServiceUrl,
            //    SvrBeginDate = DateTime.Now,
            //    SvrEndDate = DateTime.Now,
            //};
            _customerRepo.Insert(customer);

            var userRegister = new BT_UserRegister()
            {
                CustomerId = customer.CustomerId,
                UserID = "fjq",
                RegisterTime = DateTime.Now,
                FunctionsMOD = "POS_PC",
                MachineID = Guid.NewGuid().ToString(),
            };
            _userRegisterRepo.Insert(userRegister);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<CustomerDto> GetCustomers()
        {
            var list1 = _userRegisterRepo.GetAllList();
            var list = _customerRepo.GetAllList();
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        public void RemoveCustomer(int id)
        {
            _customerRepo.Delete(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        public void UpdateCustomer(int id, UpdateCustomerInput input)
        {
            var customer = _customerRepo.GetAll().Where(c => c.Id == id).SingleOrDefault();
            if (customer == null)
            {
                throw new UserFriendlyException($"客户Id:{id}不存在");
            }

            customer.CustomerName = input.CustomerName;
            customer.ServiceUrl = input.ServiceUrl;
        }
    }
}