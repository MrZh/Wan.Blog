using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Wan.Blog.Domain.Common
{
    public class CommonRedis : IDisposable
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        private const string Conn = "localhost";

        public CommonRedis(string redisConnString = null)
        {
            if (redisConnString == null)
            {
                redisConnString = Conn;
            }

            _redis = ConnectionMultiplexer.Connect(redisConnString);
            _db = _redis.GetDatabase();
        }


        /// <summary>
        /// 根据键值获得数据
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>对象</returns>
        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        /// <summary>
        /// 根据键值获得对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            string result = _db.StringGet(key);
            if (string.IsNullOrWhiteSpace(result))
            {
                result = "";
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// 设置Redis值
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <param name="expireMinutes">过期时间</param>
        /// <returns></returns>
        public bool Insert(string key, string value, int expireMinutes = 0)
        {
            return expireMinutes > 0 ? _db.StringSet(key, value, TimeSpan.FromMinutes(expireMinutes)) : _db.StringSet(key, value);
        }

        /// <summary>
        /// 设置Redis值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <param name="expireMinutes">过期时间</param>
        /// <returns></returns>
        public bool Insert<T>(string key, T value, int expireMinutes = 0)
        {
            string tempValue = JsonConvert.SerializeObject(value);
            return expireMinutes > 0 ? _db.StringSet(key, tempValue, TimeSpan.FromMinutes(expireMinutes)) : _db.StringSet(key, tempValue);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>是否删除成功</returns>
        public bool Remove(string key)
        {
            return _db.KeyDelete(key);
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }
}
