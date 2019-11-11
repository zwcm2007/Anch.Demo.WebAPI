using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GisqRealEstate.MaintainWeb.Common
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 移除权利人名称
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string RemoveQlrName(this string value, string name)
        {
            if (value == null) return null;

            var sb = new StringBuilder();

            var names = value.Split('、').ToList();

            foreach (var item in names)
            {
                if (!item.Contains(name))
                {
                    sb.Append(item).Append('、');
                }
            }
            return sb.ToString().TrimEnd('、');
        }

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