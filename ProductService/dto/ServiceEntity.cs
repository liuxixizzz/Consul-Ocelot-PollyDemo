using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.dto
{
    public class ServiceEntity
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string ServiceName { get; set; }
        public string ServiceId { get; set; }
        public string ConsulIP { get; set; }
        public int ConsulPort { get; set; }
    }
}
