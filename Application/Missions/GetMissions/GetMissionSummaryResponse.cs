using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.GetMissions
{
    public sealed record GetMissionSummaryResponse
    (
        Guid Id,
        string Status,
        Guid? MoverId,
        int ItemCount,
        decimal TotalWeight,
        DateTime Timestamp
    );
}
