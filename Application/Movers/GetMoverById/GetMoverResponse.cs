using Domain.Movers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.GetMoverById
{
    public sealed record GetMoverResponse
    (
        Guid Id,
        decimal Energy,
        decimal WeightLimit,
        string Status
    );
}
