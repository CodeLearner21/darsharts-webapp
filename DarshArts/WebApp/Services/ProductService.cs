using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    }
}
