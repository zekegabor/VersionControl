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
            object[,] values = new object[Flats.Count, headers.Length];
            int count = 0;
            foreach (var f in Flats)
            {
                values[count, 0] = f.Code;
                values[count, 1] = f.Vendor;
                values[count, 2] = f.Side;
                values[count, 3] = f.District;
                values[count, 4] = f.Elevator;
                values[count, 5] = f.NumberOfRooms;
                values[count, 6] = f.FloorArea;
                values[count, 7] = f.Price;
                count++;
            }
        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();  
        }
    }
}
