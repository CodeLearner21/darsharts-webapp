using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.ViewModels.Products.Forms;
using WebApp.Data.Interfaces;
using WebApp.Entities;

namespace WebApp.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            if (products != null && products.Count > 0)
                return products;

            return null;
        }

        public async Task<Product> GetOneAsync(string id)
        {
            var product = await _context.Products.FindAsync(Guid.Parse(id));
            if (product != null)
                return product;

            return null;
        }

        public async Task<string> SaveAsync(Product product)
        {            
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
            return product.Id.ToString();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(p => p.Id == Guid.Parse(id));
                _context.Products.Remove(product);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
