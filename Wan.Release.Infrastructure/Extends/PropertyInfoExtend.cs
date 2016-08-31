using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Wan.Release.Infrastructure.Attribute;

namespace Wan.Release.Infrastructure.Extends
{
    public static class PropertyInfoExtend
    {

        /// <summary>
        /// 是否是主键
        /// </summary>
        /// <param name="propertyInfo">属性字段</param>
        /// <returns>是否主键</returns>
        public static bool IsPrimaryKey(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes().OfType<KeyAttribute>().Any();
        }

        /// <summary>
        /// 获得表字段
        /// </summary>
        /// <param name="propertyInfo">属性字段</param>
        /// <param name="isPrimaryKey">是否是主键</param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo propertyInfo, out bool isPrimaryKey)
        {
            isPrimaryKey = false;
            foreach (var item in propertyInfo.GetCustomAttributes())
            {

                if (item is KeyAttribute)
                {
                    isPrimaryKey = true;
                }
                else
                {
                    var attribute = item as ColumnAttribute;
                    if (attribute == null) continue;
                    var columnAttribute = attribute;
                    return columnAttribute.Name;
                }
            }

            return "";
        }

        /// <summary>
        /// 获得表字段
        /// </summary>
        /// <param name="propertyInfo">属性字段</param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo propertyInfo)
        {
            foreach (var item in propertyInfo.GetCustomAttributes())
            {
                if (!(item is ColumnAttribute)) continue;
                var columnAttribute = item as ColumnAttribute;
                return columnAttribute.Name;
            }

            return "";
        }

        /// <summary>
        /// 获得是否是外键
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool GetIsRelId(this PropertyInfo propertyInfo)
        {
            var isKey = propertyInfo.GetCustomAttributes().OfType<KeyAttribute>().Any();
            return !isKey && propertyInfo.GetCustomAttributes().OfType<RelIdAttribute>().Any();
        }

        /// <summary>
        /// 获得是否是外键
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string GetRelId(this PropertyInfo propertyInfo)
        {
            var isKey = propertyInfo.GetCustomAttributes().OfType<KeyAttribute>().Any();
            return isKey ? null : propertyInfo.GetCustomAttributes().OfType<RelIdAttribute>().Select(item => item as RelIdAttribute).Select(columnAttribute => string.IsNullOrWhiteSpace(columnAttribute.Name) ? propertyInfo.Name : columnAttribute.Name).FirstOrDefault();
        }
    }
}
