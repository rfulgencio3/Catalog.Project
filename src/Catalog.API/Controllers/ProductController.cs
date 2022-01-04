using Catalog.Application.DTOs;
using Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet(Name = nameof(GetProducts))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProducts();
            return StatusCode(200, products);
        }

        [HttpGet("name", Name = nameof(GetProductByName))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByName(string name)
        {
            var products = await _productService.GetProductByName(name);
            return StatusCode(200, products);
        }

        [HttpGet("category", Name = nameof(GetProductByCategory))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByCategory(string category)
        {
            var products = await _productService.GetProductByName(category);
            return StatusCode(200, products);
        }

        [HttpPost(Name = nameof(PostProduct))]
        public async Task<IActionResult> PostProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var response = await _productService.CreateProduct(productDto);

                if (response.Errors.Count > 0) return StatusCode(404, response.Errors);
                return StatusCode(201, response);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut(Name = nameof(UpdateProduct))]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                var response = await _productService.UpdateProduct(productDto);

                if (response.Errors.Count > 0) return StatusCode(404, response.Errors);
                return StatusCode(201, response);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
