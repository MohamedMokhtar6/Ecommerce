using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Brand> Brand { get; }
        IBaseRepository<Category> Category { get; }
        IBaseRepository<Product> Product { get; }
        IBaseRepository<Cart> Cart { get; }
        IBaseRepository<ApplicationUser> Users { get; }
        IBaseRepository<CartItem> CartItem { get; }
        IBaseRepository<Order> Order { get; }
        IBaseRepository<OrderItem> OrderItem { get; }

        int Complete();
    }
}