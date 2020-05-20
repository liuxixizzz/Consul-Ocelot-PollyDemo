using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsgService.dto
{
    public class SendEmailRequest
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
