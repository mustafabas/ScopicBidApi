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
       readonly IRepository<ProductEntity> _productRepository;

        public ProductService(IRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductEntity GetProductById(int id)
        {
            
            var query = _productRepository.GetById(id);
            
            return query;
        }

        public List<ProductEntity> GetProducts()
        {
            var query = _productRepository.ListAll();
     
            return query.ToList();
        }
    }

}
                                                                                                                                                                                 