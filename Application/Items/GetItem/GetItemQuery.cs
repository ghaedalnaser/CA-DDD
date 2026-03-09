using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;
using Domain.Items;
using MediatR;
namespace Application.Items.GetItem

{
    // This record represents the query for getting items. 
    public sealed record GetItemQuery : IRequest<Result<List<GetItemResponse>>>
    {
    }
}
