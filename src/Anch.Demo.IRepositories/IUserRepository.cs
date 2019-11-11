using Abp.Domain.Repositories;
using BaiTeng.POS.Core;

namespace BaiTeng.POS.IRepositories
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    public interface IUserRepository : IRepository<User, int>
    {
        ///// <summary>
        ///// 根据姓名查找所有人
        ///// </summary>
        ///// <param name="name">姓名</param>
        ///// <returns></returns>
        //List<Person> FindPersonsByName(string name);

        ///// <summary>
        ///// 根据姓名查找所有人
        ///// </summary>
        ///// <param name="name">姓名</param>
        ///// <returns></returns>
        //Task<List<Person>> FindPersonsByNameAsync(string name);

        //List<Person> GetBy();
    }
}