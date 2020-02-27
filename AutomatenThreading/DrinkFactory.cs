using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatenThreading
{
    class DrinkFactory
    {
        internal int sodaCount = 0;
        internal int beerCount = 0;
        Random rnd = new Random(DateTime.Now.Millisecond);
        public Drink ProduceDrink()
        {
            int decider = rnd.Next(0, 2);
            Drink drink;
            if (decider == 1)
            {
                drink = new Beer();
                beerCount++;
            }
            else
            {
                drink = new Soda();
                sodaCount++;
            }

            return drink;
        }
    }
}
