using Domain.Mission.MissionValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mission
{
    public interface IMissionRepository
    {
        public Task<Mission?> GetByIdAsync(MissionId id, CancellationToken cancellationToken = default);
        public Task<List<Mission>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task AddAsync(Mission mission, CancellationToken cancellationToken = default);

       // public Task UpdateAsync(Mission mission, CancellationToken cancellationToken = default);
       // public Task DeleteAsync(Mission mission, CancellationToken cancellationToken = default);    
    }
}
