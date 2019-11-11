using GisqRealEstate.MaintainWeb.EntityFramework;
using System;
using System.Linq;

namespace GisqRealEstate.MaintainWeb.Common
{
    /// <summary>
    /// 数据生成器
    /// </summary>
    public class DataGenerator : IGenerate
    {
        private readonly ISqlExecuter _sqlExecuter;

        public DataGenerator(ISqlExecuter sqlExecuter)
        {
            _sqlExecuter = sqlExecuter;
        }

        public string GreateNewYWH()
        {
            var ywh = $"{DateTime.Now.ToString("yyyyMMdd")}-Z-{GenerateBdcdjSeqBsm()}";
            return ywh;
        }

        public Int64 GenerateBdcdjSeqBsm()
        {
            return _sqlExecuter.SqlQuery<Int64>("select seq_bsm.nextval from dual").Single();
        }

        public Int64 GenerateSjwhSeqBsm()
        {
            return _sqlExecuter.SqlQuery<Int64>("select sjwh.seq_bsm.nextval from dual").Single();
        }

        public string CreateBBH()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}