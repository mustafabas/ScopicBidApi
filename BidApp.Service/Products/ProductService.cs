using System;
using System.Collections.Generic;
using System.Text;
using BidApp.Entities;
using System.Linq;
using BidApp.DataL;

namespace BidApp.Service.Products
{
    public class ProductService:IProductService
    {
       readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetProducts()
        {
            var query = _productRepository.ListAll();
            return query.ToList();
        }
    }

}
                                                                                                                                                                                 