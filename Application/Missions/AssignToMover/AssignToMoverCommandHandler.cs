using Domain.Mission;
using Domain.Mission.MissionValueObject;
using Domain.Movers;
using Domain.Movers.MoverValueObject;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.AssignToMover
{
    internal class AssignToMoverCommandHandler : IRequestHandler<AssignToMoverCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMoverRepository _moverRepository;
        private readonly IMissionRepository _missionRepository;

        public AssignToMoverCommandHandler(IUnitOfWork unitOfWork, IMoverRepository moverRepository, IMissionRepository missionRepository)
        {
            _unitOfWork = unitOfWork;
            _moverRepository = moverRepository;
            _missionRepository = missionRepository;
        }

        public async Task<Result> Handle(AssignToMoverCommand request, CancellationToken cancellationToken)
        {
            // Get Mover
            var mover = await _moverRepository.GetByIdAsync(request.moverId, cancellationToken);
            if (mover == null)
            {
                return Result.Failure(MoverError.NotFound);
            }
            
            // Get Mission
            var mission = await _missionRepository.GetByIdAsync(request.MissionId, cancellationToken);
            if (mission == null)
            {
                return Result.Failure(MissionError.NotFound);
            }

            // Assign mover to mission
            var result = mission.AssignToMover(mover);
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
