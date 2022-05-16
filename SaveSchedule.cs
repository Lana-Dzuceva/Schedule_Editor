using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Newtonsoft.Json;

namespace Schedule_Editor
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
                MessageBox.Show("Файл не был выбран");
                return;
            }
            MessageBox.Show(xlPath);
            XLWorkbook workbook = new XLWorkbook();
            ListSubgroupSchedule AllSheduleGroup;
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\subgroupShedule.json"))
            {
                string json = file.ReadToEnd();
                AllSheduleGroup = JsonConvert.DeserializeObject<ListSubgroupSchedule>(json);
            }
            foreach (var group in AllSheduleGroup.Shedule)
            {
                workbook.AddWorksheet(group.Name);
                for (int i = 0; i < group.ScheduleFieldsSubjects.Count; i++)
                {
                    //workbook.Worksheet(group.Name).
                    string text = group.ScheduleFieldsSubjects[i];
                    //MessageBox.Show((text == string.Empty).ToString());
                    //MessageBox.Show("С" + (i + 1).ToString());
                    workbook.Worksheet(group.Name).Cell("C" + (i + 1).ToString()).Value = "1";
                    MessageBox.Show(("С" == "C").ToString());
                }
            }
            
            workbook.SaveAs(xlPath);
        }
    }
}
