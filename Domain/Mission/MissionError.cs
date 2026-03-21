using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mission
{
    public class MissionError
    {
        public static readonly Error NotFound = new(
             "Mission.NotFound", "The mission with the specified ID was not found.");

        public static readonly Error ItemAlreadyLoaded = new(
                  "Mission.ItemAlreadyLoaded", 
                  "This item is already loaded on the mission.");

        public static readonly Error InvalidStatus = new(
                  "Mission.InvalidStatus", 
                  "Mission is not in the correct status for this operation.");

        public static readonly Error CannotCancelInCurrentState = new(
                  "Mission.CannotCancelInCurrentState", 
                  "Mission can only be cancelled if it's Planned or Loading.");

        public static readonly Error ExceedsWeightLimit = new(
                  "Mission.ExceedsWeightLimit", 
                  "Total weight exceeds the mover's weight limit.");

        public static readonly Error WeightLimitExceeded = new(
                  "Mission.WeightLimitExceeded", 
                  "Item cannot be loaded. Weight limit would be exceeded.");

        public static readonly Error InsufficientEnergy = new(
                  "Mission.InsufficientEnergy", 
                  "Total energy required exceeds the mover's available energy.");

        public static readonly Error MoverNotResting = new(
                  "Mission.MoverNotResting", 
                  "Mover must be in Resting status to start a mission.");

        public static readonly Error MoverNotLoading = new(
              "Mission.MoverNotLoading",
              "Mover must be in Loading status to start a mission.");

        public static readonly Error NoItemsLoaded = new(
                  "Mission.NoItemsLoaded", 
                  "Cannot start mission without any loaded items.");
        public static readonly Error MoverAlreadyAssigned = new(
           "Mission.MoverAlreadyAssigned",
           "Mission already has a mover assigned.");

        public static readonly Error MoverNotAssigned = new(
            "Mission.MoverNotAssigned",
            "Mission has no mover assigned.");

        public static readonly Error InvalidMover = new(
            "Mission.InvalidMover",
            "The provided mover does not belong to this mission.");

        public static readonly Error Conflict = new(
            "Mission.Conflict", "The mission was modified by another request. Please retry.");
    }
}
