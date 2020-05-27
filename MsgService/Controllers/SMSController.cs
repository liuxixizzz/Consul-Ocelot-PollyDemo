using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MsgService.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsgService.Controllers
{
    /// <summary>
    /// 短信接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SMSController
    {
        private readonly ILogger<SMSController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public SMSController(ILogger<SMSController> logger)
        {
            _logger = logger;
        }


        //发请求，报文体为{phoneNum:"110",msg:"aaaaaaaaaaaaa"}，
        /// <summary>
        /// 小米短信接口
        /// </summary>
        /// <param name="model"></param>
        [HttpPost(nameof(Send_MI))]
        public void Send_MI(dynamic model)
        {
            Console.WriteLine($"通过小米短信接口向{model.phoneNum}发送短信{model.msg}");
        }

        /// <summary>
        /// 联想短信接口
        /// </summary>
        /// <param name="model"></param>
        [HttpPost(nameof(Send_LX))]
        public void Send_LX(SendSMSRequest model)
        {
            Console.WriteLine($"通过联想短信接口向{model.PhoneNum}发送短信{model.Msg}");
            _logger.LogInformation($"通过小米短信接口向{model.PhoneNum}发送短信{model.Msg}");
        }

        /// <summary>
        /// 华为短信接口
        /// </summary>
        /// <param name="model"></param>
        [HttpPost(nameof(Send_HW))]
        public void Send_HW(SendSMSRequest model)
        {
            Console.WriteLine($"通过华为短信接口向{model.PhoneNum}发送短信{model.Msg}");
        }
    }
}
