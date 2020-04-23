using System;
using StackExchange.Redis;

namespace Infrastructure
{
    public class RedisCache<T> : ICache<T>
    {
            private ConnectionMultiplexer redisConnection;

            public RedisCache(string hostName)
            {
                redisConnection = ConnectionMultiplexer.Connect(new ConfigurationOptions()
                {
                    EndPoints = { hostName },
                    AbortOnConnectFail = false
                });
            }

            public T Get(string key)
            {
                var db = redisConnection.GetDatabase();
                object s = db.StringGet(key);
                //if (s == null)
                //    return default(T);

                return (T)s;

            }

            public void Set(string key, T value)
            {
                var db = redisConnection.GetDatabase();

                if (value == null)
                {
                    db.StringSet(key, RedisValue.Null);
                }
                else
                {
                    db.StringSet(key, Convert.ToInt32(value));
                }
            }

    }
}