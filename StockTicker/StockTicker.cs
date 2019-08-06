using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using StockTicker.StockService;
using Unity.Resolution;

namespace StockTicker
{
    public partial class StockTicker : Form
    {
        public bool MarketOpen { get; set; }


        IUnityContainer container = new UnityContainer();

        IStockObserver Investor;

        //will be instantiated by Unity
        IStockService WatchedStocks;


        public StockTicker()
        {
            InitializeComponent();
            //register types with unity container, , this can be done with configuration file
            container.RegisterType<IStockService, StockPriceService>();
            container.RegisterType<IStockObserver, StockInvestor>();
        }

        

        private void StockTicker_Load(object sender, EventArgs e)
        {
            //resolve and instintiate type using Unity
            Investor = container.Resolve<StockInvestor>("Investor1");

            //resolve and instintiate type using Unity
            WatchedStocks = container.Resolve<StockPriceService>();

            (Investor as StockInvestor).OnPriceChange += OnPriceChange;
        }

        private void OnPriceChange(object sender, StockEventArgs e)
        {
            string messageItem = $"Stock{e.Stock.Symbol} price has changed to {e.Stock.Price} at {e.Stock.LastUpdated:yyyy-MM-dd HH:mm:ss.fff}";
            
            //update UI in thread safe way
            if (lstBoxStocks.InvokeRequired)
            {
                lstBoxStocks.Invoke(new MethodInvoker(delegate
                {
                    lstBoxStocks.Items.Add(messageItem);
                }));
            }
            else
            {
                // This is the UI thread so perform the task.
                lstBoxStocks.Items.Add(messageItem);
            }
            
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            WatchedStocks.Unsubscribe(Investor);

            lblStatus.Text = "Disconnected ... No price update";

            btnStart.Enabled = true;
            btnStop.Enabled = false;

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {

            //with a little work on UI, you can allow the user to subscribe to individual stock 
            WatchedStocks.Subscribe(Investor, "Stock1");
            WatchedStocks.Subscribe(Investor, "Stock2");

            lblStatus.Text = "Monitoring ... waiting price update";

            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }
    }
}
