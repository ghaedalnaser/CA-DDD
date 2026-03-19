using Application.Movers.GetMoverById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;

namespace Application.Movers.GetMovers
{
    public record GetMoversQuery : IRequest<Result<List<GetMoverResponse>>>
    {

    }
}
