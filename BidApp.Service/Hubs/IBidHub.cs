using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BidApp.Service.Hubs
{
   public interface IBidHub
    {
         Task SendMessage(String message);
         Task SendMqMessage(List<String> messages);
    }
}
