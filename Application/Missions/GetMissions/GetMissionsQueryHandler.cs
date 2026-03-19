using Domain.Mission;
using Domain.Movers;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.GetMissions
{
    internal sealed class GetMissionsQueryHandler : IRequestHandler<GetMissionsQuery, Result<List<GetMissionSummaryResponse>>>
    {
        private readonly IMissionRepository _missionRepository;
        private readonly IMoverRepository _moverRepository;

        public GetMissionsQueryHandler(
            IMissionRepository missionRepository,
            IMoverRepository moverRepository)
        {
            _missionRepository = missionRepository;
            _moverRepository = moverRepository;
        }

        public async Task<Result<List<GetMissionSummaryResponse>>> Handle(GetMissionsQuery request, CancellationToken cancellationToken)
        {
            // Get all missions
            var missions = await _missionRepository.GetAllAsync(cancellationToken);
            
            var response = missions.Select(mission => new GetMissionSummaryResponse(
                Id: mission.Id,
                Status: mission.Status.ToString(),
                MoverId: mission.MoverId?.Value,
                ItemCount: mission.Items.Count,
                TotalWeight: mission.Items.Sum(item => item.Weight.Value),
                Timestamp: mission.Timestamp
            )).ToList();

            return Result.Success(response);
        }
    }
}
