using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutomatenThreading
{
    class Consumer
    {
        int consumeCount;
        //Removes a drink object from conveyor in parameter if there are any
        public void Consume(object conveyor)
        {
            Drink drink;
            Conveyor tempCon = (Conveyor)conveyor;
            while (true)
            {
                lock (Automat._lock)
                {
                    while (tempCon.load.Count > 0)
                    {

                        drink = tempCon.load.Peek();
                        Console.WriteLine("I drink " + drink.brand);
                        tempCon.load.Dequeue();
                        consumeCount++;
                        Console.WriteLine("I have drank a total of " + consumeCount + " " + drink.brand);
                        Monitor.PulseAll(Automat._lock);
                        Thread.Sleep(500);
                    }
                    Monitor.Wait(Automat._lock);
                }
            }
        }
    }
}
