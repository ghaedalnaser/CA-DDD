using Domain.Mission;
using Domain.Mission.MissionValueObject;
using Domain.Movers;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.GetMissionById
{
    internal sealed class GetMissionByIdQueryHandler : IRequestHandler<GetMissionByIdQuery, Result<GetMissionResponse>>
    {
        private readonly IMissionRepository _missionRepository;
        private readonly IMoverRepository _moverRepository;

        public GetMissionByIdQueryHandler(
            IMissionRepository missionRepository,
            IMoverRepository moverRepository)
        {
            _missionRepository = missionRepository;
            _moverRepository = moverRepository;
        }

        public async Task<Result<GetMissionResponse>> Handle(GetMissionByIdQuery request, CancellationToken cancellationToken)
        {
            // Get mission by id
            var mission = await _missionRepository.GetByIdAsync(request.MissionId, cancellationToken);
            if (mission is null)
            {
                return Result.Failure<GetMissionResponse>(MissionError.NotFound);
            }

            // Get mover details if assigned
            MoverResponse? moverResponse = null;
            if (mission.MoverId != null)
            {
                var mover = await _moverRepository.GetByIdAsync(mission.MoverId, cancellationToken);
                if (mover != null)
                {
                    moverResponse = new MoverResponse(
                        mover.Id,
                        mover.Energy.Value,
                        mover.WeightLimit.Value,
                        mover.Status.ToString()
                    );
                }
            }

            // Format response
            var response = new GetMissionResponse(
                Id: mission.Id,
                Status: mission.Status.ToString(),
                Mover: moverResponse,
                Items: mission.Items.Select(item => new ItemResponse(
                    item.Id,
                    item.Name,
                    item.Weight.Value,
                    item.Status.ToString()
                )).ToList(),
                Timestamp: mission.Timestamp
            );

            return Result.Success(response);
        }
    }
}
