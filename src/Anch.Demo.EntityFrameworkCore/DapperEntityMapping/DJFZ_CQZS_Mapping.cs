using Dapper;
using DapperExtensions.Mapper;
using GisqRealEstate.MaintainWeb.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace GisqRealEstate.MaintainWeb.EntityFramework.DapperEntityMapping
{
    /// <summary>
    /// "登记房证产权证书"实体映射
    /// </summary>
    public class DJFZ_CQZS_Mapping : ClassMapper<DJFZ_CQZS>
    {
        public DJFZ_CQZS_Mapping()
        {
            Table("DJFZ_CQZS");
            Schema("BDCDJ");
            Map(d => d.Id).Column("BSM");

            AutoMap();
        }
    }
}