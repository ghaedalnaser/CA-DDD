using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mission
{
    public enum MissionStatus
    {
        Planned = 0,
        Loading = 1,
        OnMission = 2,
        Completed = 3,
        Cancelled = 4,
        Failed = 5
    };
}
