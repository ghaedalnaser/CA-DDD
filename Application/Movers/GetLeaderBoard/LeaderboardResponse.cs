using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.GetLeaderBoard
{
     public record LeaderboardResponse(
        Guid Id,
        decimal WeightLimit,
        decimal Energy,
        string Status,
        int CompletedMissionsCount
         );
}
