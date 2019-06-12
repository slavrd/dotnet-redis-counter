using System;
using Xunit;
using ServiceStack.Redis;

namespace RedisCounter.Tests
{
    public class TestRedisCounterFactory
    {
        [Fact]
        public void TestInitRCM()
        {   
            // Assuming redis available on localhost:6379. If fails will throw an exception
            try 
            {
                RedisCounterFactory.SetRCM("localhost",6379,0);
            }
            catch(Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void TestGetCounter()
        {
            RedisCounterFactory.SetRCM("localhost",6379,0);

            var rc = RedisCounterFactory.GetCounter();
            Assert.IsType<RedisCounter>(rc);
        }

    }

}