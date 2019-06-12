using System;

namespace RedisCounter
{
    public interface IRedisCounter
    {
        ///<summary>
        /// Gets the current value of the counter.
        ///</summary>
        int GetCount();

        ///<summary>
        /// Increases the counter value with c (1 by default).
        /// If this.Key has not been set, will be initialized with c as value
        ///</summary>
        void IncrCount(int c = 1);
    }
}