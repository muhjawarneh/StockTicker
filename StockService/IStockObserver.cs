using System;
using System.Collections.Generic;
using System.Text;

namespace StockTicker.StockService
{
    //interface of the subscriber of the Observer pattern
    public interface IStockObserver
    {
        void Update(StockInfo e);
    }
}
