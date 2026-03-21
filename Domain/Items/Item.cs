using Domain.Items.ItemValueObjects;
using Domain.Mission.MissionValueObject;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Items
{
    public class Item : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public Weight Weight { get; private set; } = null!;
        public ItemStatus Status { get; private set; }
        public MissionId? ReservedMissionId { get; private set; }

        //Concurrency
        public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

        private Item(ItemId id, string name, Weight weight) : base(id.Value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            Name = name;
            Weight = weight ?? throw new ArgumentNullException(nameof(weight));
            Status = ItemStatus.Available;
            ReservedMissionId = null;
        }

        // Parameterless constructor for EF Core
        private Item() : base(Guid.Empty) { }

        public static Item Create(ItemId id, string name, Weight weight)
        {
            return new Item(id, name, weight);
        }

        public Result ReserveForMission(MissionId missionId)
        {
            if (Status != ItemStatus.Available)
                return Result.Failure(ItemError.NotAvailable);

            Status = ItemStatus.Reserved;
            ReservedMissionId = missionId;
            return Result.Success();
        }

        public void SetAvailable()
        {
            Status = ItemStatus.Available;
            ReservedMissionId = null;
        }

        public void SetConsumed()
        {
            Status = ItemStatus.Consumed;
            ReservedMissionId = null;
        }
    }
}

