using BidApp.Service.Products;
using BidApp.Service.Users;
using BidApp.WebApi.Models;
using BidApp.WebApi.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;
        readonly IUserService _userService;

        private static string IMAGE_BASE_URL = "https://localhost:44382/Images/";
        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }
        [HttpGet("[action]")]
        public IActionResult getAll()
        {
            ResponseModel<List<ProductApiResponse>> responseModel = new ResponseModel<List<ProductApiResponse>>();
            List<ProductApiResponse> productList = new List<ProductApiResponse>();
            var products = _productService.GetProducts();
            foreach (var product in products)
            {
                productList.Add(new ProductApiResponse
                {
                    Description = product.Description,
                    Name = product.Name,
                    PhotoPath = IMAGE_BASE_URL + product.PhotoPath,
                    StartPrice = product.CurrentPrice.Value,
                    Id = product.Id,
                    ExpireDateTime =product.ExpireDateTime,

                });
            }
            responseModel.SetOk(productList);
            return Ok(responseModel);
        }

        [HttpGet]
        public IActionResult get(int id)
        {
            ResponseModel<ProductDetailApiResponse> response = new ResponseModel<ProductDetailApiResponse>();

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                response.Message = "Product Not Found";
                response.SetNok();
            }
            else
            {
                ProductDetailApiResponse productDetail = new ProductDetailApiResponse();
                productDetail.Id = product.Id;
                productDetail.Description = product.Description;
                productDetail.Name = product.Name;
                productDetail.PhotoPath = IMAGE_BASE_URL+product.PhotoPath;
                productDetail.StartPrice = product.StartPrice;
                productDetail.ExpireDateTime = product.ExpireDateTime;
                foreach (var item in product.ProductBids)
                {
                    productDetail.ProductBidResponses.Add(new ProductBidResponse
                    {
                        Price = item.Offer,
                        UserName = _userService.GetUserById(item.UserId).UserName,
                        RecordDate = item.RecordDate,
                        UserId=item.UserId
                    });
    
                }
                response.SetOk(productDetail);
             
            }

            return Ok(response);

        }

    }
}
