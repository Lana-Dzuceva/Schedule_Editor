using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Shedule_Editor
{
    static class SaveSchedule
    {
        public static void Save()
        {
            var curDir = Environment.CurrentDirectory;
            var xlPath = string.Empty;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = curDir + @"\..\..";
                //saveFileDialog.Filter = "(*.xls)|*.xls*";
                saveFileDialog.DefaultExt = ".xlsx";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Выберите местоположение будущего файла";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xlPath = saveFileDialog.FileName;
                }
            }
            if (xlPath == string.Empty)
            {
                MessageBox.Show("Считывание не было завершено");
                return;
            }
            MessageBox.Show(xlPath);
            XLWorkbook workbook = new XLWorkbook();
            workbook.AddWorksheet("Расписание");
            workbook.Worksheets.Worksheet("Расписание").Cell("A1").Value = "Расписание";

            workbook.AddWorksheet("Расписание2");
            workbook.Worksheets.Worksheet(1).Cell("A1").Value = "Расписание2";
            workbook.SaveAs(xlPath);
        }
    }
}
