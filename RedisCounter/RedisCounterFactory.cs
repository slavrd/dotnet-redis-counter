using System;
using ServiceStack.Redis;

namespace RedisCounter
{
    public static class RedisCounterFactory
    {
        private static IRedisClientsManager _rcm = null;

        ///<summary>
        /// Configure the Redis clients manager.
        ///</summary>
        public static void SetRCM(string raddr, int rport, int rdb)
        {
            string rcon = String.Format($"redis://{raddr}?db={rdb}");
            _rcm = new RedisManagerPool(rcon);
        }

        ///<summary>
        /// Create an IRadesCounter with the factory's IRedisClientsManager
        ///</summary>
        public static IRedisCounter GetCounter(string cKey = "counter")
        {
            if (_rcm == null)
            {
                throw new ApplicationException("factory's redis client manager is not set");
            }
            return new RedisCounter(_rcm, cKey);
        }
    }
}