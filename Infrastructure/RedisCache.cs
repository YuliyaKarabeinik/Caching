using System;
using System.IO;
using System.Runtime.Serialization;
using StackExchange.Redis;

namespace Infrastructure
{
    public class RedisCache : ICache
    {
        private ConnectionMultiplexer redisConnection;
        private readonly string prefix;

        DataContractSerializer serializer = new DataContractSerializer(typeof(object));

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
            byte[] s = db.StringGet(prefix + key);
            return s == null ? null : serializer.ReadObject(new MemoryStream(s));
        }

        public void Set(string key, object value, DateTime expirationTime)
        {
            var db = redisConnection.GetDatabase();

            if (value == null)
            {
                db.StringSet(prefix + key, RedisValue.Null, expirationTime - DateTime.Now);
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, value);
                db.StringSet(key, stream.ToArray(), expirationTime - DateTime.Now);
            }
        }
    }
}