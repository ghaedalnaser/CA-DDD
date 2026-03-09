using Application.Items.GetItem;
using Domain.Items.ItemValueObjects;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items.GetItemById
{
     public sealed record GetItemByIdQuery(ItemId Id) : IRequest<Result<GetItemResponse>>
    {
    }
}
