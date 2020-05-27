using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsgService.dto
{
    public class SendEmailRequest
    {
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
    }
}
