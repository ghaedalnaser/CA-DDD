using Domain.Item.ItemValueObjects;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Item
{
    public class ItemEntity : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public Weight Weight { get; private set; } = null! ;
        private ItemEntity(ItemId id, string name, Weight weight) : base(id.Value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            Name = name;
            Weight = weight ?? throw new ArgumentNullException(nameof(weight));
        }
        // Parameterless constructor for EF Core
        private ItemEntity() : base(Guid.Empty) { }

        public static ItemEntity Create(ItemId id ,string name, Weight weight)
        {
            return new ItemEntity(id ,name, weight);
        }
    }

}
