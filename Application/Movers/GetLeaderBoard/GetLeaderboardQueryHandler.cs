using Domain.Movers;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.GetLeaderBoard
{
     class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQuery , Result<List<LeaderboardResponse>>>
    {
        private readonly IMoverRepository _moverRepository;
        public GetLeaderboardQueryHandler(IMoverRepository moverRepository)
        {
            _moverRepository = moverRepository;
        }
        public async Task<Result<List<LeaderboardResponse>>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
        {
            var movers = await _moverRepository.GetAllAsync(cancellationToken);


            var leaderboard = movers
                .OrderByDescending(m => m.CompletedMissionsCount)
                .Select(m => new LeaderboardResponse(
                    m.Id,
                    m.WeightLimit.Value,
                    m.Energy.Value,
                    m.Status.ToString(),
                    m.CompletedMissionsCount))
                .ToList();

            return leaderboard;
        }

    }
}
