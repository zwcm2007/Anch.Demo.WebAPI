using Abp.EntityFrameworkCore;
using BaiTeng.POS.Core;
using BaiTeng.POS.IRepositories;

namespace BaiTeng.POS.EntityFrameworkCore
{
    /// <summary>
    /// user repository
    /// </summary>
    public class UserRepository : SystemRepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(IDbContextProvider<SystemDbContext> dbContextProvider)
               : base(dbContextProvider)
        {
        }




        //public List<Person> FindPersonsByName(string name)
        //{
        //    return Context.Database.SqlQuery<Person>("select * from Persons").ToList();

        //    //return GetAll().Where(p => p.Name.Contains(name)).ToList();
        //}

        //public async Task<List<Person>> FindPersonsByNameAsync(string name)
        //{
        //    return await GetAll().Where(p => p.Name.Contains(name)).ToListAsync();
        //}

        //public List<Person> GetBy()
        //{
        //    var sql = GetAll().Include(p => p.Tasks)
        //        .Where(p => p.Name == "fjq")

        //        //.GroupBy(p => p.Id)
        //        //.Select(g => new
        //        //{
        //        //    id = g.Key,
        //        //    name = g.Max(p => p.Name),
        //        //    name2 = g.Max(p => p.Tasks.AsQueryable().Select(t => t.Name))
        //        //})
        //        .ToString();

        //    return null;
        //}
    }
}