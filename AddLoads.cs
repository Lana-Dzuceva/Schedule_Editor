using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace Shedule_Editor
{
    class AddLoads
    {
        //Парсинг нагрузок преподавателей
        public static void GenerateNewLoads()
        {
            var curDir = Environment.CurrentDirectory;
            var xlPath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = curDir + @"\..\..";
                openFileDialog.Filter = "(*.xlsm)|*.xlsm|(*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Выберите файл с нагрузками преподавателей";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xlPath = openFileDialog.FileName;
                }
            }
            try
            {
                List<Teacher> teachlst = new List<Teacher>();
                XLWorkbook book = new XLWorkbook(xlPath);
                var lists = book.Worksheets;
                foreach (var item in lists)
                {
                    string listName = item.Name;
                    if (!listName.Contains("Вакансия") && !listName.Contains("поручение"))
                    {
                        string[] fioTeach = item.Cell("A4").GetValue<string>().Split();
                        List<Subject> listSubjects = new List<Subject>();
                        Subject sb;
                        int st = 13;
                        string dir = item.Cell("B" + st.ToString()).GetValue<string>();
                        while (!dir.Contains("Итого"))
                        {
                            dir = item.Cell("B" + st.ToString()).GetValue<string>();
                            string classF = item.Cell("J" + st.ToString()).GetValue<string>();
                            if ((dir.Contains("Математика") || dir.Contains("Информатика")) && (classF.Contains("Лекция") || classF.Contains("Лабораторная") || classF.Contains("Практич")))
                            {
                                sb = new Subject(item.Cell("E" + st.ToString()).GetValue<string>(),
                                    item.Cell("O" + st.ToString()).GetValue<int>(),
                                    item.Cell("G" + st.ToString()).GetValue<string>(),
                                    classF);
                                listSubjects.Add(sb);
                            }
                            st++;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}
