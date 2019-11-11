using Abp.ObjectMapping;
using BaiTeng.POS.EntityFrameworkCore;
using System.Linq;

namespace BaiTeng.POS.Application
{
    /// <summary>
    /// 账号应用服务
    /// </summary>
    public class TestAppService : PosAppServiceBase, ITestAppService
    {
        private readonly ISqlExecuter<BusinessDbContext> _sqlExecuter;

        public TestAppService(
            ISqlExecuter<BusinessDbContext> sqlExecuter,
            IObjectMapper objectMapper)
            : base(objectMapper)
        {
            _sqlExecuter = sqlExecuter;
        }

        public void GetTest()
        {
            var aaa = _sqlExecuter.CurrentDbContext.V_CARSETS.ToList();

            //var bbbs = _objectMapper.Map<List<AAADto>>(aaas);

            //throw new UserFriendlyException($"出错啦");

            //var a = _sqlExecuter.QueryList<aaaDto>("select sptm from aaa");

            //var c = _sqlExecuter.ExecuteSqlCommand("delete from aaa where sptm = '17a78802030001'");

            //var b = _sqlExecuter.ExecuteSqlCommand("delete from aaa where sptm = '6683101020001'");

            //using (var conn = _provider.GetDbContext().Database.GetDbConnection())
            //{
            //    //conn.Open();
            //    //using (var cmd = conn.CreateCommand())                                                                                                                                                                                                                                                                                                                                                              fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy99999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999edbbbbbbbbbbbbbbrgefhrbvhdbvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbfhtfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffyttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt6666666666666666666666666666666667ywfigeduhcvfyhhhhhdby hrcv c hjuv bgvggggtp['ijnuhbvgf cdnftiygftdrcfghnjop,ml.ovfd;llk,;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;jjhn sx'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''dfdjuaAzs
            //    //{
            //        var cmd = conn.CreateCommand();
            //        cmd.Transaction = _provider.GetDbContext().Database.CurrentTransaction.GetDbTransaction();
            //        cmd.CommandText = "select count(*) from aaa";
            //        var result = cmd.ExecuteScalar().ToString();
            //    //}
            //}

            //var tableList = _provider.GetDbContext().Query<aaa>()
            //        .FromSql("select sptm as Id, * from aaa")
            //        .AsNoTracking()
            //        .ToList();

            //var tableList = _provider.GetDbContext().Set<aaa>()
            //     .FromSql("select sptm as Id, * from aaa")
            //     .AsNoTracking()
            //     .ToList();

            //var tableList = _provider.GetDbContext().Set<aaa>()
            //                 .FromSql("select sptm as Id, * from aaa")
            //                 .AsNoTracking()
            //                 .ToList();
        }
    }
}