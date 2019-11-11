using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Abp.UI;
using GisqRealEstate.MaintainWeb.Core;
using GisqRealEstate.MaintainWeb.EntityFramework;
using GisqRealEstate.MaintainWeb.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GisqRealEstate.MaintainWeb.Common
{
    /// <summary>
    /// 应用缓存管理
    /// </summary>
    public class AppCacheManager : IAppCacheManager
    {
        private readonly IEnumValueRepository _enumValueRepo;
        private readonly IRepository<T_XTSZ, long> _xtszRepo;
        private readonly IRepository<BDC_MENU, string> _menuRepo;
        private readonly IRepository<BDC_PERMISSION, string> _permissonRepo;
        private readonly ICacheManager _cacheManager;
        private readonly IReadOnlyDatabase _database;

        public AppCacheManager(ICacheManager cacheManager,
            IReadOnlyDatabase database,
            IEnumValueRepository enumValueRepo,
            IRepository<T_XTSZ, long> xtszRepo,
            IRepository<BDC_MENU, string> menuRepo,
            IRepository<BDC_PERMISSION, string> permissonRepo)
        {
            _cacheManager = cacheManager;
            _database = database;
            _enumValueRepo = enumValueRepo;
            _xtszRepo = xtszRepo;
            _menuRepo = menuRepo;
            _permissonRepo = permissonRepo;
        }

        public static IAppCacheManager Instance
        {
            get
            {
                var a = IocManager.Instance.Resolve<IAppCacheManager>();
                return a;
            }
        }

        /// <summary>
        /// 枚举缓存字典
        /// </summary>
        /// <param name="enumName">枚举名字</param>
        /// <returns></returns>
        public Dictionary<string, string> GetEnumDict(string enumName)
        {
            List<T_ENUMVALUE> enums = GetEnumList(enumName);

            var dict = enums?.ToDictionary(e => e.ENUMVALUE, e => e.ENUMMC);

            return dict;
        }

        /// <summary>
        /// 枚举缓存列表 add by fengjq 20180711
        /// </summary>
        /// <param name="enumName">枚举名字</param>
        /// <returns></returns>
        public List<T_ENUMVALUE> GetEnumList(string enumName)
        {
            return _cacheManager.GetCache("EnumsCache").Get(enumName.ToUpper(), () =>
            {
                return _enumValueRepo.GetEnums(enumName);
            });
        }

        /// <summary>
        /// 系统设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetXTSZCache(string key)
        {
            return _cacheManager.GetCache("XTSZCache").Get(key, () =>
            {
                return _xtszRepo.GetAll().Where(s => s.SZDM == key).Select(s => s.SZZ).FirstOrDefault();
            });
        }
        /// <summary>
        /// 获取维护系统的设置项 wanglz 2019年3月5日13:07:35
        /// </summary>
        /// <param name="key">Maintain</param>
        /// <returns></returns>
        public XTSZ_Output GetMaintainXtszCache(string key)
        {
            return _cacheManager.GetCache("MaintainXtszCache").Get(key,() => {
                XTSZ_Output xtszOutput = new XTSZ_Output();
                var xtszList = _xtszRepo.GetAll().Where(s => s.SZDM.StartsWith(key)).ToList();
                xtszOutput.List = xtszList;
                xtszOutput.TotalCount = xtszList.Count();
                return xtszOutput;

            });
        }
       /// <summary>
       /// 更新系统设置项 wanglz 2019年3月5日13:58:42
       /// </summary>
       /// <param name="dto"></param>
        public void UpdateXtsz(XTSZ_Dto dto)
        {
            var xtsz = _xtszRepo.FirstOrDefault(x => x.SZDM == dto.SZDM);
            if (xtsz != null)
                throw new UserFriendlyException($"更新失败。设置项:{dto.SZDM}已存在。");
            T_XTSZ model = _xtszRepo.Get((long)dto.Id);
            model.SZMC = dto.SZMC;
            model.SZDM = dto.SZDM;
            model.SZZ = dto.SZZ;
            _xtszRepo.Update(model);

            ClearCacheByName("MaintainXtszCache");
        }
        /// <summary>
        /// 删除系统设置项 wanglz 2019年3月5日13:58:55
        /// </summary>
        /// <param name="bsm"></param>
        public void DelXtsz(decimal bsm)
        {
            _xtszRepo.Delete((long)bsm);
            ClearCacheByName("MaintainXtszCache");
        }

        public void AddXtsz(XTSZ_Dto dto)
        {
            var xtsz = _xtszRepo.FirstOrDefault(x => x.SZDM == dto.SZDM);
            if (xtsz != null)
                throw new UserFriendlyException($"新增失败。设置项:{dto.SZDM}已存在。");
            _xtszRepo.Insert(new T_XTSZ() { GXSJ = DateTime.Now, REV_ = 1, SZDM = dto.SZDM, SZMC = dto.SZMC, SZZ = dto.SZZ });
            ClearCacheByName("MaintainXtszCache");
        }
        /// <summary>
        /// 行政区编码缓存
        /// </summary>
        public List<XZQ_Dto> GetXZQBMCache(int xzqjb)
        {
            return _cacheManager.GetCache("XZQBMCache").Get(xzqjb, () =>
            {
                //var sql = $"select ''XZQMC,''XZQBM from dual union all select XZQMC,XZQBM from v_xzqbm where xzqjb = {xzqjb}";
                var sql = $" select XZQMC,XZQBM from xzqbm where xzqjb={xzqjb}";
                return _database.SqlQuery<XZQ_Dto>(sql).ToList();
            });
        }

        /// <summary>
        /// 所有菜单缓存 add by fengjq 20180529
        /// </summary>
        /// <returns></returns>
        public List<BDC_MENU> GetAllMenusCache()
        {
            return _cacheManager.GetCache("MenusCache").Get("all", () =>
            {
                return _menuRepo.GetAll().ToList();
            });
        }

        /// <summary>
        /// 所有操作权限缓存 add by fengjq 20180531
        /// </summary>
        /// <returns></returns>
        public List<BDC_PERMISSION> GetAllPermissonsCache()
        {
            return _cacheManager.GetCache("PermissonsCache").Get("all", () =>
            {
                return _permissonRepo.GetAll().ToList();
            });
        }

        /// <summary>
        /// 数据字段树
        /// </summary>
        /// <returns></returns>
        public List<T_ENUMVALUE> GetTreeForSjzd()
        {
            return _cacheManager.GetCache("SjzdCache").Get("all", () =>
            {
                //先获取ENUMCAPTION
                List<string> enumCaption =
                    _enumValueRepo.GetAllList(p => (p.PARENTNODE == null || p.PARENTNODE == "0")).GroupBy(p => p.ENUMCAPTION)
                        .Select(x => x.Key)
                        .ToList();
                List<T_ENUMVALUE> yiji = new List<T_ENUMVALUE>();
                enumCaption.ForEach(p => { yiji.Add(new T_ENUMVALUE() { Id = 0, ENUMVALUE = Guid.NewGuid().ToString(), ENUMMC = p, PARENTNODE = "-99" }); });
                List<T_ENUMVALUE> erji = _enumValueRepo.GetAllList(p => p.ENUMVALUE != p.PARENTNODE);//过滤掉导致死循环的数据
                yiji.AddRange(erji);

                return yiji;
            });
        }

        /// <summary>
        /// 清理缓存
        /// </summary>
        public void ClearCacheByName(string name)
        {
            _cacheManager.GetCache(name).Clear();
        }
    }

    public class XZQ_Dto
    {
        public string XZQBM { get; set; }
        public string XZQMC { get; set; }
    }

    public class XTSZ_Output
    {
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        public IList<T_XTSZ> List { get; set; }
    }

    public class XTSZ_Dto
    {
        public decimal Id { get; set; }
        public string SZMC { get; set; }
        public string SZDM { get; set; }
        public string SZZ { get; set; }
    }
}