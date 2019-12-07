using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Stocks.Forms;
using WebApp.Areas.Admin.ViewModels.Stocks.Pages;
using WebApp.Data.Interfaces;
using WebApp.Entities;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StockService> _logger;
        public StockService(IStockRepository stockRepository, IProductRepository productRepository, IMapper mapper, ILogger<StockService> logger)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<StockDetailsDto>> GetAllAsync()
        {
            try
            {
                var stocks = await _stockRepository.GetAllAsync();
                if (stocks != null)
                    return _mapper.Map<IEnumerable<StockDetailsDto>>(stocks);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in stock service {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<StockFormViewModel> GetEditFormDataAsync(string id)
        {
            try
            {
                var stock = await _stockRepository.GetOneAsync(id);
                if (stock == null)
                    return null;

                return _mapper.Map<StockFormViewModel>(stock);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in stock service: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveStockAsync(StockFormViewModel stockForm)
        {
            try
            {
                var stock = _mapper.Map<Stock>(stockForm);
                var result = await _stockRepository.SaveAndReturnIdAsync(stock);

                if (!string.IsNullOrWhiteSpace(result))
                    return result;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in StockService: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(string.Format("Exception occur in StockService: {0}", ex.Message));
            }
        }

        public async Task<bool> UpdateAsync(StockFormViewModel stockForm)
        {
            try
            {                
                var updateStock = _mapper.Map<Stock>(stockForm);
                updateStock.Product = await _productRepository.GetOneAsync(stockForm.ProductId);
                return await _stockRepository.UpdateStock(updateStock);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception in stock service: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
                
        public async Task<bool> DeleteAsync(string stockId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stockId))
                    return false;

                var stockToRemove = await _stockRepository.GetOneAsync(stockId);
                stockToRemove.Product = await _productRepository.GetOneAsync(stockToRemove.ProductId.ToString());

                return await _stockRepository.DeleteAsync(stockToRemove);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception in stock service: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
