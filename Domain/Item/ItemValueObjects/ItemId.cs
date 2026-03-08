using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Item.ItemValueObjects
{
    public sealed class ItemId : ValueObject
    {
        public Guid Value { get; private set; }
        public ItemId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ItemId cannot be empty.", nameof(value));
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
