using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MsgService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region consul 注册服务
            string ip = Configuration["ip"];
            int port = Convert.ToInt32(Configuration["port"]);

            string serviceName = "MsgService";
            string serviceId = serviceName + Guid.NewGuid();
            using (var client = new ConsulClient(a =>
            {
                a.Address = new Uri("http://127.0.0.1:8500");
                a.Datacenter = "dc1";
            }))
            {
                client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = serviceId,
                    Name = serviceName,
                    Address = ip,
                    Port = port,
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务停止多久后反注册(注销)
                        Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                        HTTP = $"http://{ip}:{port}/api/health",//健康检查地址
                        Timeout = TimeSpan.FromSeconds(5)
                    }
                }).Wait();
            }
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                using (var client = new ConsulClient(a =>
                {
                    a.Address = new Uri("http://127.0.0.1:8500");
                    a.Datacenter = "dc1";
                }))
                {
                    client.Agent.ServiceDeregister(serviceId).Wait();
                }
            });
            #endregion


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
