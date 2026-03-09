using Domain.Items.ItemValueObjects;

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

        public Weight Weight { get; private set; } = null! ;

        private Item(ItemId id, string name, Weight weight) : base(id.Value)

        {

            if (string.IsNullOrWhiteSpace(name))

                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            Name = name;

            Weight = weight ?? throw new ArgumentNullException(nameof(weight));

        }

        // Parameterless constructor for EF Core

        private Item() : base(Guid.Empty) { }



        public static Item Create(ItemId id ,string name, Weight weight)

        {

            return new Item(id ,name, weight);

        }

        
        public void Update(string name, Weight weight)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
 
            Name = name;
            Weight = weight ?? throw new ArgumentNullException(nameof(weight));
        }

    }



}

