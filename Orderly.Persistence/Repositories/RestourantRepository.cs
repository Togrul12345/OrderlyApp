using Microsoft.EntityFrameworkCore;
using Orderly.Application.Repositories;
using Orderly.Domain.Entities;
using Orderly.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Persistence.Repositories
{
    public class RestourantRepository : IRestourantRepository
    {
        private readonly AppDbContext _context;

        public RestourantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
