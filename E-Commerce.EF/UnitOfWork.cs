using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using E_Commerce.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;


        public IBaseRepository<Brand> BrandRepo { get;private set; }

        public IBaseRepository<Category> CategoryRepo { get; private set; }

        public IBaseRepository<Product> ProductRepo { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            BrandRepo = new BaseRepository<Brand>(_context);
            CategoryRepo = new BaseRepository<Category>(_context);
            ProductRepo = new BaseRepository<Product>(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
