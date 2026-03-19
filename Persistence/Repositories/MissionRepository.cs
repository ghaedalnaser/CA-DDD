using Domain.Mission;
using Domain.Mission.MissionValueObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class MissionRepository : IMissionRepository
    {
        private readonly AppDbContext _context;
        public MissionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Mission mission, CancellationToken cancellationToken)
        {
            await _context.Missions.AddAsync(mission, cancellationToken);
        }
        public async Task<Mission?> GetByIdAsync(MissionId Id, CancellationToken cancellationToken)
        {
            return await _context.Missions
                .Include("Items")
                .Include("ActivityLogs")
                .FirstOrDefaultAsync(m => m.Id == Id.Value, cancellationToken);
        }
        public async Task<List<Mission>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Missions
                .Include("Items")
                .Include("ActivityLogs")
                .ToListAsync(cancellationToken);
        }
    }
 }
