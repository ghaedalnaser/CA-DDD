using Domain.Primitives;
using MediatR;
using System;

namespace Application.Missions.CreateMission
{
    public sealed record CreateMissionCommand() : IRequest<Result<Guid>>;
}
