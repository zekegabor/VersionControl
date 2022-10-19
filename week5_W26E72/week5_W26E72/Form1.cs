using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace week5_W26E72
{
    public partial class Form1 : Form
    {
        BindingList<Entities.RateData> Rates = new BindingList<Entities.RateData>();
        public Form1()
        {

            InitializeComponent();           

            var mnbReq = new MNBServRef.MNBArfolyamServiceSoapClient();
            var getCurr = new MNBServRef.GetExchangeRatesRequestBody()
            { 
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var MnbGetExResp = mnbReq.GetExchangeRates(getCurr);
            var result = MnbGetExResp.GetExchangeRatesResult;

            XMLWork(result);

            dataGridView1.DataSource = Rates;
        }

        private void XMLWork(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement x in xml.DocumentElement)
            {
                var ratDat = new Entities.RateData();
                ratDat.Date = DateTime.Parse(x.GetAttribute("date"));
                var firstChild = (XmlElement)x.ChildNodes[0];
                ratDat.Currency = firstChild.GetAttribute("curr");
                var unit = decimal.Parse(firstChild.GetAttribute("unit"));
                var value = decimal.Parse(firstChild.InnerText);
                if (unit != 0)
                {
                    ratDat.Value = value / unit;
                }
                Rates.Add(ratDat);
            }
        }
    }
}
