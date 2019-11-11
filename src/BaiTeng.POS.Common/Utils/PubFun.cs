using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace GisqRealEstate.MaintainWeb.Common
{
    /// <summary>
    /// 公共函数
    /// </summary>
    public class PubFun
    {
        /// <summary>
        /// 针对带列名的json串转换成DataTable
        /// </summary>
        /// <param name="p_strJson"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(String p_strJson)
        {
            if (string.IsNullOrEmpty(p_strJson))
            {
                return null;
            }

            DataTable dtRet = new DataTable();
            ///由于.*不含回车与换行,所以当出现这样字符时,会有错误,要特别注意啊.....
            //String strRxOneRow = "[\\[,]\\{\"(.|\\s)*?\"\\}[\\],]";
            var strRxOneRow = @"\{.*?\}";
            Regex rx = new Regex(strRxOneRow);
            int iNextRowBeginIndex = 0;
            Match match = null;
            //bool isFirstMatch = true;

            do
            {
                match = rx.Match(p_strJson, iNextRowBeginIndex);
                if (match.Success)
                {
                    //isFirstMatch = false;
                    String strOneRow = match.Groups[0].Value;
                    try
                    {
                        AddOneDataRow(ref dtRet, strOneRow);
                    }
                    catch (Exception ex)
                    {
                        //Logger.Log.Error(ex.StackTrace);
                        throw new Exception("生成的行不对");
                    }

                    ///由于上面一行把两行之间的 , 号配到了,所以下次要把上面一次的逗号含在内,故要减1
                    iNextRowBeginIndex = match.Index + strOneRow.Length - 1;
                }
                //else
                //{
                //    if (isFirstMatch)
                //    {
                //        ///只存在一行的时候
                //        AddOneDataRow(ref dtRet, p_strJson);
                //    }
                //}
            } while (match.Success);

            return dtRet;
        }

        /// <summary>
        /// 列值不能含 ","  ":"  [{"   ,{"  "},  "}]
        /// </summary>
        /// <param name="r_dt"></param>
        /// <param name="strOneRow">表中一行的json字符串</param>
        private static void AddOneDataRow(ref DataTable r_dt, string strOneRow)
        {
            if (String.IsNullOrEmpty(strOneRow))
            {
                return;
            }
            //去头," 号不要去掉,后面统一去
            //String strRxHead = "[\\[,]\\{(\")?";
            string strRxHead = "\\{\"?";
            Regex rx = new Regex(strRxHead);
            strOneRow = rx.Replace(strOneRow, "");

            //去尾
            //String strRxLast = "(\")?\\}[\\],]";
            string strRxLast = "\"?\\}";
            rx = new Regex(strRxLast);
            strOneRow = rx.Replace(strOneRow, "");

            Boolean isFirstAddRow = true;
            if (r_dt.Rows.Count > 0)
            {
                isFirstAddRow = false;
            }

            ///正常情况下为     "a":"b","c":"d"
            ///为了防止出现
            ///                 ",":",","c":"d"     "/,":"/,","c":"d"
            ///                 ":":":","c":"d"     "/:":"/:","c":"d"
            ///                 "a":333,"b":44

            #region Exception1

            rx = new Regex(":\",\"");
            strOneRow = rx.Replace(strOneRow, ":\"/,\"");
            rx = new Regex("\",\":");
            strOneRow = rx.Replace(strOneRow, "\"/,\":");
            rx = new Regex(":\":\"");
            strOneRow = rx.Replace(strOneRow, ":\"/:\"");
            rx = new Regex("\":\":");
            strOneRow = rx.Replace(strOneRow, "\"/:\":");
            rx = new Regex(":[^\"]");

            Match match = null;
            int iBegin = 0;
            do
            {
                match = rx.Match(strOneRow, iBegin);
                if (match.Success)
                {
                    strOneRow = strOneRow.Insert(match.Index + 1, "\"");
                    iBegin = match.Index + match.Length;
                }
                else
                {
                    break;
                }
            }
            while (iBegin < strOneRow.Length);

            rx = new Regex("[^\"],");
            iBegin = 0;
            do
            {
                match = rx.Match(strOneRow, iBegin);
                if (match.Success)
                {
                    strOneRow = strOneRow.Insert(match.Index + 1, "\"");
                    iBegin = match.Index + match.Length;
                }
                else
                {
                    break;
                }
            }
            while (iBegin < strOneRow.Length);

            #endregion Exception1

            DataRow dr = null;
            ArrayList strCols = ToSplitStringByString(strOneRow, "\",\"");
            for (int i = 0; i < strCols.Count; ++i)
            {
                ArrayList strOneCol = ToSplitStringByString(strCols[i].ToString(), "\":\"");
                //去掉列名,与值的最前与最后的 " 号
                if (String.IsNullOrEmpty(strOneCol[0].ToString()))
                {
                    continue;
                }

                #region Exception1

                if (strOneCol.Count > 1)
                {
                    if (strOneCol[0].ToString().Equals("/,") || strOneCol[0].ToString().Equals("/:"))
                    {
                        strOneCol[0] = strOneCol[0].ToString().Remove(0, 1);
                    }
                    if (strOneCol[1].ToString().Equals("/,") || strOneCol[1].ToString().Equals("/:"))
                    {
                        strOneCol[1] = strOneCol[1].ToString().Remove(0, 1);
                    }
                }

                #endregion Exception1

                if (strOneCol.Count == 2)
                {
                    ///认为列名,最后的 ,最前的 " 号也被split掉了
                    if (isFirstAddRow)
                    {
                        ///如果不是第一行,则不用添加列,认为前面行把列已经定好了
                        try
                        {
                            ///不管这列存在与否,都先认为它不存在
                            r_dt.Columns.Add(strOneCol[0].ToString().ToUpper());
                        }
                        catch (Exception ex)
                        {
                            //Logger.Log.Error(ex.StackTrace);
                        }
                    }
                    if (i == 0)
                    {
                        ///每一个列,用新建一行,但后面的列,还是在第一个列添加进去的行中加列,所以要用下面的else
                        dr = r_dt.NewRow();
                    }
                    else
                    {
                        dr = r_dt.Rows[r_dt.Rows.Count - 1];
                    }
                    dr[strOneCol[0].ToString()] = strOneCol[1];
                    if (i == 0)
                    {
                        r_dt.Rows.Add(dr);
                    }
                }
            }
        }

        /// <summary>
        /// 把长字符串,以小字符串进行分开,长字符串格式为content&&content&&......&&
        /// 小字符串为&&,最后必须以&&结尾(这是格式).9 7
        /// </summary>
        /// <param name="p_strContent">待折分的长字符串</param>
        /// <param name="p_strSubString">用作分隔符的字符串</param>
        /// <returns></returns>
        private static ArrayList ToSplitStringByString(string p_strContent, string p_strSubString)
        {
            ArrayList al = new ArrayList();
            int iBegin = 0, iEnd = 0;
            if (string.IsNullOrEmpty(p_strContent))
            {
                return al;
            }
            if (p_strContent.LastIndexOf(p_strSubString) != (p_strContent.Length - p_strSubString.Length))
            {
                p_strContent += p_strSubString;
            }
            iEnd = p_strContent.IndexOf(p_strSubString);

            while (iBegin < p_strContent.Length && iEnd > 0 && iEnd != (p_strContent.Length - p_strContent.Length))
            {
                string str = p_strContent.Substring(iBegin, iEnd - iBegin);
                al.Add(str);
                iBegin = iEnd + p_strSubString.Length;
                iEnd = p_strContent.IndexOf(p_strSubString, iBegin);
            }
            return al;
        }

        /// <summary>
        /// MD5加密处理
        /// </summary>
        /// <param name="cleanText">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string cleanText)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] password = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(cleanText));

            return Convert.ToBase64String(password);
        }

        public static string DesEncrypt(string strText)
        {
            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(strText);
            return Convert.ToBase64String(Buffer);
        }

        public static string DesDecrypt(string strText)
        {
            byte[] Buffer = Convert.FromBase64String(strText);
            return ASCIIEncoding.ASCII.GetString(Buffer);
        }

        /// <summary>
        /// 把json搞成xml字符串
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="jObj">json对象</param>
        /// <returns>xml字符串</returns>
        public static string DtoToXmlString(Type t, JObject jObj)
        {
            if (jObj == null) return "";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlElement root = xmlDoc.CreateElement("Table");
            // root.SetAttribute("TableName", tableName);
            xmlDoc.AppendChild(root);

            BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;

            XmlElement child;
            XmlCDataSection cdata;
            var s = from p in jObj.Children() select p;

            foreach (var item in s)
            {
                var pro = (item as JProperty);
                if (pro.Name.ToUpper() == "ID")
                {
                    continue;
                }
                child = xmlDoc.CreateElement("Column");
                child.SetAttribute("Name", pro.Name);
                if (t.GetProperty(pro.Name.ToUpper(), flag) == null)
                {
                    continue;
                }
                child.SetAttribute("Type", GetPropertyType(t.GetProperty(pro.Name.ToUpper(), flag)));
                child.SetAttribute("FullType", t.GetProperty(pro.Name.ToUpper(), flag).PropertyType.FullName);
                cdata = xmlDoc.CreateCDataSection(pro.Value.ToString());
                child.AppendChild(cdata);
                root.AppendChild(child);
            }

            return xmlDoc.InnerXml;
        }

        public static string DtoToXmlString2(Type t, JObject jObj)
        {
            if (jObj == null) return "";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlElement root = xmlDoc.CreateElement("Table");
            // root.SetAttribute("TableName", tableName);
            xmlDoc.AppendChild(root);

            BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;

            XmlElement child;
            XmlCDataSection cdata;
            var s = from p in jObj.Children() select p;

            foreach (var item in s)
            {
                var pro = (item as JProperty);
                if (string.IsNullOrWhiteSpace(pro.Value.ToString()))
                    continue;
                if (pro.Name.ToUpper() == "ID")
                {
                    continue;
                }
                child = xmlDoc.CreateElement("Column");
                child.SetAttribute("Name", pro.Name);
                if (t.GetProperty(pro.Name.ToUpper(), flag) == null)
                {
                    continue;
                }
                child.SetAttribute("Type", GetPropertyType(t.GetProperty(pro.Name.ToUpper(), flag)));
                child.SetAttribute("FullType", t.GetProperty(pro.Name.ToUpper(), flag).PropertyType.FullName);
                cdata = xmlDoc.CreateCDataSection(pro.Value.ToString());
                child.AppendChild(cdata);
                root.AppendChild(child);
            }

            return xmlDoc.InnerXml;
        }

        public static string GetId(JObject jObj)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
            var s = from p in jObj.Children() select p;
            foreach (var item in s)
            {
                var pro = (item as JProperty);

                if (pro.Name.ToUpper() == "ID")
                {
                    return pro.Value.ToString();
                }
            }
            return string.Empty;
        }

        public static string GetPropertyType(PropertyInfo property)
        {
            if (!property.PropertyType.IsGenericType)
            {
                //非泛型
                return property.PropertyType.FullName;
            }
            else
            {
                //泛型Nullable<>
                //Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                //if (genericTypeDefinition == typeof(Nullable<>))
                //{
                //    property.SetValue(obj, string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType)), null);
                //}

                return Nullable.GetUnderlyingType(property.PropertyType).FullName;
            }
        }

        public static void GetEntity(Object obj, string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            XmlNode root = xmlDoc.SelectSingleNode("Table");
            XmlNodeList children = root.SelectNodes("Column");

            BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;

            Type t = obj.GetType();
            foreach (XmlNode singleXmlNode in children)
            {
                var type = Type.GetType(singleXmlNode.Attributes["FullType"].Value, true, true);
                if (type.IsGenericType)
                {
                    t.GetProperty(singleXmlNode.Attributes["Name"].Value.ToUpper(), flag).SetValue(obj, string.IsNullOrEmpty(singleXmlNode.InnerText) ? null : Convert.ChangeType(singleXmlNode.InnerText, Nullable.GetUnderlyingType(type)), null);
                }
                else
                {
                    t.GetProperty(singleXmlNode.Attributes["Name"].Value.ToUpper(), flag).SetValue(obj, Convert.ChangeType(singleXmlNode.InnerText, type));
                }
            }
        }

        public static void GetEntity(Object obj, string xmlString, object keyValue)
        {
            GetEntity(obj, xmlString);

            Type t = obj.GetType();

            t.GetProperty("Id").SetValue(obj, keyValue);
        }

        public static string GetSqlByXml(string tableName, string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString)) return string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            XmlNode root = xmlDoc.SelectSingleNode("Table");
            XmlNodeList children = root.SelectNodes("Column");
            var names = string.Empty;
            var values = string.Empty;
            foreach (XmlNode singleXmlNode in children)
            {
                var type = singleXmlNode.Attributes["Type"].Value;
                var name = singleXmlNode.Attributes["Name"].Value;
                var value = singleXmlNode.InnerText;
                names += $"{name},";
                if (type == "System.DateTime")
                {
                    values += $"to_date('{value}','yyyy-mm-dd hh24:mi:ss'),";
                    continue;
                }
                values += $"'{value}',";
            }
            return $"insert into {tableName}({names.TrimEnd(',')}) values({values.TrimEnd(',')})";
        }

        #region 历史恢复现状XML读取 add by fengjq 20180925

        /// <summary>
        /// 存放权利相关功能的SQL语句的XML
        /// </summary>
        public static string qlDataXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ExtendFun\qlData.xml");//qlData

        private static XmlDocument xmlqlDataDoc = null;

        /// <summary>
        /// 2017-3-27
        /// 权利相关功能中提取的SQL
        /// </summary>
        public static XmlDocument XmlqlDataDoc
        {
            get
            {
                if (xmlqlDataDoc == null)
                {
                    xmlqlDataDoc = new XmlDocument();
                    xmlqlDataDoc.Load(qlDataXml);
                }
                return xmlqlDataDoc;
            }
        }

        /// <summary>
        /// 存放单元相关功能的SQL语句的XML
        /// </summary>
        public static string dyDataXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ExtendFun\dyData.xml");

        private static XmlDocument xmldyDataDoc = null;

        /// <summary>
        /// 2017-3-27
        /// 单元相关功能中提取的SQL
        /// </summary>
        public static XmlDocument XmldyDataDoc
        {
            get
            {
                if (xmldyDataDoc == null)
                {
                    xmldyDataDoc = new XmlDocument();
                    xmldyDataDoc.Load(dyDataXml);
                }
                return xmldyDataDoc;
            }
        }

        #endregion 历史恢复现状XML读取 add by fengjq 20180925

        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="bm">表名</param>
        /// <param name="zdm">字段名</param>
        /// <returns></returns>
        public static string GetPropertyType(string bm, string zdm)
        {
            var t = Type.GetType($"GisqRealEstate.MaintainWeb.Core.{bm},GisqRealEstate.MaintainWeb.Core", true, true);
            var flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
            string propType = PubFun.GetPropertyType(t.GetProperty(zdm.ToUpper(), flag));
            return propType;
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="colname"></param>
        /// <param name="value"></param>
        /// <param name="Coltype"></param>
        /// <returns></returns>
        public static string SqlAddWhere(string sql, string colname, string value, bool sel, string Coltype = "string", bool issize = true)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (Coltype.ToUpper() == "DATE")
                {
                    if (value != "0001/1/1 0:00:00")
                    {
                        if (issize)
                            sql += " AND " + colname + ">=#" + value + "#";
                        else
                            sql += " AND " + colname + "<=#" + value + "#";
                    }
                }
                else if (Coltype.ToUpper() == "INT")
                    sql += " AND " + colname + " = " + value + "";
                else
                {
                    if (sel)
                        sql += " AND " + colname + " = '" + value + "'";
                    else
                        sql += " AND " + colname + " like '%" + value + "%'";
                }
            }
            return sql;
        }

        /// <summary>
        /// 根据表名和字段名获取字段信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static FieldInfo GetFieldInfo(string tableName, string fieldName)
        {
            string fldName = fieldName.ToUpper();

            Type t = Type.GetType($"GisqRealEstate.MaintainWeb.Core.{tableName},GisqRealEstate.MaintainWeb.Core", true, true);
            PropertyInfo propInfo = t.GetProperty(fldName);
            if (propInfo == null)
                throw new Exception($"{fldName}属性不存在");

            string len = null;
            IEnumerable<CustomAttributeData> customAttr = propInfo.CustomAttributes;
            if (customAttr != null && customAttr.Count() > 0)
            {
                try
                {
                    len = Convert.ToString(customAttr.ElementAtOrDefault(0).ConstructorArguments[0].Value);
                }
                catch (Exception)
                {
                }
            }

            // string desc = ((DescriptionAttribute)Attribute.GetCustomAttribute(propInfo, typeof(DescriptionAttribute))).Description;  // 属性描述

            string typeName = !propInfo.PropertyType.IsGenericType ? propInfo.PropertyType.FullName : Nullable.GetUnderlyingType(propInfo.PropertyType).FullName;

            return new FieldInfo { TypeName = typeName, TypeLength = len /*, Description = desc */};
        }


        public static object XmlDeserialize(string filepath, Type type)
        {
            object result = null;
            if (File.Exists(filepath))
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    try
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(type);
                        result = xmlSerializer.Deserialize(reader);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return result;
        }
    }

    public class FieldInfo
    {
        /// <summary>
        /// 字段类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 字段类型长度
        /// </summary>
        public string TypeLength { get; set; }
    }
}