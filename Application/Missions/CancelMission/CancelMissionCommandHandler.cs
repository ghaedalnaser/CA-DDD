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

namespace Application.Missions.CancelMission
{
    internal class CancelMissionCommandHandler : IRequestHandler<CancelMissionCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMissionRepository _missionRepository;
        private readonly IMoverRepository _moverRepository;

        public CancelMissionCommandHandler(IUnitOfWork unitOfWork, IMissionRepository missionRepository, IMoverRepository moverRepository)
        {
            _unitOfWork = unitOfWork;
            _missionRepository = missionRepository;
            _moverRepository = moverRepository;
        }

        public async Task<Result> Handle(CancelMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = await _missionRepository.GetByIdAsync(request.MissionId, cancellationToken);
            if (mission == null)
                return Result.Failure(MissionError.NotFound);

            Mover? mover = null;
            if (mission.MoverId != null)
            {
                mover = await _moverRepository.GetByIdAsync(mission.MoverId, cancellationToken);
                if (mover == null)
                    return Result.Failure(MoverError.NotFound);
            }

            var result = mission.CancelMission(mover);
            if (result.IsFailure)
                return Result.Failure(result.Error);
                
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
