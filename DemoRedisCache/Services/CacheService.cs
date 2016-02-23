using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using StackExchange.Redis;
using System.Threading.Tasks;
using StackExchange.Redis.DataTypes;
using DemoRedisCache.Common;


namespace DemoRedisCache.Services
{
    public interface ICacheService
    {
        bool IsAlive();
        bool Exists(string key);
        void Save(string key, string value, TimeSpan? expiry = null);
        void Save(string key, int value, TimeSpan? expiry = null);
        string Get(string key);
        List<string> Get(List<string> keys);
        void Remove(string key);
    }

    public class CacheService : ICacheService
    {
        private readonly IRedisCache _cache;
        private readonly TimeSpan _duration = TimeSpan.FromMilliseconds(5000);

        public CacheService()
        {
            var cacheEndpoint = ConfigurationManager.AppSettings["CacheEndpoint"];
            var cachePassword = ConfigurationManager.AppSettings["CachePassword"];
            var cacheRetry = 3;// 5;
            _cache = new RedisCache(cacheEndpoint, cachePassword, cacheRetry);
        }

        public bool IsAlive()
        {
            return _cache != null && _cache.IsAlive();
        }

        public void RunScript(string script)
        {
            _cache.DBInstance.ScriptEvaluate(script);
        }

        public bool Exists(string key)
        {
            return _cache.DBInstance.KeyExists(key);
        }

        public void Save(string key, string value, TimeSpan? expiry = null)
        {
            _cache.DBInstance.StringSetAsync(key, value, expiry);
        }

        public void Save(string key, int value, TimeSpan? expiry = null)
        {
            _cache.DBInstance.StringSetAsync(key, value.ToString(), expiry);
        }

        public string Get(string key)
        {
            return _cache.DBInstance.StringGet(key);
        }

        public List<string> Get(List<string> keys)
        {
            List<RedisKey> hashedKeys = new List<RedisKey>();

            if (keys == null || keys.Count == 0)
                return null;

            foreach (string key in keys)
                hashedKeys.Add(key);

            return Get(hashedKeys);
        }

        public void Remove(string key)
        {
            _cache.DBInstance.KeyDelete(key);
        }

        #region Private Methods

        private List<string> Get(List<RedisKey> hashedRedisKeys)
        {
            var values = _cache.DBInstance.StringGet(hashedRedisKeys.ToArray()).Select(rv => rv.ToString()).ToList();

            // decrypt values
            int valuesCount = values.Count();
            for (int i = 0; i < valuesCount; i++)
                values[i] = values[i];

            return values;
        }

        #endregion
    }
}