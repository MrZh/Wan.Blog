using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using log4net;

namespace Wan.Release.Infrastructure.Base
{
    public class CommandContext
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        private readonly string _connString;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseCommand));
        public CommandContext(string connString)
        {
            _connString = connString;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="baseCommand">基础事务语句</param>
        /// <param name="commandTimeout">事务执行时间</param>
        /// <returns>是否成功</returns>
        public int ExecTransaction(BaseCommand baseCommand, int commandTimeout = 0)
        {
            int result;
            if (commandTimeout <= 0)
            {
                commandTimeout = 30;
            }
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        result = conn.Execute(baseCommand.Sql, baseCommand.Obj, trans, commandTimeout, CommandType.Text);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        Logger.Error(ex);
                        throw;
                    }
                    trans.Commit();
                }

                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="baseCommand">基础事务语句</param>
        /// <param name="commandTimeout">事务执行时间</param>
        /// <returns>是否成功</returns>
        public static int BaseTransaction(BaseCommand baseCommand, int commandTimeout = 0)
        {
            int result;
            if (commandTimeout <= 0)
            {
                commandTimeout = 30;
            }
            using (IDbConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        result = conn.Execute(baseCommand.Sql, baseCommand.Obj, trans, commandTimeout, CommandType.Text);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        Logger.Error(ex);
                        throw;
                    }
                    trans.Commit();
                }

                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="baseCommands">基础事务语句</param>
        /// <param name="commandTimeout">事务执行时间</param>
        /// <returns>是否成功</returns>
        public static int BaseTransaction(List<BaseCommand> baseCommands, int commandTimeout = 0)
        {
            var result = 0;
            if (commandTimeout <= 0)
            {
                commandTimeout = 30;
            }
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        result += baseCommands.Sum(baseCommand => conn.Execute(baseCommand.Sql, baseCommand.Obj, trans, commandTimeout, CommandType.Text));
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        Logger.Error(ex);
                        throw;
                    }
                    trans.Commit();
                }

                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="baseCommands">基础事务语句</param>
        /// <param name="commandTimeout">事务执行时间</param>
        /// <returns>是否成功</returns>
        public int ExecTransaction(List<BaseCommand> baseCommands, int commandTimeout = 0)
        {
            var result = 0;
            if (commandTimeout <= 0)
            {
                commandTimeout = 30;
            }
            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        result += baseCommands.Sum(baseCommand => conn.Execute(baseCommand.Sql, baseCommand.Obj, trans, commandTimeout, CommandType.Text));
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        Logger.Error(ex);
                        throw;
                    }
                    trans.Commit();
                }

                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 执行Command
        /// </summary>
        /// <param name="baseComand"></param>
        /// <returns></returns>
        public int ExecCommand(BaseCommand baseComand)
        {
            using (IDbConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                var result = conn.Execute(baseComand.Sql, baseComand.Obj);
                Logger.Info(baseComand.Sql);
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// 执行Command
        /// </summary>
        /// <param name="baseComand"></param>
        /// <returns></returns>
        public static int BaseCommand(BaseCommand baseComand)
        {
            using (IDbConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var result = conn.Execute(baseComand.Sql, baseComand.Obj);
                Logger.Info(baseComand.Sql);
                conn.Close();
                return result;
            }
        }
    }
}
