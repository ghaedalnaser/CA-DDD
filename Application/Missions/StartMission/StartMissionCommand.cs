using Domain.Mission.MissionValueObject;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.StartMission
{
    public sealed record StartMissionCommand(MissionId MissionId) : IRequest<Result>
    {
    }
}
