using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.GetMissions
{
    public sealed record GetMissionsQuery() : IRequest<Result<List<GetMissionSummaryResponse>>>
    {
    }
}
