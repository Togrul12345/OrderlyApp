using Orderly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Application.Repositories
{
    public interface IRestourantRepository
    {
        Task<Restaurant?> GetByIdAsync(int id);
    }
}
