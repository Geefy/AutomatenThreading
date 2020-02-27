using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatenThreading
{

    abstract class Drink
    {
        public string brand;
        public Drink(string brand)
        {
            this.brand = brand;
        }
    }
}
