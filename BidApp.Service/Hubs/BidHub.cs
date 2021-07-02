using BidApp.Entities;
using BidApp.Service.Hubs;
using BidApp.Service.Products;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BidApp.Service.Hubs
{
    public class BidHub : Hub
    {
        readonly IProductBidService _productBidService;


        
        public BidHub(IProductBidService productBidService)
        {
            this._productBidService = productBidService;
        }


        public async Task SendMqMessage(List<string> messages)
        {
            await Clients.All.SendAsync("ReceiveMQMessage", messages);
    
        }


        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task GroupSendMessage(string groupName, string message)
        {

            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
            string[] messages = message.Split("-");
            string[] userInfos = messages[0].Split(":");

            ProductBidEntity productBid = new ProductBidEntity();
            productBid.Offer =Convert.ToDecimal(messages[1]);
            productBid.ProductId = Convert.ToInt32(groupName);
            productBid.RecordDate = DateTime.Now;
            productBid.UserId = Convert.ToInt32(userInfos[0]);
           await _productBidService.insertProductBid(productBid);
        }
    }
}
