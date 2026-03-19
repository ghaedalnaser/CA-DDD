using Domain.Items.ItemValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items.GetItem
{
    // This record represents the response for the GetItemQuery. It contains the properties that will be returned to the client when they request an item.
    public sealed record GetItemResponse
    (
        Guid Id,
        string Name,
        decimal Weight,
        string Status
    );
}
