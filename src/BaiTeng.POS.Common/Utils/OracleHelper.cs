using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisqRealEstate.MaintainWeb.Common
{
    public static class OracleHelper
    {
        public static string Page(this string sql, int skipCount, int fetchCount)
        {
            var totalCount = skipCount + fetchCount;

            var sql1 = $"select * from (select rownum as rn,temp.* from ({sql})temp where rownum <= {totalCount}) where rn >= {skipCount}";

            return sql1;
        }


        public static string Count(this string sql)
        {

            var sql1 = $"select count(*) from ({sql}) temp";

            return sql1;
        }
    }
}
