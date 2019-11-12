using Abp.Application.Services;
using System.Threading.Tasks;

namespace Anch.Demo.Application
{
    public interface IUserAppService : IApplicationService
    {
        void ChangePassword();

        Task<LoginOutput> CheckLogin(LoginInput input);

        SearchUsersOutput SearchAllUsers(SearchUsersInput input);
    }
}