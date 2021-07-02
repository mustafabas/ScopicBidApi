using BidApp.DataL;
using BidApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidApp.Service.Products
{
    public class ProductBidService : IProductBidService
    {
        IRepository<ProductBidEntity> _productBidRepository;
        IAsyncRepository<ProductBidEntity> _productBidAsync;

        public ProductBidService(IRepository<ProductBidEntity> productBidRepository, IAsyncRepository<ProductBidEntity> productBidAsync)
        {
            this._productBidAsync = productBidAsync;
            this._productBidRepository = productBidRepository;
        }
        public  List<ProductBidEntity> getProductBid(int userId, int productId)
        {
            var query = _productBidRepository.ListAll();
            return query.Where(x => x.ProductId == productId && x.UserId == userId).ToList();
            
        }

        public async Task<bool> insertProductBid(ProductBidEntity productBid)
        {
            await _productBidAsync.AddAsync(productBid);
            return true;
        }
    }
}
