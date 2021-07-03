using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Hubs
{
    public class MessageModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public  string UserName { get; set; }
        public int Price { get; set; }
        public DateTime ExpireDate { get; set; }

        public string ResponseMessage { get; set; }

        public bool AutoBid { get; set; }

    }
}
