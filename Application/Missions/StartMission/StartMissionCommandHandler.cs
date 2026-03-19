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

namespace Application.Missions.StartMission
{
    internal class StartMissionCommandHandler : IRequestHandler<StartMissionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMissionRepository _missionRepository;
        private readonly IMoverRepository _moverRepository;

        public StartMissionCommandHandler(IUnitOfWork unitOfWork, IMissionRepository missionRepository, IMoverRepository moverRepository)
        {
            _unitOfWork = unitOfWork;
            _missionRepository = missionRepository;
            _moverRepository = moverRepository;
        }
        
        public async Task<Result> Handle(StartMissionCommand command, CancellationToken cancellationToken)
        { 
            var mission = await _missionRepository.GetByIdAsync(command.MissionId, cancellationToken);
            if (mission is null)
            {
                return Result.Failure(MissionError.NotFound);
            }
            
            if (mission.MoverId == null)
            {
                return Result.Failure(MissionError.MoverNotAssigned);
            }
            
            var mover = await _moverRepository.GetByIdAsync(mission.MoverId, cancellationToken);
            if (mover is null)
            {
                return Result.Failure(MoverError.NotFound);
            }
            
            var result = mission.StartMission(mover);
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
