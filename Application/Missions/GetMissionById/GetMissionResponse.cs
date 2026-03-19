using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.GetMissionById
{
    public sealed record GetMissionResponse
    (
        Guid Id,
        string Status,
        MoverResponse? Mover,
        List<ItemResponse> Items,
        DateTime Timestamp 
    );

    public sealed record MoverResponse
    (
        Guid Id,
        decimal Energy,
        decimal WeightLimit,
        string Status
    );

    public sealed record ItemResponse
    (
        Guid Id,
        string Name,
        decimal Weight,
        string Status
    );
}
