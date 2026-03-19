using MediatR;
using Domain.Primitives;
using Domain.Mission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Mission.MissionValueObject;

namespace Application.Missions.CreateMission
{
    internal class CreateMissionCommandHandler : IRequestHandler<CreateMissionCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMissionRepository _missionRepository;

        public CreateMissionCommandHandler(IUnitOfWork unitOfWork, IMissionRepository missionRepository)
        {
            _unitOfWork = unitOfWork;
            _missionRepository = missionRepository;
        }
        public async Task<Result<Guid>> Handle(CreateMissionCommand request, CancellationToken cancellationToken)
        {
            //Create mission entity and save to database
            var mission = Mission.Create(
                 missionId: new MissionId(Guid.NewGuid())
                );

            await _missionRepository.AddAsync(mission ,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(mission.Id);
        }
    }
}
