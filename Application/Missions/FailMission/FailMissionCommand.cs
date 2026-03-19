using Domain.Mission.MissionValueObject;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.FailMission
{
    public sealed record FailMissionCommand(MissionId MissionId) : IRequest<Result>
    {
    }
}
