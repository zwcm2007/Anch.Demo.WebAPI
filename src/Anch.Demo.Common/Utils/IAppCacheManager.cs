using GisqRealEstate.MaintainWeb.Core;
using System;
using System.Collections.Generic;

namespace GisqRealEstate.MaintainWeb.Common
{
    public interface IAppCacheManager
    {
        Dictionary<string, string> GetEnumDict(string key);

        List<T_ENUMVALUE> GetEnumList(string key);

        void ClearCacheByName(string name);

        string GetXTSZCache(string key);
        XTSZ_Output GetMaintainXtszCache(string key);

        List<XZQ_Dto> GetXZQBMCache(int xzqjb);


        List<T_ENUMVALUE> GetTreeForSjzd();
        void UpdateXtsz(XTSZ_Dto dto);
        void DelXtsz(decimal bsm);
        void AddXtsz(XTSZ_Dto dto);
    }
}