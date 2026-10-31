using Orderly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        void Update(Product product);
        Task<Product?> GetByIdAsync(int id);
    }
}
