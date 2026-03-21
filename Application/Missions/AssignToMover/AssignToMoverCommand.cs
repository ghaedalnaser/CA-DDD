using Domain.Mission.MissionValueObject;
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
     public sealed record AssignToMoverCommand(MissionId MissionId, MoverId moverId, string IdempotencyKey) : IRequest<Result>, IIdempotentCommand
    {
    }
}
