using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Products.Forms;
using WebApp.Data.Interfaces;
using WebApp.Entities;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDetailsDto>> GetAllAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                if (products != null)
                    return _mapper.Map<IEnumerable<ProductDetailsDto>>(products);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while getting products {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductDetailsDto> GetById(string id)
        {
            try
            {
                var product = await _productRepository.GetOneAsync(id);
                if (product == null)
                    return null;

                return _mapper.Map<ProductDetailsDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception while getting product by id: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductFormViewModel> EditById(string id)
        {
            try
            {
                var product = await _productRepository.GetOneAsync(id);
                if (product == null)
                    return null;

                return _mapper.Map<ProductFormViewModel>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception while getting product by id: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveAndGetIdAsync(ProductFormViewModel productForm)
        {
            try
            {
                productForm.DateCreated = DateTime.Now;
                productForm.DateUpdated = DateTime.Now;
                var product = _mapper.Map<Product>(productForm);
                var productId = await _productRepository.SaveAsync(product);
                if (string.IsNullOrEmpty(productId))
                    return null;

                return productId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while saving product: {0} at {1}", ex.Message, DateTime.Now.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(ProductFormViewModel productForm)
        {
            try
            {
                var product = _mapper.Map<Product>(productForm);
                var result = await _productRepository.UpdateAsync(product);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception while updating product {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                return await _productRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while deleting product: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
