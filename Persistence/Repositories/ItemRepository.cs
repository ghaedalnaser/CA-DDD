using Domain.Items;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Persistence.Repositories
{
    internal class ItemRepository : IItemRepository
    {

        private readonly AppDbContext _dbContext;
        public ItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Item item , CancellationToken cancellationToken)
        {
            await _dbContext.Items.AddAsync(item , cancellationToken);
        }

        public async Task<List<Item>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Items.ToListAsync(cancellationToken);
        }
    }
}
