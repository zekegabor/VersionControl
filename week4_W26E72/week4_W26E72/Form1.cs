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
                xlSheet.Cells[1, i + 1] = headers[i];
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
                values[count, 8] = $"=H{count+2}/G{count+2}";
                count++;
            }
            xlSheet.get_Range(
                GetCell(2, 1),
                GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();  
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }
    }
}
