using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutomatenThreading
{
    //Vores handler class
    class Automat
    {
        public static object _lock = new object();
        DrinkFactory factory = new DrinkFactory();
        Conveyor mainConveyor = new Conveyor(10);
        public Conveyor sodaConveyor = new Conveyor(5);
        public Conveyor beerConveyor = new Conveyor(5);
        
        //TO DO: Sæt count på produceret drinks og count på consumed drinks
        Drink Produce()
        {
            Drink drink = factory.ProduceDrink();
            return drink;
        }

        
        void PutOnConveyor(Conveyor conveyor, Drink drink)
        {

            conveyor.load.Enqueue(drink);

        }
        

            //Puts the drinks on the conveyors they belong to while there is space for it
        public void DrinkSplitter()
        {
            while (true)
            {

                lock (_lock)
                {
                    while (mainConveyor.load.Count > 0)
                    {
                        if (mainConveyor.load.Peek().brand == "Beer" && beerConveyor.maxCapacity > beerConveyor.load.Count)
                        {

                            PutOnConveyor(beerConveyor, mainConveyor.load.Dequeue());

                            Console.WriteLine("I put on beer conveyor");
                        }
                        else if (mainConveyor.load.Peek().brand == "Soda" && sodaConveyor.maxCapacity > sodaConveyor.load.Count)
                        {

                            PutOnConveyor(sodaConveyor, mainConveyor.load.Dequeue());
                            Console.WriteLine("I put on soda conveyor");
                        }
                        else
                            Monitor.Wait(_lock);

                        Thread.Sleep(200);

                        Monitor.PulseAll(_lock);
                    }
                }

            }

        }

        void PrintDrinkCount()
        {
            Console.WriteLine("I have produced a total of " + factory.beerCount + " Beer");
            Console.WriteLine("I have produced a total of " + factory.sodaCount + " Soda");
        }
        //Fills up the main conveyor with random drinks using the Produce method
        public void FillMainConveyor()
        {
            while (true)
            {

                lock (_lock)
                {
                    while (mainConveyor.load.Count < mainConveyor.maxCapacity)
                    {

                        PutOnConveyor(mainConveyor, Produce());
                        Console.WriteLine("I produce");
                        Thread.Sleep(100);
                        Monitor.PulseAll(_lock);
                    }
                    PrintDrinkCount();
                    Monitor.Wait(_lock);
                }
            }
        }
    }
}
