using Domain.Items.ItemValueObjects;
using Domain.Movers.MoverValueObject;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movers.CreateMover
{
    public record CreateMoverCommand(decimal WeightLimit ,decimal Energy ) : IRequest<Result<Guid>>
    {
    }
}
