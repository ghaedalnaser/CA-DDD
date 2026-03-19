using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movers.MoverValueObject
{
    public sealed class MoverId : ValueObject
    {
        public Guid Value { get; private set; }
        public MoverId(Guid value)
        {
            if(value == Guid.Empty)
                throw new ArgumentException("MoverId cannot be empty.", nameof(value));
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
