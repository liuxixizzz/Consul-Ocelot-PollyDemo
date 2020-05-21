using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ProductService.dto;
using System;

namespace ProductService.Units
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime hostApplicationLifetime, ServiceEntity serviceEntity)
        {
            using (var client = new ConsulClient(a => { a.Address = new Uri($"http://{serviceEntity.ConsulIP}:{serviceEntity.ConsulPort}"); a.Datacenter = "dc1"; }))
            {
                client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = serviceEntity.ServiceId,
                    Name = serviceEntity.ServiceName,
                    Address = serviceEntity.IP,
                    Port = serviceEntity.Port,
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务停止多久后反注册(注销)
                        Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                        HTTP = $"http://{serviceEntity.IP}:{serviceEntity.Port}/api/health",//健康检查地址
                        Timeout = TimeSpan.FromSeconds(5)
                    }
                }).Wait();
                hostApplicationLifetime.ApplicationStopped.Register(() =>
                {
                    using (var client = new ConsulClient(a => { a.Address = new Uri($"http://{serviceEntity.ConsulIP}:{serviceEntity.ConsulPort}"); a.Datacenter = "dc1"; }))
                    {
                        client.Agent.ServiceDeregister(serviceEntity.ServiceId).Wait();
                    }
                });
            }
            return app;
        }
    }
}
