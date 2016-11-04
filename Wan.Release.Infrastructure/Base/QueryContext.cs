using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using log4net;

namespace Wan.Release.Infrastructure.Base
{
    public class QueryContext
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseCommand));
        private readonly string _connString;

        public QueryContext(string connString)
        {
            _connString = connString;
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public T GetByParam<T>(BaseQuery query)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var item = conn.Query<T>(query.Sql, query.Obj).FirstOrDefault();
                conn.Close();
                return item;
            }
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<T> GetListByParam<T>(BaseQuery query)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var list = conn.Query<T>(query.Sql, query.Obj).ToList();
                conn.Close();
                return list;
            }
        }


        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static T BaseGetByParam<T>(BaseQuery query)
        {
            using (IDbConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var item = conn.Query<T>(query.Sql, query.Obj).FirstOrDefault();
                conn.Close();
                return item;
            }
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<T> BaseGetListByParam<T>(BaseQuery query)
        {
            using (IDbConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var list = conn.Query<T>(query.Sql, query.Obj).ToList();
                conn.Close();
                return list;
            }
        }
    }
}