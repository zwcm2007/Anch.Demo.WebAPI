using System;
using System.ComponentModel;
using System.Linq;

namespace GisqRealEstate.MaintainWeb.Common
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 转成整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// 转成值字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToValStr(this Enum value)
        {
            return Convert.ToInt32(value).ToString();
        }


        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string GetDescription(this Enum value)
        {
            var memberInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();   //获取成员
            if (memberInfo != null)
            {
                //获取描述特性
                var attr = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description; //返回当前描述
                }
            }
            return value.ToString();
        }
    }
}