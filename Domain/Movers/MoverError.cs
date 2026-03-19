using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movers
{
    public class MoverError
    {
        public static readonly Error NotFound = new(
               "Mover.NotFound", "The mover with the specified ID was not found.");

        public static readonly Error InvalidEnergy = new(
            "Mover.InvalidEnergy", "Mover energy must be greater than zero.");

        public static readonly Error InvalidWeightLimit = new(
            "Mover.InvalidWeightLimit", "Mover weightlimit must be greater than zero.");

        public static readonly Error NotResting = new(
            "Mover.NotResting",
            "The mover must be in 'Resting' state to perform this operation.");

        public static readonly Error AlreadyOnMission = new(
            "Mover.AlreadyOnMission", "Mover is already assigned to a mission.");

        public static readonly Error NoMissionAssigned = new(
            "Mover.NoMissionAssigned", "Mover is not currently assigned to any mission.");

        public static readonly Error CannotStartMission = new(
            "Mover.CannotStartMission", "Cannot start mission. Mover must be in Loading state.");

        public static readonly Error NotOnMission = new(
            "Mover.NotOnMission", "Mover is not currently on a mission.");

    }
}
