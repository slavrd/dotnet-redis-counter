using System;
using ServiceStack.Redis;

namespace RedisCounter
{
    public class RedisCounter: IRedisCounter
    {
        public string Key { get; set; }

        private IRedisClientsManager _RCManager = null;


        ///<summary>
        /// Constructs a RedisCounter instance using rCMan as redis clients manager and cKey as the counter key.
        ///</summary>
        public RedisCounter(IRedisClientsManager rCMan, string cKey) 
        {
            _RCManager = rCMan;
            Key = cKey;
            
            // set the counter initial value
        }

        ///<summary>
        /// Gets the current value of the counter.
        ///</summary>
        public int GetCount() {
            using (IRedisClient rclient = _RCManager.GetClient())
            {
                var r = rclient.Get<int>(Key);
                return r;
            }
        }

        ///<summary>
        /// Increases the counter value with c (1 by default).
        /// If this.Key has not been set, will be initialized with c as value
        ///</summary>
        public void IncrCount(int c = 1) {
            using (IRedisClient rclient = _RCManager.GetClient())
            {
                rclient.IncrementValueBy(Key, c);
            }
        }
    }
}
