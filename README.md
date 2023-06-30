# MicroService
new a tutorial &amp;&amp; log for myself to learn related components about microservice.

## 服务发现
首先，微服务是将原本单体程序，不断拆分成多个服务实例，并采用高可用的方式如集群，来完成之前单体的功能。在微服务拆分过程中，可能服务很多，集群也很多，那么对于需要调用服务的客户端来说，通过http请求服务的时候，需要知道IP及端口，而服务众多且配置灵活，可能因为某些服务会动态扩展，所以配置上也需要一个中间件，它的功能就是对于所有服务，统一进行服务注册到它身上，这样客户端在访问的时候，就只需要跟它打交道，它则把客户端需要的服务的真实IP及端口给到客户端，这样客户端就能进行访问了。而这个过程也就是服务发现。
### 一. Consul中间件  
    对于NET Core程序，我使用的是Consul来完成此服务发现的功能。现交代下具体执行环境
- `docker run -d -p 8500:8500 --name=consul1 consul:1.15.4 agent -server -bootstrap -ui -client='0.0.0.0'`
- `docker run -d -p 9500:8500 --name=consul2 consul:1.15.4 agent -server -bootstrap -ui -client='0.0.0.0' -join 172.17.0.2`
- `docker run -d -p 10500:8500 --name=consul3 consul:1.15.4 agent -server -bootstrap -ui -client='0.0.0.0' -join 172.17.0.2`
- `docker run -d -p 11500:8500 --name=consulclient consul:1.15.4 agent  -ui -client='0.0.0.0' -join 172.17.0.2`
- 这里使用的是docker环境，运行了三个server的Consul以及一个client的Consul实例。
- `dotnet new webapi --name ServiceA`
- `dotnet add package Consul`
- 然后新建一个webapi的项目，并且引用此Consul的Nuget包。


## 服务治理
服务治理很高大上，其实说的是对于请求服务的过程中进行管理这些请求，如请求超时了，做请求限流的功能，做服务熔断的功能等，不需要集成在客户端的请求上，可以使用中间件来统一实现这些功能，有点AOP编程的味道。
### 一. Ocelet中间件
    对于NET Core程序，我使用的是Ocelot来作为此服务治理的网关，且此中间件是用C#编写且开源的，方便自己去理解，更新和定制。
### 二. 集成Ocelot与Consul中间件——标配
