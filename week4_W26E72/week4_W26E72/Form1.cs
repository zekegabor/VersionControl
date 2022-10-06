using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;

namespace week4_W26E72
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excel.Application xlApp;
        Excel.Workbook xlWb1;
        Excel.Worksheet xlSheet;

        public Form1()
        {
            InitializeComponent();

            LoadData();

            CreateExcel();

        }

        private void CreateExcel()
        {

            try
            {
                xlApp = new Excel.Application();
                xlWb1 = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWb1.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                xlWb1.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWb1 = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
                     "Kód",
                     "Eladó",
                     "Oldal",
                     "Kerület",
                     "Lift",
                     "Szobák száma",
                     "Alapterület (m2)",
                     "Ár (mFt)",
                     "Négyzetméter ár (Ft/m2)"
            };
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[i, i + 1] = headers[i];
            }
        }

        private void LoadData()
        {
            Flats = context.Flats.ToList();  
        }
    }
}
