using Domain.Items.ItemValueObjects;
using Domain.Mission.MissionValueObject;
using Domain.Movers.MoverValueObject;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Missions.LoadItem
{
     public sealed record LoadItemCommand(ItemId ItemId, MissionId MissionId) : IRequest<Result>
    {
    }
}
