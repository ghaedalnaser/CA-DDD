using Domain.Items.ItemValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Items
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(ItemId  Id, CancellationToken cancellationToken = default);
        Task <List<Item>> GetAllAsync(CancellationToken cancellationToken=default);
        Task AddAsync(Item item, CancellationToken cancellationToken = default);

    }
}
