using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.dto;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>();
        static ProductController()
        {
            products.Add(new Product
            {
                Id = 1,
                Name = "T430笔记本",
                Price = 8888,
                Description = "CPU i7标压版，1T硬盘" });
            products.Add(new Product
            {
                Id = 2,
                Name = "华为Mate10",
                Price = 3888,
                Description = "大猩猩屏幕，多点触摸" });
            products.Add(new Product
            {
                Id = 3,
                Name = "天梭手表",
                Price = 9888,
                Description = "瑞士 经典款，可好了" });
        }
    
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var product = products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
            }
            return product;
        }
        [HttpPost]
        public void Add(Product model)
        {
            if (products.Any(p => p.Id == model.Id))
            {
                Response.StatusCode = (int)HttpStatusCode.Conflict;//通过状态码而非响应体报错，是restful风格
            return;
            }
            products.Add(model);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
        }
    }
}
