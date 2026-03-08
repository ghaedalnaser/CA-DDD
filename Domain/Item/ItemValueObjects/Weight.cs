using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Item.ItemValueObjects
{
    public sealed class Weight : ValueObject
    {
        public decimal Value { get; private set; }
        public Weight(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Weight cannot be negative.", nameof(value));
            Value = value;
        }
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
