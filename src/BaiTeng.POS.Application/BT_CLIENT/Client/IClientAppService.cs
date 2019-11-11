using Abp.Application.Services;
using System.Collections.Generic;

namespace BaiTeng.POS.Application
{
    public interface IClientAppService : IApplicationService
    {
        List<CustomerDto> GetCustomers();

        void AddCustomer(AddCustomerInput input);

        void UpdateCustomer(int id, UpdateCustomerInput input);

        void RemoveCustomer(int id);
    }
}