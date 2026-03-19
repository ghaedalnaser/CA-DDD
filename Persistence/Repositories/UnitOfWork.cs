using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;

namespace Persistence.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return  _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
