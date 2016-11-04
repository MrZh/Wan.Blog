namespace Wan.Release.Infrastructure.Base
{
    public class BaseQuery
    {
        public BaseQuery(string sql, object obj)
        {
            Sql = sql;
            Obj = obj;
        }

        protected BaseQuery()
        {
        }

        public string Sql { get; protected set; }

        public object Obj { get; protected set; }
    }
}