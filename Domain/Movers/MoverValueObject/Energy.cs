using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movers.MoverValueObject
{
    public sealed class  Energy  : ValueObject
    {
        public decimal Value { get; private set; }
        public Energy(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Energy must be greater than zero .", nameof(value));
            Value = value;
        } 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
