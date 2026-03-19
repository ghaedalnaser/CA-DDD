using Domain.ActivityLogs;
using Domain.Items.ItemValueObjects;
using Domain.Mission.MissionValueObject;
using Domain.Movers.MoverValueObject;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movers
{
    public sealed class Mover : Entity
    {

        public Energy Energy { get; private set; } = null!;
        public Weight WeightLimit { get; private set; } = null!;
        public MoverStatus Status { get; private set; }
        public MissionId? CurrentMissionId { get; private set; }
        public int CompletedMissionsCount { get; private set; }


        //constructors
        private Mover() : base(Guid.Empty) { }
        
        private Mover(MoverId id, Energy energy, Weight weightLimit) : base(id.Value)
        {
            Energy = energy ?? throw new ArgumentNullException(nameof(energy));
            WeightLimit = weightLimit ?? throw new ArgumentNullException(nameof(weightLimit));
            Status = MoverStatus.Resting;
            CurrentMissionId = null;
            CompletedMissionsCount = 0;
        }

        //factory method
        public static Mover Create(MoverId id, Energy energy, Weight weightLimit)
        {
            return new Mover(id, energy, weightLimit);
        }

        public Result Assign(MissionId missionId)
        {
            if (CurrentMissionId != null)
                return Result.Failure(MoverError.AlreadyOnMission);

            CurrentMissionId = missionId;

            return Result.Success();
        }

        public Result SetOnMission()
        {
            if (CurrentMissionId == null)
                return Result.Failure(MoverError.NoMissionAssigned);

            Status = MoverStatus.OnMission;

            return Result.Success();
        }

        public Result CompleteMission()
        {
            CompletedMissionsCount++;
            return SetResting();
        }

        public Result SetResting()
        {
          
            Status = MoverStatus.Resting;
            CurrentMissionId = null;

            return Result.Success();
        }
    }
}
