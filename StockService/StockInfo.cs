using System;
using System.Collections.Generic;
using System.Text;

namespace StockTicker.StockService
{

    public class StockInfo 
    {
        //stock Symobol or Name
        public string Symbol { get; set; }

        //current market price
        public int Price { get; set; }
        //full name and description
        public string Description { get; set; }
        //the time and date when the price was updated
        public DateTime LastUpdated { get; set; }

        //this is used only for this test purpose to set the right values with fluctuation (Delta=30)
        public int MinPrice { get; set; }

        public StockInfo() { }

        public static bool operator == (StockInfo m1, StockInfo m2)
        {
            return m1.ToString() == m2.ToString();
                
        }

        public static bool operator !=(StockInfo m1, StockInfo m2)
        {
            return m1.ToString() != m2.ToString();
        }


        public override string ToString()
        {
            return $"Stock Symbol:{Symbol}, CurrentPrice:{Price}, LastUpdated: {LastUpdated}";
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == (obj as StockInfo).ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }


        

    }
}
