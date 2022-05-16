using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Newtonsoft.Json;

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
                MessageBox.Show("Файл не был выбран");
                return;
            }
            MessageBox.Show(xlPath);
            XLWorkbook workbook = new XLWorkbook();
            ListSubgroupShedule AllSheduleGroup;
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\subgroupShedule.json"))
            {
                string json = file.ReadToEnd();
                AllSheduleGroup = JsonConvert.DeserializeObject<ListSubgroupShedule>(json);
            }
            foreach (var group in AllSheduleGroup.Shedule)
            {
                workbook.AddWorksheet(group.Name);
                for (int i = 0; i < group.ScheduleFieldsSubjects.Count; i++)
                {
                    //workbook.Worksheet(group.Name).
                    workbook.Worksheet(group.Name).Cell("С" + (i + 1).ToString()).Value = group.ScheduleFieldsSubjects[i];
                    //workbook.Worksheet(group.Name).Cell("A"+ i.ToString()).Value = //group.ScheduleFieldsSubjects[i];
                }
            }
            //for (int i = 0; i < AllSheduleGroup.Shedule.Count; i++)
            //{
            //    //MessageBox.Show(AllSheduleGroup.Shedule[i].Name);
            //    workbook.AddWorksheet(AllSheduleGroup.Shedule[i].Name);

            //}
            //workbook.AddWorksheet("Расписание");
            //workbook.Worksheets.Worksheet("Расписание").Cell("A1").Value = "Расписание";

            //workbook.AddWorksheet("Расписание2");
            //workbook.Worksheets.Worksheet(1).Cell("A1").Value = "Расписание2";
            workbook.SaveAs(xlPath);
        }
    }
}
