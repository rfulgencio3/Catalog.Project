using Catalog.Application.DTOs;
using Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(Guid id);
        Task<IEnumerable<ProductDto>> GetProductByName(string name);
        Task<IEnumerable<ProductDto>> GetProductByCategory(string categoryName);

        Task<ResponseBase> CreateProduct(ProductDto productDto);
        Task<ResponseBase> UpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(Guid id);
    }
}