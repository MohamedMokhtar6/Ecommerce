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


        public IBaseRepository<Brand> Brand { get; private set; }

        public IBaseRepository<Category> Category { get; private set; }

        public IBaseRepository<Product> Product { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Brand = new BaseRepository<Brand>(_context);
            Category = new BaseRepository<Category>(_context);
            Product = new BaseRepository<Product>(_context);
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