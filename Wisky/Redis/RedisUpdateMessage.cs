using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Wisky.Redis
{
    public class RedisUpdateMessage
    {
        public int Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<int> LocationIds { get; set; }
        public int BrandId { get; set; }
    }
}