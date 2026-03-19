using Domain.Movers;
using Domain.Movers.MoverValueObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Persistence.Repositories

{
    internal class MoverRepository : IMoverRepository
    {
        private readonly AppDbContext _dbcontext;
        public MoverRepository(AppDbContext context)
        {
            _dbcontext = context;
        }
        public async Task AddAsync(Mover mover, CancellationToken cancellationToken = default)
        {
            await _dbcontext.Movers.AddAsync(mover, cancellationToken);
        }
        public async Task<Mover?> GetByIdAsync(MoverId Id, CancellationToken cancellationToken = default)
        {
            return await _dbcontext.Movers
                .FirstOrDefaultAsync(m => m.Id == Id.Value, cancellationToken);
        }
        public async Task<List<Mover>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbcontext.Movers.ToListAsync(cancellationToken);
        } 
    }
}

