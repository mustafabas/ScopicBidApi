using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Rabbit
{
    public interface IConsumerService
    {
        void ReceiveMessageFromQ();
    }
}
