using System;
using System.Collections.Generic;
using System.Text;

namespace StockTicker.StockService
{
    public class StockEventArgs : EventArgs
    {
        public StockInfo Stock { get; set; }

        public StockEventArgs(StockInfo value)
        {
            Stock = value; 
        }
        

    }
}
