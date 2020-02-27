using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatenThreading
{
    class Conveyor
    {
        public Queue<Drink> load;
        public int maxCapacity;
        public Conveyor(int conveyorSize)
        {
            load = new Queue<Drink>(conveyorSize);
            maxCapacity = conveyorSize;
        }
    }
}
