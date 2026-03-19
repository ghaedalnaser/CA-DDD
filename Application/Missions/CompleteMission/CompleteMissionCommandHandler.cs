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

namespace Application.Missions.CompleteMission
{
    internal sealed class CompleteMissionCommandHandler : IRequestHandler<CompleteMissionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMissionRepository _missionRepository;
        private readonly IMoverRepository _moverRepository;

        public CompleteMissionCommandHandler(
            IUnitOfWork unitOfWork, 
            IMissionRepository missionRepository,
            IMoverRepository moverRepository)
        {
            _unitOfWork = unitOfWork;
            _missionRepository = missionRepository;
            _moverRepository = moverRepository;
        }

        public async Task<Result> Handle(CompleteMissionCommand request, CancellationToken cancellationToken)
        {
            // Get the mission by ID
            var mission = await _missionRepository.GetByIdAsync(request.MissionId, cancellationToken);
            if (mission is null)
            {
                return Result.Failure(MissionError.NotFound);
            }

            // Get the assigned mover
            if (mission.MoverId == null)
            {
                return Result.Failure(MissionError.MoverNotAssigned);
            }

            var mover = await _moverRepository.GetByIdAsync(mission.MoverId, cancellationToken);
            if (mover is null)
            {
                return Result.Failure(MoverError.NotFound);
            }

            // Complete the mission
            var result = mission.CompleteMission(mover);
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
