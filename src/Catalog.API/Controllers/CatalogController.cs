using Catalog.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Catalog.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
    }
}
