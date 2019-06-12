using System;
using Xunit;
using ServiceStack.Redis;

namespace RedisCounter.Tests
{
    public class TestRedisCounter
    {
        private IRedisClientsManager _rcm = null;
        private RedisCounter rc = null;

        ///<summary>
        /// Initializes connection to redis on localhost:6379
        ///</summary>
        public TestRedisCounter()
        {
            _rcm = new RedisManagerPool("localhost:6379");
            rc = new RedisCounter(_rcm,"count");
        }

        [Fact]
        public void TestGetCount()
        {
            using(var rclient = _rcm.GetClient())
            {
                rclient.Set("count",3);
            }

            int cv = rc.GetCount();
            Assert.True(cv == 3);
        }

        [Fact]
        public void TestIncrCount()
        {
            using(var rclient = _rcm.GetClient())
            {
                // confirm that call with no arguments increases value by 1
                int iv = rclient.Get<int>("count");
                rc.IncrCount();
                int sv = rclient.Get<int>("count");
                Assert.True(sv == iv + 1,"counter did not increase by 1");

                // confirm that call with an arg increases value by arg
                int arg = 5;
                iv = rclient.Get<int>("count");
                rc.IncrCount(arg);
                sv = rclient.Get<int>("count");
                Assert.True(sv == iv + arg, $"counter did not increase by {arg}");
            }
        }
    }
}