using BidApp.Entities;
using BidApp.Service.Hubs;
using BidApp.Service.Products;
using BidApp.Service.Rabbit;
using BidApp.Service.Users;
using Microsoft.AspNetCore.SignalR;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BidApp.Service.Hubs
{
    public class BidHub : Hub
    {
        readonly IProductBidService _productBidService;

         readonly IProducerService _producer;

        readonly IUserService _userService;

        public BidHub(IProductBidService productBidService, IProducerService producer, IUserService userService)
        {
            this._productBidService = productBidService;
            this._producer = producer;
            this._userService = userService;
        }




        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task GroupSendMessage(string groupName, string message)
        {
            MessageModel messageModel = new JavaScriptSerializer().Deserialize<MessageModel>(message);

            if (messageModel.ExpireDate > DateTime.Now)
            {
        
                var user = _userService.GetUserById(messageModel.UserId);
                var sumOther = _productBidService.GetSumOfferByUserId(messageModel.UserId, messageModel.ProductId);
                int price = Convert.ToInt32(user.MaxAmount);

                if(messageModel.AutoBid)
                {
                    if (price < messageModel.Price + sumOther)
                    {
                        messageModel.ResponseMessage = "you can't bid more than the amount you set";
                        var json = new JavaScriptSerializer().Serialize(messageModel);
                        await Clients.Group(groupName).SendAsync("ReceiveMessage", json);
                    }
                    else
                    {
                        var json = new JavaScriptSerializer().Serialize(messageModel);
                        await Clients.Group(groupName).SendAsync("ReceiveMessage", json);
                        _producer.PushMessageToQ(messageModel);
                    }
                }
                else
                {
                    var json = new JavaScriptSerializer().Serialize(messageModel);
                    await Clients.Group(groupName).SendAsync("ReceiveMessage", json);
                            _producer.PushMessageToQ(messageModel);
                }
            }
            else
            {
                messageModel.ResponseMessage = "Time Expired";
                var json = new JavaScriptSerializer().Serialize(messageModel);
                await Clients.Group(groupName).SendAsync("ReceiveMessage", json);

            }

        }
    }
}
