using AutoMapper;
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

        public async Task<string> SaveAsync(Product product)
        {            
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
            return product.Id.ToString();
        }
    }
}
