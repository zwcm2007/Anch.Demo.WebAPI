using Abp.Application.Services;
using Anch.Demo.Core;
using System.Threading.Tasks;

namespace Anch.Demo.Application
{
    /// <summary>
    /// IUserAppService
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        void ChangePassword(string userId, string newPwd);

        void AddUser(AddUserInput input);

        Task<LoginOutput> CheckLogin(LoginInput input);

        SearchUsersOutput SearchAllUsers(SearchUsersInput input);
    }
}