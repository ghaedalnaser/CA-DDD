using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mission.MissionValueObject
{
    public sealed  class   MissionId : ValueObject
    {

        public Guid Value { get; private set; }
        public MissionId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("MissionId cannot be empty.", nameof(value));
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
