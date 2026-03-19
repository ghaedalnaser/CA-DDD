using Domain.Movers;
using Domain.Movers.MoverValueObject;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.GetMoverById
{
    public record GetMoverByIDQuery(MoverId Id) : IRequest<Result<GetMoverResponse>>
    {
    }
}
