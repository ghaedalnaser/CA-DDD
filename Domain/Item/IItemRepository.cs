using Domain.Item.ItemValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Item
{
    public interface IItemRepository
    {
        //Task<ItemEntity> GetByIdAsync(ItemId id);

        //Task <List<ItemEntity>> GetAllAsync();
        Task AddAsync(ItemEntity item, CancellationToken cancellationToken = default);

        //Task UpdateAsync(ItemEntity item);
        //Task DeleteAsync(ItemId id);
    }
}
