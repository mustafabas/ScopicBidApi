using BidApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BidApp.Service.Products
{
    public interface IProductBidService
    {
        Task<bool> insertProductBid(ProductBidEntity productBid);
        List<ProductBidEntity> getProductBid(int userId, int productId);


    }
}
