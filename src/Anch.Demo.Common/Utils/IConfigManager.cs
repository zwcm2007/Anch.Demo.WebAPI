using GisqRealEstate.MaintainWeb.Core;
using System.Collections.Generic;

namespace GisqRealEstate.MaintainWeb.Common.Utils
{
    public interface IConfigManager
    {
        List<BDC_MENU> Menus { get; }

        List<BDC_PERMISSION> Permissons { get; }

        string[] GetTableNamesByField(string tmmc, string zdmc);

        string GetMasterTableNameByShtm(SHTM shtm);


        //string GetValue(string key);
    }
}