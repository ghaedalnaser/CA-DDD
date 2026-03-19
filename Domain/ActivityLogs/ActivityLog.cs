using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ActivityLogs
{
    public class ActivityLog : Entity
    {
        public new Guid Id { get; private set; }
        public string ActivityType { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime Timestamp { get; private set; }

        private ActivityLog() : base(Guid.Empty)
        {
            ActivityType = string.Empty;
            Description = string.Empty;
            Timestamp = DateTime.UtcNow;
        }
        
        private ActivityLog(Guid id, string activityType, string description, DateTime timestamp) : base(id)
        {
            ActivityType = activityType;
            Description = description;
            Timestamp = timestamp;
        }
        public static ActivityLog Create(string activityType, string description)
        {
                return new ActivityLog(Guid.NewGuid(), activityType, description, DateTime.UtcNow);
        }
    }
}
