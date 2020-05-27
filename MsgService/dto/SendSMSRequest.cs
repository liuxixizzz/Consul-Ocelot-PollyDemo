using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsgService.dto
{
    public class SendSMSRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }
    }
}
