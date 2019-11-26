using System.Collections.Generic;

namespace GisqRealEstate.MaintainWeb.Common
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 数组成员通过分隔符连接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> array, string separator = ",")
        {
            return string.Join(separator, array);
        }
    }
}