using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;

namespace Domain.Items
{
    public class ItemError
    {
       
         public static readonly Error NotFound = new(
                "Item.NotFound", "The item with the specified ID was not found.");

            public static readonly Error InvalidName = new(
                "Item.InvalidName", "Item name cannot be empty.");

            public static readonly Error InvalidWeight = new(
                "Item.InvalidWeight", "Item weight must be greater than zero.");
        
    }
}
