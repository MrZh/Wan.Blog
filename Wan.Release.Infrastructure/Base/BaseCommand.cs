using System;

namespace Wan.Release.Infrastructure.Base
{
    public class BaseCommand
    {
        public string CommandId { get; protected set; }

        public string Sql { get; protected set; }

        public object Obj { get; protected set; }

        public BaseCommand(string sql, object obj)
        {
            Sql = sql;
            Obj = obj;
            CommandId = Guid.NewGuid().ToString();
        }

        protected BaseCommand()
        {

        }
    }
}
