using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Entities
{ 
    public class ProductBidEntity:BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Offer { get; set; }
        public DateTime RecordDate { get; set; }

        public virtual ProductEntity Product { get; set; }

    }
}
