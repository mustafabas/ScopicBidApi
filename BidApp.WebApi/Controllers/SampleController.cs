using BidApp.Service.Rabbit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IProducerService _producer;
        private readonly IMemoryCache _memoryCache;

        public SampleDataController(IProducerService producer, IMemoryCache memoryCache)
        {
            _producer = producer;
            _memoryCache = memoryCache;
        }

        [HttpGet("[action]")]
        public void SendToQ()
        {
            _producer.PushMessageToQ();
        }

        [HttpGet("[action]")]
        public List<string> Refresh()
        {
            Dictionary<string, int> messages = null;
            _memoryCache.TryGetValue<Dictionary<string, int>>("messages", out messages);
            if (messages == null) messages = new Dictionary<string, int>();

            return messages.OrderBy(m => m.Value).Select(m => m.Key).ToList();
        }
    }
}
