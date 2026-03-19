using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.GetLeaderBoard
{
     public sealed  record GetLeaderboardQuery : IRequest<Result<List<LeaderboardResponse>>>
    {
    }
}
