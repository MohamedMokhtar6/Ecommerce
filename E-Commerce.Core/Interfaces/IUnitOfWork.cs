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
        IBaseRepository<Brand> BrandRepo { get; }
        IBaseRepository<Category> CategoryRepo { get; }
        IBaseRepository<Product> ProductRepo { get; }

        int Complete();
    }
}
