using Abp.Domain.Repositories;
using Abp.EntityFramework;
using GisqRealEstate.MaintainWeb.Core;


namespace GisqRealEstate.MaintainWeb.EntityFramework
{
    /// <summary>
    /// TaskRepository
    /// </summary>
    public class TaskRepository : MaintainRepositoryBase<Task, string>, IRepository<Task, string>
    {
        public TaskRepository(IDbContextProvider<MaintainDbContext> dbContextProvider)
             : base(dbContextProvider)
        {
        }
    }
}