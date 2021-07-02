using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidApp.WebApi.Models.Products
{
    public class ProductBidResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Price { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
