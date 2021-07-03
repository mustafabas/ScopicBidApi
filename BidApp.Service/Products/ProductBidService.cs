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
        IRepository<ProductEntity> _productRepository;
        IRepository<ProductBidEntity> _productBidRepository;
        IAsyncRepository<ProductBidEntity> _productBidAsync;

        public ProductBidService(IRepository<ProductBidEntity> productBidRepository, IAsyncRepository<ProductBidEntity> productBidAsync,
              IRepository<ProductEntity> productRepository)
        {
            this._productBidAsync = productBidAsync;
            this._productBidRepository = productBidRepository;
            this._productRepository = productRepository;
        }
        public List<ProductBidEntity> GetProductBid(int userId, int productId)
        {
            var query = _productBidRepository.ListAll();
            return query.Where(x => x.ProductId == productId && x.UserId == userId).ToList();

        }

        public decimal GetSumOfferByUserId(int userId, int currenctProductId)
        {
            decimal sum = 0;
            var productIds = _productBidRepository.ListAll().Where(x => x.ProductId != currenctProductId).GroupBy(x => x.ProductId).Select(x => new { id = x.Key }).ToList();
            foreach (var item in productIds)
            {
                int productId = Convert.ToInt32(item.id);
                var product = _productRepository.GetById(productId);
                if (product.ExpireDateTime > DateTime.Now)
                {
                    var productBid = _productBidRepository.ListAll().LastOrDefault(x => x.UserId == userId && x.ProductId == productId);
                    if (productBid !=null)
                    {
                        sum += productBid.Offer;
                    }
           
                }

            }

            return sum;
        }

        public async Task<bool> InsertProductBid(ProductBidEntity productBid)
        {
            await _productBidAsync.AddAsync(productBid);
            return true;
        }
    }
}
