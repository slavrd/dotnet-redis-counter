# A simple C# Redis counter

A basic C# library for a counter using Redis as a backend. It uses 

## RedisCounter

Consists of a redis counter interface `IRedisCounter` and a concrete class `RedisCounter` which implements it. It also contains a static class `RedsiCounterFactory` used to manage the redis connection and produce instances of `IRedisCounter`.

## Using IRedisCounter

`IRedisCounter` interface defines the following method:

* Getting the current count  - `int GetCount()`
* Increasing the counter by a provided value (default 1 if nothing is provided) - `void IncrCount(int c = 1)`

## Instances of `RedisCounter`

The Library uses [`ServiceStack.Redis`](https://github.com/ServiceStack/ServiceStack.Redis) to connect to a Redis server.

To create an instance of `RedisCounter` directly you need to provide an instance of `ServiceStack.Redis.IRedisClientsManager` to the constructor so it can connect to the Redis server and the also the redis key which will be used. For example:

```C#
var rcm = new ServiceStack.Redis.IRedisClientsManager("localhost");
var counter = new RedisCounter(rcm, "count");
```

Alternatively you can use the static `RedisCounterFactory.GetCounter(string cKey = "counter")` to get a counter which uses a provided redis key.

Before using `RedisCounterFactory` to generate `RedisCounter` instances the redis connection manager needs to be set-up. This is done by calling `SetRCM(string raddr, int rport, int rdb)` where you provide the redis host, redis port and the id of the redis db.

```C#
RedisCounterFactory.SetRCM("localhost",6379,0); // connect to redis on localhost:6379, db id 0
IRedisCounter counter = RedisCounterFactory.GetCounter("count"); // use redis key count
counter.IncrCount();
string cc = counter.GetCount();
Console.Writeline($"Current count is {cc}");
```

## RedisCounterClient

The project contains a simple console app that uses the library. Each time it is started it will display the current value of the counter, increase it by 1 and then display the new value.

It takes the redis address from environment variable `REDIS_ADDR`. The redis host and port can be set e.g `localhost:6379`. If the port is not set the default redis port `6379` will be used.

To run it:

```bash
export REDIS_ADDR="redis_host:redis_port"
cd RedisCounterClient
dotnet build
dotnet bin/Debug/netcoreapp2.2/RedisCounterClient.dll
```

## Vagrant project

There is Vagrant projec included which can be used to build a redis server and an Ubuntu Xenial client with installed .Net SDK

The redis server will listen to the default port `6379` which will also be mapped to the same port on the host.

### Vagrant prerequisites

* Install [VirtualBox](https://www.virtualbox.org/wiki/Downloads)
* Install [Vagrant](https://www.vagrantup.com/downloads.html)

### Run Vagrant

* Build VMs - `vagrant up`
* Login to VM - `vagrant ssh redis|client`
* Destroy VMs - `vagrant destroy`

To run the `RedisCounterClient`, while connected to the `client` VM:

* `cd /vagrant/RedisCounter/Client`
* `dotnet build`
* `dotnet bin/Debug/netcoreapp2.2/RedisCounterClient.dll`
