using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;
using Domain.Items;
using MediatR;
using Domain.Items.ItemValueObjects;
namespace Application.Items.GetItem
{
    // This record represents the query for getting items. 
    // If Id is provided, returns single item; if null, returns all items
    public sealed record GetItemQuery(ItemId? Id) : IRequest<Result<List<GetItemResponse>>>
    {
    }
}
