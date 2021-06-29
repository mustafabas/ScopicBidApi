using BidApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BidApp.Service.Products
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
