using System;
using System.Collections.Generic;

namespace BidApp.Entities
{
    public class ProductEntity:BaseEntity
    {
        public string Name { get; set; }
        public  decimal StartPrice { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public decimal? CurrentPrice { get; set; }
        public virtual List<ProductBidEntity> ProductBids { get; set; }

    }
}
