using StackExchange.Redis;
using System;

namespace CodeForces.Units.Redis
{
    internal static class RedisUnit
    {
        private const String ConnectionString = "localhost";
        private const String Key = "mykey";

        public static void Run()
        {

            using (var redis = ConnectionMultiplexer.Connect(ConnectionString))
            {
                var db = redis.GetDatabase();
                db.StringSet(Key, "qwerty", TimeSpan.FromSeconds(5));
            }

            using (var redis = ConnectionMultiplexer.Connect(ConnectionString))
            {
                var db = redis.GetDatabase();
                Console.WriteLine(db.StringGet(Key));
            }
        }
    }
}