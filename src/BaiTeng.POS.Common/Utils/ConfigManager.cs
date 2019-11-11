using Abp.Dependency;
using Abp.UI;
using GisqRealEstate.MaintainWeb.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GisqRealEstate.MaintainWeb.Common.Utils
{
    /// <summary>
    /// 配置管理器
    /// add by fengjq 20190117
    /// </summary>
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration _configuration;
        private readonly List<SYS_MENU> _menus;
        private readonly List<SYS_PERMISSION> _permissions;

        public ConfigManager(IConfiguration configuration,
            IOptions<List<SYS_MENU>> optMenus,
            IOptions<List<SYS_PERMISSION>> optPermissions)
        {
            _configuration = configuration;
            _menus = optMenus.Value;
            _permissions = optPermissions.Value;
        }

        public static IConfigManager Instance
        {
            get
            {
                var a = IocManager.Instance.Resolve<IConfigManager>();
                return a;
            }
        }

        /// <summary>
        /// 菜单配置
        /// </summary>
        public List<BDC_MENU> Menus
        {
            get
            {
                var menus = new List<BDC_MENU>();
                _menus.ForEach(m =>
                {
                    var menu = new BDC_MENU
                    {
                        Id = m.Id,
                        TEXT = m.Text,
                        I18N = m.I18n,
                        ISGROUP = m.IsGroup == 1,
                        LINK = m.Link,
                        ICON = m.Icon,
                        PARENT = m.Parent,
                        SORT = Convert.ToDecimal(m.Sort)
                    };
                    menus.Add(menu);
                });
                return menus;
            }
        }

        /// <summary>
        /// 权限配置
        /// </summary>
        public List<BDC_PERMISSION> Permissons
        {
            get
            {
                var permissons = new List<BDC_PERMISSION>();

                _permissions.ForEach(p =>
                {
                    var permisson = new BDC_PERMISSION
                    {
                        Id = p.Id,
                        CODE = p.Code,
                        NAME = p.Name,
                        PARENT = p.Parent,
                        ISLEAF = p.IsLeaf == 1,
                        MENUS = p.Menus,
                        SORT = p.Sort.ToString()
                    };
                    permissons.Add(permisson);
                });
                return permissons;
            }
        }


        /// <summary>
        /// 字段对应表名配置
        /// </summary>
        /// <param name="tmmc">条目名称</param>
        /// <param name="zdmc">字段名称</param>
        /// <returns></returns>
        public string[] GetTableNamesByField(string tmmc, string zdmc)
        {
            var cfgItemName = $"{tmmc.ToLower()}:{zdmc.ToLower()}";
            var config = _configuration[cfgItemName];
            if (config == null)
            {
                throw new UserFriendlyException($"{cfgItemName}配置项找不到！");
            }

            var tbNames = config.Split(',');
            return tbNames;
        }

        /// <summary>
        /// 字段对应表名配置
        /// </summary>
        /// <param name="shtm"></param>
        /// <param name="fldName"></param>
        /// <returns></returns>
        public string GetMasterTableNameByShtm(SHTM shtm)
        {
            var tbNames = GetTableNamesByField(shtm.ToString(), "_del");
            return tbNames[0];
        }

        /// <summary>
        /// 配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //public string GetValue(string key)
        //{
        //    var config = _configuration[key];
        //    if (config == null)
        //    {
        //        throw new UserFriendlyException($"[{key}]配置项找不到！");
        //    }
        //    return config;
        //}
    }

    /// <summary>
    /// 系统菜单
    /// </summary>
    public class SYS_MENU
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string I18n { get; set; }
        public int IsGroup { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string Parent { get; set; }
        public int Sort { get; set; }
    }

    /// <summary>
    /// 系统权限
    /// </summary>
    public class SYS_PERMISSION
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Parent { get; set; }
        public byte IsLeaf { get; set; }
        public int Sort { get; set; }
        public string Menus { get; set; }
    }
}