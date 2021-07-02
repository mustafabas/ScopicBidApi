using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidApp.WebApi.Models.Products
{
    public class ProductApiResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartPrice { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDateTime { get; set; }
    }
}
