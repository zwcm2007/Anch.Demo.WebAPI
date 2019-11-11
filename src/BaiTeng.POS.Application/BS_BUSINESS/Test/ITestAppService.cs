using Abp.Application.Services;
using System.Threading.Tasks;

namespace BaiTeng.POS.Application
{
    public interface ITestAppService : IApplicationService
    {
        void GetTest();

        //Task<LoginOutput> CheckLogin(LoginInput input);

        //SearchUsersOutput SearchAllUsers(SearchUsersInput input);

        //void AddRole(AddRoleInput input);

        //void UpdateRole(int id, UpdateRoleInput input);

        //void RemoveRole(int id);
        //void AddUser(AddUserInput input);
    }
}