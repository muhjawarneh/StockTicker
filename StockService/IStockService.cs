using System;
using System.Collections.Generic;
using System.Text;

namespace StockTicker.StockService
{
    //interface of the publisher Observer pattern 
    public interface IStockService
    {
        void Subscribe(IStockObserver observer, string Stock);
        void Unsubscribe(IStockObserver observer, string Stock);
        void Unsubscribe(IStockObserver observer);
        void Notify();

    }
}
