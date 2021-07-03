using BidApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BidApp.Service.Products
{
    public interface IProductBidService
    {
        Task<bool> InsertProductBid(ProductBidEntity productBid);
        List<ProductBidEntity> GetProductBid(int userId, int productId);

        decimal GetSumOfferByUserId(int userId, int currenctProductId);


    }
}
