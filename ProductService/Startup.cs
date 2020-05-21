using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.dto;
using ProductService.Units;

namespace ProductService
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

            #region consul ×¢²á·þÎñ
            string ip = Configuration["ip"];
            int port = Convert.ToInt32(Configuration["port"]);
            string serviceName = "ProductService";
            string serviceId = serviceName + Guid.NewGuid();
            ServiceEntity serviceEntity = new ServiceEntity
            {
                IP = ip,
                Port = port,
                ServiceName = serviceName,
                ServiceId = serviceId,
                ConsulIP = "127.0.0.1",
                ConsulPort = 8500,
            };
            app.RegisterConsul(hostApplicationLifetime, serviceEntity);
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
