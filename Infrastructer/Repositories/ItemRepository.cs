using Domain.Item;
using Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infrastructer.Repositories
{
    internal class ItemRepository : IItemRepository
    {

        private readonly AppDbContext _dbContext;
        public ItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ItemEntity item , CancellationToken cancellationToken)
        {
            await _dbContext.Item.AddAsync(item , cancellationToken);
        }

    }
}
