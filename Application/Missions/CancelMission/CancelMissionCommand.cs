using Domain.Mission.MissionValueObject;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.CancelMission
{
     public sealed record CancelMissionCommand(MissionId MissionId, string IdempotencyKey) : IRequest<Result>, IIdempotentCommand
    {
    }
}
