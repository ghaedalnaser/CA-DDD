using Domain.Items;
using Domain.Items.ItemValueObjects;
using Domain.Movers.MoverValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movers
{
    public interface IMoverRepository
    {

        Task<Mover?> GetByIdAsync(MoverId Id, CancellationToken cancellationToken = default);
        Task<List<Mover>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Mover mover, CancellationToken cancellationToken = default);

    }
}
