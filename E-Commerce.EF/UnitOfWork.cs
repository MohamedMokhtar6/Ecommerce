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
        public IBaseRepository<Cart> Cart { get; private set; }
        public IBaseRepository<ApplicationUser> Users { get; private set; }
        public IBaseRepository<CartItem> CartItem { get; private set; }
        public IBaseRepository<OrderItem> OrderItem { get; private set; }
        public IBaseRepository<Order> Order { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Brand = new BaseRepository<Brand>(_context);
            Category = new BaseRepository<Category>(_context);
            Product = new BaseRepository<Product>(_context);
            Cart = new BaseRepository<Cart>(_context);
            Users = new BaseRepository<ApplicationUser>(_context);
            CartItem = new BaseRepository<CartItem>(_context);
            OrderItem = new BaseRepository<OrderItem>(_context);
            Order = new BaseRepository<Order>(_context);
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