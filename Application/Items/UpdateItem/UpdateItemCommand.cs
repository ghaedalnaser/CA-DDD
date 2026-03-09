using MediatR;
using Domain.Items; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items.ItemValueObjects;
using Domain.Primitives;

namespace Application.Items.UpdateItem
{
    public sealed record UpdateItemCommand(ItemId Id , string Name , decimal Weight) : IRequest<Result>
    {
    }
    public sealed record UpdateItemRequest(string Name , decimal Weight);
}
