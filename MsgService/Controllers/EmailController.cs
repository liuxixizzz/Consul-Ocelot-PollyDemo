using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MsgService.dto;

namespace MsgService.Controllers
{
    /// <summary>
    /// 邮件接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        /// <summary>
        /// QQ邮件接口
        /// </summary>
        /// <param name="model"></param>
        [HttpPost(nameof(Send_QQ))]
        public void Send_QQ(SendEmailRequest model)
        {
            Console.WriteLine($"通过QQ邮件接口向{model.Email}发送邮件，标题{model.Title}，内容：{ model.Body}");
        }

        /// <summary>
        /// 网易邮件接口
        /// </summary>
        /// <param name="model"></param>
        [HttpPost(nameof(Send_163))]
        public void Send_163(SendEmailRequest model)
        {
            Console.WriteLine($"通过网易邮件接口向{model.Email}发送邮件，标题{model.Title}，内容：{ model.Body}");
        }

        /// <summary>
        /// Sohu邮件接口
        /// </summary>
        /// <param name="model"></param>
        [HttpPost(nameof(Send_Sohu))]
        public void Send_Sohu(SendEmailRequest model)
        {
            Console.WriteLine($"通过Sohu邮件接口向{model.Email}发送邮件，标题{model.Title}，内容：{ model.Body}");
        }
    }
}
