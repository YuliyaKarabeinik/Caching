using System;
using StackExchange.Redis;

namespace Infrastructure
{
    public class RedisCache : ICache
    {
            private ConnectionMultiplexer redisConnection;
            private readonly string prefix;

            public RedisCache(string hostName, string prefix = " ")
            {
                this.prefix = prefix;
                redisConnection = ConnectionMultiplexer.Connect(new ConfigurationOptions()
                {
                    EndPoints = { hostName },
                    AbortOnConnectFail = false
                });
            }

            public object Get(string key)
            {
                var db = redisConnection.GetDatabase();
                return db.StringGet(prefix + key);

            }

            public void Set(string key, object value)
            {
                var db = redisConnection.GetDatabase();

                if (value == null)
                {
                    db.StringSet(prefix + key, RedisValue.Null);
                }
                else
                {
                    db.StringSet(prefix + key, Convert.ToInt32(value));
                }
            }

    }
}