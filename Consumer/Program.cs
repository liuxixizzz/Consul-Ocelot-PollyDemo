using Consul;
using RestTemplateCore;
using System;
using System.Net.Http;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取服务
            using (var consulClient = new ConsulClient(c => c.Address = new Uri("http://127.0.0.1:8500")))
            {
                var services = consulClient.Agent.Services().Result.Response;
                foreach (var service in services.Values)
                {
                    Console.WriteLine($"id={service.ID},name={service.Service},ip={service.Address},port={service.Port}");
                }
            }
            //消费服务
            using (HttpClient httpClient = new HttpClient())
            {
                RestTemplate rest = new RestTemplate(httpClient);
                rest.ConsulServerUrl = "http://127.0.0.1:8500";
                var ret1 = rest.GetForEntityAsync<Product[]>("http://ProductService/api/Product/").Result;
                Console.WriteLine(ret1.StatusCode);
                if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (var p in ret1.Body)
                    {
                        Console.WriteLine($"id={p.Id},name={p.Name}");
                    }
                }
            }
        }
    }

    class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
