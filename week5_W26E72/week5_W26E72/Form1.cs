using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace week5_W26E72
{
    public partial class Form1 : Form
    {
        BindingList<Entities.RateData> Rates = new BindingList<Entities.RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {

            InitializeComponent();

            var getCurrType = new MNBServRef.GetExchangeRatesRequestBody()
            {
                //currencyNames = comboBox1.SelectedItem.ToString(),
                currencyNames = "EUR",
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            var curr = new MNBServRef.GetCurrenciesRequestBody(getCurrType);

            RefreshData();

            dateTimePicker1.MouseDown += DateTimePicker1_MouseDown;
            dateTimePicker2.MouseDown += DateTimePicker2_MouseDown;
            comboBox1.MouseDown += ComboBox1_MouseDown;
        }

        private void ComboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            RefreshData();
        }

        private void DateTimePicker2_MouseDown(object sender, MouseEventArgs e)
        {
            RefreshData();
        }

        private void DateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();
            
            var mnbReq = new MNBServRef.MNBArfolyamServiceSoapClient();
            var getCurr = new MNBServRef.GetExchangeRatesRequestBody()
            {
                //currencyNames = comboBox1.SelectedItem.ToString(),
                currencyNames = "EUR",
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            var MnbGetExResp = mnbReq.GetExchangeRates(getCurr);
            var result = MnbGetExResp.GetExchangeRatesResult;

            XMLWork(result);

            dataGridView1.DataSource = Rates;

            comboBox1.DataSource = Currencies;

            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
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
