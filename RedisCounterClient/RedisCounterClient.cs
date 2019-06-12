using System;
using RedisCounter;

namespace RedisCounterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IRedisCounter rc = RedisCounterFactory.GetCounter("counter");
            Console.WriteLine("current count is {0}", rc.GetCount());
            Console.WriteLine("increasing count...");
            rc.IncrCount();
            Console.WriteLine("current count is now {0}", rc.GetCount());

        }

        static Program()
        {
            // Get the Reddis Address from env var
            string raddr = Environment.GetEnvironmentVariable("REDIS_ADDR");
            if (String.IsNullOrEmpty(raddr))
            {
                Console.WriteLine("error: REDIS_ADDR environment variable is not set");
                Environment.Exit(1);
            }

            // Check if raddr contains a port. If not use redis default
            string[] rhost = raddr.Split(':');
            if (rhost.Length == 2)
            {   
                RedisCounterFactory.SetRCM(rhost[0], Int32.Parse(rhost[1]), 0);
            }
            else
            {
                RedisCounterFactory.SetRCM(rhost[0], 6379, 0);
            }
        }
    }
}
