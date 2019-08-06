using System;
using System.Collections.Generic;
using System.Threading;

namespace StockTicker.StockService
{

    public class StockPriceService : IStockService
    {

        private static Random rndGen = new Random(DateTime.Today.Millisecond);
        List<StockInfo> StockDataSource = new List<StockInfo>();

        private Dictionary<IStockObserver, List<string>> InvestorsMap = new Dictionary<IStockObserver, List<string>>();
        Thread PriceChecker;

      
        //Get the list of Stocks from the data source and start monitoring its prices
        public StockPriceService()
        {
          
            StockDataSource.Add(new StockInfo { Symbol = "Stock1", Price = 250, MinPrice = 240, LastUpdated = DateTime.Now });
            StockDataSource.Add(new StockInfo { Symbol = "Stock2", Price = 190, MinPrice = 180, LastUpdated = DateTime.Now });


            //this thread is to simulate Stock Market data data APIs
            PriceChecker = new Thread(this.MonitorStockPrices);
            PriceChecker.Start();
        }

       public  void MonitorStockPrices()
       {
            //this parameter is used to simulate the fluctuations per requriments  
            int Delta = 30;

            do
            {
                foreach (var s in StockDataSource)
                {
                    s.Price = s.MinPrice + rndGen.Next(0, Delta);
                }
                Notify();
                Thread.Sleep(5000);

            } while (true);// while (InvestorsList.Count > 0);
       }


        public void Notify()
        {
            foreach(var pair in InvestorsMap)
            {
                var investor = pair.Key;
                var StockList = pair.Value;
                foreach(var symbol in StockList)
                {
                    var stock = StockDataSource.Find(st => st.Symbol == symbol);
                    investor.Update(stock);

                }

            }
        }

        public void Subscribe(IStockObserver observer, string Stock)
        {
            //if the investor just subscribed, then add it to the dictionary with the desired stcik
            if (!InvestorsMap.ContainsKey(observer))
            {
                var InvestorList = new List<string>();
                InvestorList.Add(Stock);
                InvestorsMap.Add(observer, InvestorList);
            }
            else //the investor already subscribed for another stock, just add it to the new one.
            {
                //make sure the investor is not already subscribed
                if(!InvestorsMap[observer].Contains(Stock) )
                    InvestorsMap[observer].Add(Stock);
            }
        }

        //unsubscribe from one stock only
        public void Unsubscribe(IStockObserver observer, string Stock)
        {
            throw new NotImplementedException();
        }

        //unsubscribe from all stocks and stop receiving notificatins
        public void Unsubscribe(IStockObserver observer)
        {
            InvestorsMap.Remove(observer);
        }

        

    }
}
