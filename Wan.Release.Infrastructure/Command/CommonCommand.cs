using System;
using System.Collections.Generic;
using System.Linq;
using Wan.Release.Infrastructure.Base;
using Wan.Release.Infrastructure.Extends;

namespace Wan.Release.Infrastructure.Command
{
    /// <summary>
    ///     Just for sth very special
    /// </summary>
    public class CommonCommand : BaseCommand
    {
        /// <summary>
        ///     Init for object and commandType
        /// </summary>
        /// <param name="obj">obj</param>
        /// <param name="commandEnum">commandType</param>
        public CommonCommand(object obj, CommandEnum commandEnum)
        {
            CommandId = Guid.NewGuid().ToString();
            Sql = obj.GetType().GetSql(obj, commandEnum);
            Obj = obj;
        }

        /// <summary>
        ///     Init for list of object and commandType
        /// </summary>
        /// <param name="objs">obj</param>
        /// <param name="commandEnum">commandType</param>
        public CommonCommand(List<object> objs, CommandEnum commandEnum)
        {
            if (objs == null) throw new ArgumentNullException(nameof(objs));
            CommandId = Guid.NewGuid().ToString();
            if (objs.Count <= 0)
            {
                return;
            }

            Obj = objs;
            Sql = objs[0].GetType().GetSql(objs[0], commandEnum);
        }

        /// <summary>
        ///     Init for list of object
        /// </summary>
        /// <param name="objs">obj</param>
        public CommonCommand(List<object> objs)
        {
            if (objs == null) throw new ArgumentNullException(nameof(objs));
            CommandId = Guid.NewGuid().ToString();
            if (objs.Count <= 0)
            {
                return;
            }

            Obj = objs;
            Sql = objs[0].GetType().GetSql(objs[0]);
        }

        /// <summary>
        ///     Init for object
        /// </summary>
        /// <param name="obj">obj</param>
        public CommonCommand(object obj)
        {
            CommandId = Guid.NewGuid().ToString();
            Sql = obj.GetType().GetSql(obj);
            Obj = obj;
        }

        /// <summary>
        ///     Init for obj with commandType
        /// </summary>
        /// <param name="obj">obj</param>
        /// <param name="justSql">true for full sql, false for the sql from obj</param>
        /// <param name="commandEnum">commandType</param>
        public CommonCommand(object obj, bool justSql = true, CommandEnum commandEnum = CommandEnum.Insert)
        {
            if (justSql)
            {
                Obj = obj;
                Sql = obj.GetType().GetSql(commandEnum);
            }

            else
            {
                Obj = obj;
                Sql = obj.GetType().GetSql(obj, commandEnum);
            }

            CommandId = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Init for list of obj with commandType
        /// </summary>
        /// <param name="objs">obj</param>
        /// <param name="justSql">true for full sql, false for the sql from obj</param>
        /// <param name="commandEnum">commandType</param>
        public CommonCommand(List<object> objs, bool justSql = true, CommandEnum commandEnum = CommandEnum.Insert)
        {
            if (objs.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(objs));
            if (justSql)
            {
                Obj = null;
                Sql = objs[0].GetType().GetSql(commandEnum);
            }

            else
            {
                Obj = objs;
                Sql = objs[0].GetType().GetSql(objs[0], commandEnum);
            }

            CommandId = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Init BaseCommand for object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="commandEnum"></param>
        /// <returns></returns>
        public static BaseCommand InitBaseCommand(object obj, CommandEnum commandEnum = CommandEnum.Insert)
        {
            return new BaseCommand(obj.GetType().GetSql(commandEnum), obj);
        }

        /// <summary>
        ///     只用于批量增加数据
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="commandEnum"></param>
        /// <returns></returns>
        public static BaseCommand InitBaseCommand(List<object> objs, CommandEnum commandEnum = CommandEnum.Insert)
        {
            if (objs.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(objs));
            return new BaseCommand(objs[0].GetType().GetSql(CommandEnum.Insert), objs);
        }

        /// <summary>
        ///     Init BaseCommand for everyone in objs
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="commandEnum"></param>
        /// <returns></returns>
        public static List<BaseCommand> InitBaseCommands(List<object> objs, CommandEnum commandEnum = CommandEnum.Insert)
        {
            if (objs.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(objs));

            return objs.Select(item => new BaseCommand(item.GetType().GetSql(item, commandEnum), item)).ToList();
        }
    }
}