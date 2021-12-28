using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.ViewModels;
using Catalog.Data.Repositories;
using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Services
{
    public class ProductService : BaseCatalogService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(
            IProductRepository productRepository,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase> CreateProduct(ProductDto productDto)
        {
            if (_productRepository.GetProductByName(productDto.Name) != null)
                return ResponseBase.AddError("PRODUCT_WITH_THE_SAME_NAME_ALREADY_EXISTS");

            Product product = new Product();

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Category = productDto.Category;
            product.Image = productDto.Image;
            product.Price = productDto.Price;
            product.CreateDate = DateTime.UtcNow;

            _productRepository.CreateProduct(product);
            //_productRepository.Save();
            if (product.Id == null)
                return ResponseBase.AddError("ERROR_TO_ADD_CHARACTERISTIC");
            return ResponseBase;
        }

        public Task<bool> DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProduct(Guid id)
        {
            var response = _productRepository.GetProduct(id);
            return _mapper.Map<ProductDto>(response);
        }

        public async Task<IEnumerable<ProductDto>> GetProductByCategory(string categoryName)
        {
            var response = _productRepository.GetProductByCategory(categoryName);
            return _mapper.Map<List<ProductDto>>(response);
        }

        public async Task<IEnumerable<ProductDto>> GetProductByName(string name)
        {
            var response = _productRepository.GetProductByName(name);
            return _mapper.Map<List<ProductDto>>(response);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var response = _productRepository.GetProducts();
            return _mapper.Map<List<ProductDto>>(response);
        }

        public Task<ResponseBase> UpdateProduct(ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
