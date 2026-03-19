using Domain.ActivityLogs;
using Domain.Items;
using Domain.Items.ItemValueObjects;
using Domain.Mission.MissionValueObject;
using Domain.Movers;
using Domain.Movers.MoverValueObject;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mission
{
    public class Mission : AggregateRoot
    {
        private List<Item> _items = new();
        private List<ActivityLog> _activityLogs = new();
        public MoverId MoverId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public MissionStatus Status { get; private set; }
        public IReadOnlyCollection<Item> Items => _items.AsReadOnly();
        public IReadOnlyCollection<ActivityLog> ActivityLogs => _activityLogs.AsReadOnly();


        private Mission() : base(Guid.Empty)
        {
            MoverId = null!;
            Timestamp = DateTime.UtcNow;
            Status = MissionStatus.Planned;
        }

        private Mission(MissionId missionId) : base(missionId.Value)
        {
            Timestamp = DateTime.UtcNow;
            Status = MissionStatus.Planned;
        }
        //create a mission
        public static Mission Create(MissionId missionId)
        {
            return new Mission(missionId);
        }

        //assign mission to mover
        public Result AssignToMover(Mover mover)
        {
            //check if mission is in planned status
            if (Status != MissionStatus.Planned)
                return Result.Failure(MissionError.InvalidStatus);

            //check if mover is resting
            if (mover.Status != MoverStatus.Resting)
                return Result.Failure(MissionError.MoverNotResting);

            //check if mission already has a mover assigned
            if (MoverId != null)
                return Result.Failure(MissionError.MoverAlreadyAssigned);

            // Assuming the mover has a method to assign a mission, which would handle any necessary state changes for the mover
            var result = mover.Assign(new MissionId(Id));
            if(result.IsFailure)
                return result;

            MoverId = new MoverId(mover.Id);

            _activityLogs.Add(ActivityLog.Create(
                 "MissionAssigned",
                 $"Mission assigned to mover '{mover.Id}' with energy {mover.Energy.Value} and weight limit {mover.WeightLimit.Value}"));
            return Result.Success();
        }
        //load item to mission 
        public Result LoadItem(Item item, Mover mover)
        {   
            //check if the mover is assigned to this mission
            if (MoverId == null || MoverId.Value != mover.Id)
                return Result.Failure(MissionError.InvalidMover);

            //check mission status
            if (Status != MissionStatus.Planned && Status != MissionStatus.Loading)
                return Result.Failure(MissionError.InvalidStatus);

            //check if item exists
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            //check item status
            if (item.Status != ItemStatus.Available)
                return Result.Failure(ItemError.NotAvailable);

            //check if item exists in mission items
            if (_items.Any(i => i.Id == item.Id))
                return Result.Failure(MissionError.ItemAlreadyLoaded);

            // Calculate current total weight of items in the mission
            var currentTotalWeight = _items.Sum(i => i.Weight.Value);

            // Check if adding the new item exceeds the mover's weight limit
            if (currentTotalWeight + item.Weight.Value > mover.WeightLimit.Value)
                return Result.Failure(MissionError.WeightLimitExceeded);

            // Reserve the item for this mission
            var missionId = new MissionId(Id);
            var reserveResult = item.ReserveForMission(missionId);
            if (reserveResult.IsFailure)
                return reserveResult;

            // Add item to mission
            _items.Add(item);
            Status = MissionStatus.Loading;

            //create an activity log for the load item action
            _activityLogs.Add(ActivityLog.Create(
                 "Loading",
                 $"Reserved item '{item.Name}' (weight: {item.Weight.Value}) for mission. Total weight: {currentTotalWeight + item.Weight.Value}/{mover.WeightLimit.Value}"
                 ));

            return Result.Success();
        }
        // start Mission
        public Result StartMission(Mover mover)
        {
            //check if the mover is assigned to this mission
            if (MoverId == null || MoverId.Value != mover.Id)
                return Result.Failure(MissionError.InvalidMover);

            //check Status for mission
            if (Status != MissionStatus.Loading)
                return Result.Failure(MissionError.InvalidStatus);

            //check Mover status
            if (mover.Status != MoverStatus.Resting)
                return Result.Failure(MissionError.MoverNotResting);

            //check if mission has items
            if (_items.Count == 0)
                return Result.Failure(MissionError.NoItemsLoaded);

            //calculate total weight and energy requirements
            var totalWeight = _items.Aggregate(0m, (sum, item) => sum + item.Weight.Value);
            // Assuming each item requires 1 unit of energy per weight unit - this can be refined
            var totalEnergyRequired = totalWeight;

            //check if mover has enough energy (this would need actual energy calculation logic)
            if (totalEnergyRequired > mover.Energy.Value)
                return Result.Failure(MissionError.InsufficientEnergy);

            //update mission status and mover status
             var result = mover.SetOnMission();
            if(result.IsFailure)
                return result;
             Status = MissionStatus.OnMission;


            _activityLogs.Add(ActivityLog.Create(
               "OnMission",
               $"Mission started with {_items.Count} items (total weight: {totalWeight}, energy required: {totalEnergyRequired})"
               ));

            return Result.Success();
        }
        //cancel the mission
        public Result CancelMission(Mover? mover = null)
        {
            //check mission state
            if (Status != MissionStatus.Loading && Status != MissionStatus.Planned)
                return Result.Failure(MissionError.CannotCancelInCurrentState);

            //if mover is assigned, validate it matches
            if (MoverId != null)
            {
                if (mover == null)
                    return Result.Failure(MissionError.InvalidMover);

                if (MoverId.Value != mover.Id)
                    return Result.Failure(MissionError.InvalidMover);
            }

            var itemsCount = _items.Count;

            //release all reserved items back to Available
            foreach (var item in _items)
            {
                item.SetAvailable();
            }

            //clear items and update status
            _items.Clear();
            Status = MissionStatus.Cancelled;

            //return mover to resting only if one was assigned
            if (mover != null)
            {
                var result = mover.SetResting();
                if (result.IsFailure)
                    return result;
            }

            _activityLogs.Add(ActivityLog.Create(
                 "MissionCancelled",
                 $"Mission '{Id}' cancelled. Released {itemsCount} items back to Available."));

            return Result.Success();
        }
        //complete the mission
        public Result CompleteMission(Mover mover)
        {
            if (MoverId == null || MoverId.Value != mover.Id)
                return Result.Failure(MissionError.InvalidMover);

            //check mission state
            if (Status != MissionStatus.OnMission)
                return Result.Failure(MissionError.InvalidStatus);

            //check mover state
            if (mover.Status != MoverStatus.OnMission)
                return Result.Failure(MoverError.NotOnMission);

            var itemsCount = _items.Count;

            //mark all items as consumed
            foreach (var item in _items)
            {
                item.SetConsumed();
            }

            //clear items and update status
            _items.Clear();
            //update mission status and mover status
             Status = MissionStatus.Completed;
             var result = mover.CompleteMission();
             if(result.IsFailure)
                return result;

            _activityLogs.Add(ActivityLog.Create(
                 "MissionCompleted",
                 $"Mission '{Id}' completed. Consumed {itemsCount} items."));
            return Result.Success();

        }
        //fail the mission
        public Result FailMission(Mover mover)
        {
            if (MoverId == null || MoverId.Value != mover.Id)
                return Result.Failure(MissionError.InvalidMover);
            //check mission state
            if (Status != MissionStatus.OnMission)
                return Result.Failure(MissionError.InvalidStatus);
            //check mover state
            if (mover.Status != MoverStatus.OnMission)
                return Result.Failure(MoverError.NotOnMission);

            var itemsCount = _items.Count;
            //mark all items as available again
            foreach (var item in _items)
            {
                item.SetAvailable();
            }
            //clear items and update status
            _items.Clear();
            //update mission status and mover status
            Status = MissionStatus.Failed;
            var result = mover.SetResting();
            if(result.IsFailure)
                return result;

            _activityLogs.Add(ActivityLog.Create(
                 "MissionFailed",
                 $"Mission '{Id}' failed. Released {itemsCount} items back to Available."));
            return Result.Success();
        }
    }
}
