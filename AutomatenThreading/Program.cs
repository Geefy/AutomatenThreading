using System;
using System.Threading;

namespace AutomatenThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            Automat drinkAutomat = new Automat();
            Consumer sodaConsumer = new Consumer();
            Consumer beerConsumer = new Consumer();

            Thread produceProducts = new Thread(drinkAutomat.FillMainConveyor);
            Thread splitProducts = new Thread(drinkAutomat.DrinkSplitter);

            Thread sodaConsumerThread = new Thread(sodaConsumer.Consume);
            Thread beerConsumerThread = new Thread(beerConsumer.Consume);

            produceProducts.Start();
            splitProducts.Start();

            beerConsumerThread.Start(drinkAutomat.beerConveyor);
            sodaConsumerThread.Start(drinkAutomat.sodaConveyor);
        }
    }
}
