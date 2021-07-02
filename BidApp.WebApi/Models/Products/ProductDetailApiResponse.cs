using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidApp.WebApi.Models.Products
{
    public class ProductDetailApiResponse:ProductApiResponse
    {
        public ProductDetailApiResponse()
        {
            this.ProductBidResponses = new List<ProductBidResponse>();
        }
        public List<ProductBidResponse> ProductBidResponses { get; set; }
    }
}
