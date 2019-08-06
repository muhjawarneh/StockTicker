using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTicker.StockService;

namespace StockTicker
{
    public class StockInvestor : IStockObserver
    {
        //when the price chnage, the event will be fired
        public event EventHandler<StockEventArgs> OnPriceChange;

        //keep track of the stocks watched and its history
        public List<StockInfo> PriceHistory = new List<StockInfo>();

        public string Name { get; private set; }

        public StockInvestor()
        {

        }
        public StockInvestor(string name)
        {
            Name = name;
        }

        //this will be called from the publisher (StockPriceService)
        public void Update(StockInfo e)
        {
            //check if the price has changed since last time (look at the Stock overrides)
            //if the stock returned to an old value in previous time, still it is considered a change 
            //var matchingStock = PriceHistory.Find(s => s.Equals(e));
            //if (matchingStock != null)
            {
                PriceHistory.Add(e);
                if (OnPriceChange != null)
                {
                    OnPriceChange(this, new StockEventArgs(e));
                }
            }
        }
    }
}
