using Wan.Release.Infrastructure.Base;
using Wan.Release.Infrastructure.Extends;

namespace Wan.Release.Infrastructure.Query
{
    public class Query<T> : BaseQuery where T : Entity.Entity
    {
        public Query(T obj, QueryEnum queryEnum = QueryEnum.Equal)
        {
            Sql = typeof(T).GetQuerySql(obj, queryEnum);
            Obj = obj;
        }

        public Query(object obj, QueryEnum queryEnum = QueryEnum.Equal)
        {
            Sql = typeof(T).GetPrimaryQuerySql(queryEnum);
            Obj = obj;
        }

        public static BaseQuery InitQuery(T obj, QueryEnum queryEnum = QueryEnum.Equal)
        {
            return new BaseQuery(typeof(T).GetQuerySql(obj, queryEnum), obj);
        }

        public static BaseQuery InitPrimaryQuery(object obj, QueryEnum queryEnum = QueryEnum.Equal)
        {
            return new BaseQuery(typeof(T).GetPrimaryQuerySql(queryEnum), obj);
        }

        public static BaseQuery InitQuery(object obj, string sql)
        {
            return new BaseQuery(sql, obj);
        }
    }
}