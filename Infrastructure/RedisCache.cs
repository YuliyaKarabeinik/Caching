using System;
using System.IO;
using System.Runtime.Serialization;
using StackExchange.Redis;

namespace Infrastructure
{
    public class RedisCache : ICache
    {
        private readonly ConnectionMultiplexer redisConnection;
        private readonly string prefix;
        private Type type;

        DataContractSerializer serializer => new DataContractSerializer(type);

        public RedisCache(string hostName, string prefix = "")
        {
            this.prefix = prefix;
            redisConnection = ConnectionMultiplexer.Connect(new ConfigurationOptions()
            {
                EndPoints = { hostName },
                AbortOnConnectFail = false
            });
        }

        public T Get<T>(string key)
        {
            type = typeof(T);
            var db = redisConnection.GetDatabase();
            byte[] s = db.StringGet(prefix + key);
            return s == null ? default(T) : (T)serializer.ReadObject(new MemoryStream(s));
        }

        public void Set(string key, object value, DateTime expirationTime)
        {
            type = value.GetType();
            var db = redisConnection.GetDatabase();

            if (value == null)
            {
                db.StringSet(prefix + key, RedisValue.Null, expirationTime - DateTime.Now);
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, value);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}