using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Wan.Release.Infrastructure.Base;

namespace Wan.Release.Infrastructure.Extends
{
    public static class TypeExtend
    {
        /// <summary>
        /// 获得当前的TableName
        /// </summary>
        /// <param name="classType">type</param>
        /// <returns>表名称</returns>
        public static string GetTableName(this Type classType)
        {
            var strTableName = string.Empty;

            var strEntityName = classType.FullName;

            foreach (var item in classType.GetCustomAttributes(false))
            {
                if (!(item is TableAttribute)) continue;
                var tableAttr = item as TableAttribute;
                strTableName = tableAttr.Name;
                break;
            }


            if (string.IsNullOrEmpty(strTableName))
            {
                throw new Exception("实体类:" + strEntityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
            }
            return strTableName;
        }

        /// <summary>
        /// 获得所有的属性字段,id放到第一个,如果没有主键返回Null
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static List<string> GetPropsList(this Type classType)
        {
            //ColumnList = new List<string>();
            var propsList = new List<string>();
            var ps = classType.GetProperties();
            var hasKey = false;
            foreach (var i in ps)
            {
                var isKey = i.IsPrimaryKey();
                if (isKey)
                {
                    hasKey = true;
                    propsList.Insert(0, i.Name);
                }
                else
                {
                    propsList.Add(i.Name);
                }

            }
            return !hasKey ? null : propsList;
        }

        /// <summary>
        /// 获得当前类型的Sql语句
        /// </summary>
        /// <param name="classType">当前type</param>
        /// <param name="commandEnum">sql类型</param>
        /// <returns></returns>
        public static string GetSql(this Type classType, CommandEnum commandEnum = CommandEnum.Insert)
        {
            var propsList = new List<string>();
            var ps = classType.GetProperties();
            var tableName = classType.GetTableName();
            var exp = " where ";
            foreach (var i in ps)
            {
                var isKey = i.IsPrimaryKey();
                if (isKey)
                {
                    propsList.Insert(0, i.Name);
                }
                else
                {
                    var relId = i.GetRelId();
                    if (!string.IsNullOrEmpty(relId))
                    {
                        exp = exp + relId + "=@" + i.Name + ",";
                        propsList.Add(relId);
                    }
                    else
                    {
                        propsList.Add(i.Name);
                    }
                }

            }
            if (commandEnum.Equals(CommandEnum.Insert))
            {
                var sqlText = "insert into " + tableName + "(";
                var valueText = " values ( ";
                foreach (var props in propsList)
                {
                    sqlText += props + ",";
                    valueText += "@" + props + ",";
                }

                sqlText = sqlText.Substring(0, sqlText.Length - 1);
                valueText = valueText.Substring(0, valueText.Length - 1);
                sqlText += ")";
                valueText += ")";

                return sqlText + valueText;

            }

            if (commandEnum.Equals(CommandEnum.Update))
            {
                var sqlText = "update " + tableName + " set ";
                for (var i = 1; i < propsList.Count; i++)
                {
                    sqlText = sqlText + propsList[i] + "=@" + propsList[i] + ",";
                }

                sqlText = sqlText.Substring(0, sqlText.Length - 1);
                sqlText += " where " + propsList[0] + "=@" + propsList[0];

                return sqlText;
            }

            if (commandEnum.Equals(CommandEnum.Delete))
            {
                var sqlText = "delete from " + tableName;
                sqlText += " where " + propsList[0] + "=@" + propsList[0];

                return sqlText;
            }

            if (commandEnum.Equals(CommandEnum.RelDelete))
            {
                var sqlText = "delete from " + tableName;
                exp = exp.Substring(0, exp.Length - 1);
                exp = exp.Replace(",", " and ");
                return sqlText + exp;
            }

            if (!commandEnum.Equals(CommandEnum.RelUpdate)) return null;
            {
                var sqlText = "update " + tableName + " set ";
                for (var i = 1; i < propsList.Count; i++)
                {
                    sqlText = sqlText + propsList[i] + "=@" + propsList[i] + ",";
                }

                sqlText = sqlText.Substring(0, sqlText.Length - 1);
                exp = exp.Substring(0, exp.Length - 1);
                exp = exp.Replace(",", " and ");
                return sqlText + exp;
            }
        }

        /// <summary>
        /// 获得当前类型的Sql语句
        /// </summary>
        /// <param name="classType">当前type</param>
        /// <param name="type">当前type的对象</param>
        /// <param name="commandEnum">sql类型</param>
        /// <returns></returns>
        public static string GetSql(this Type classType, object type, CommandEnum commandEnum = CommandEnum.Insert)
        {
            if (type.GetType() != classType)
            {
                return null;
            }
            var propsList = new List<string>();
            var ps = classType.GetProperties();
            var tableName = classType.GetTableName();
            var exp = " where ";
            foreach (var i in ps)
            {
                var temp = i.GetValue(type);
                if (temp == null) continue;
                var isKey = i.IsPrimaryKey();
                if (isKey)
                {
                    propsList.Insert(0, i.Name);
                }

                else
                {
                    var relId = i.GetRelId();
                    if (!string.IsNullOrEmpty(relId))
                    {
                        exp = exp + relId + "=@" + i.Name + ",";
                        propsList.Add(relId);
                    }
                    else
                    {
                        propsList.Add(i.Name);
                    }
                }
            }


            if (commandEnum.Equals(CommandEnum.Insert))
            {
                var sqlText = "insert into " + tableName + "(";
                var valueText = " values ( ";
                foreach (var props in propsList)
                {
                    sqlText += props + ",";
                    valueText += "@" + props + ",";
                }

                sqlText = sqlText.Substring(0, sqlText.Length - 1);
                valueText = valueText.Substring(0, valueText.Length - 1);
                sqlText += ")";
                valueText += ")";

                return sqlText + valueText;

            }

            if (commandEnum.Equals(CommandEnum.Update))
            {
                var sqlText = "update " + tableName + " set ";
                for (var i = 1; i < propsList.Count; i++)
                {
                    sqlText = sqlText + propsList[i] + "=@" + propsList[i] + ",";
                }

                sqlText = sqlText.Substring(0, sqlText.Length - 1);
                sqlText += " where " + propsList[0] + "=@" + propsList[0];

                return sqlText;
            }

            if (commandEnum.Equals(CommandEnum.Delete))
            {
                var sqlText = "delete from " + tableName;
                sqlText += " where " + propsList[0] + "=@" + propsList[0];

                return sqlText;
            }

            if (commandEnum.Equals(CommandEnum.RelDelete))
            {
                var sqlText = "delete from " + tableName;
                exp = exp.Substring(0, exp.Length - 1);
                exp = exp.Replace(",", " and ");
                return sqlText + exp;
            }

            if (!commandEnum.Equals(CommandEnum.RelUpdate)) return null;
            {
                var sqlText = "update " + tableName + " set ";
                for (var i = 1; i < propsList.Count; i++)
                {
                    sqlText = sqlText + propsList[i] + "=@" + propsList[i] + ",";
                }

                sqlText = sqlText.Substring(0, sqlText.Length - 1);
                exp = exp.Substring(0, exp.Length - 1);
                exp = exp.Replace(",", " and ");
                return sqlText + exp;
            }
        }

        /// <summary>
        /// in expression now is forbidden
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="type"></param>
        /// <param name="queryEnum"></param>
        /// <returns></returns>
        public static string GetQuerySql(this Type classType, object type, QueryEnum queryEnum = QueryEnum.Equal)
        {
            if (type.GetType() != classType)
            {
                return null;
            }
            var tempExp = " = ";
            if (queryEnum.Equals(QueryEnum.In))
            {
                tempExp = " = ";
            }
            var propsList = new List<string>();
            var ps = classType.GetProperties();
            var tableName = classType.GetTableName();
            var exp = " where ";
            foreach (var i in ps)
            {
                var temp = i.GetValue(type);
                if (temp == null) continue;
                var isKey = i.IsPrimaryKey();
                if (isKey)
                {
                    propsList.Insert(0, i.Name);
                }

                else
                {
                    var relId = i.GetRelId();
                    if (!string.IsNullOrEmpty(relId))
                    {
                        exp = exp + relId + tempExp + "@" + i.Name + ",";
                        propsList.Add(relId);
                    }
                    else
                    {
                        propsList.Add(i.Name);
                    }

                }
            }

            var sqlText = "select ";
            foreach (var props in propsList)
            {
                sqlText += props + ",";
            }
            sqlText = sqlText.Substring(0, sqlText.Length - 1);
            sqlText = sqlText + " from " + tableName;
            exp = exp.Substring(0, exp.Length - 1);
            exp = exp.Replace(",", " and ");

            return sqlText + exp;
        }

        public static string GetPrimaryQuerySql(this Type classType, QueryEnum queryEnum = QueryEnum.Equal)
        {
            var tempExp = " = ";
            if (queryEnum.Equals(QueryEnum.In))
            {
                tempExp = " in ";
            }
            var propsList = new List<string>();
            var ps = classType.GetProperties();
            var tableName = classType.GetTableName();

            foreach (var i in ps)
            {
                var isKey = i.IsPrimaryKey();
                if (isKey)
                {
                    propsList.Insert(0, i.Name);
                }

                else
                {
                    var relId = i.GetRelId();
                    propsList.Add(!string.IsNullOrEmpty(relId) ? relId : i.Name);
                }
            }

            var sqlText = "select ";
            foreach (var props in propsList)
            {
                sqlText += props + ",";
            }
            sqlText = sqlText.Substring(0, sqlText.Length - 1);
            sqlText = sqlText + " from " + tableName;
            var exp = " where " + propsList[0] + " " + tempExp + " @" + propsList[0];
            return sqlText + exp;
        }
    }
}