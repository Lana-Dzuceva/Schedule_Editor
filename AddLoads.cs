using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                //openFileDialog.Filter = "(*.xlsm)|*.xlsm|(*.xlsx)|*.xlsx";
                openFileDialog.Filter = "(*.xls)|*.xls*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Выберите файл с нагрузками преподавателей";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xlPath = openFileDialog.FileName;
                }
            }
            if (xlPath == string.Empty)
            {
                MessageBox.Show("Считывание не было завершено");
                return;
            }
            List<Group> listGroups = new List<Group>();
            List<Teacher> teachlst = new List<Teacher>();
            List<SubgroupSchedule> subgroupShedule = new List<SubgroupSchedule>();
            XLWorkbook book = new XLWorkbook(xlPath);


            var lists = book.Worksheets;
            foreach (var list in lists)
            {
                try
                {
                    string[] listName = list.Name.Split(new string[] { " ", "  ", "   " }, StringSplitOptions.RemoveEmptyEntries);
                    if (!listName.Contains("Вакансия") && !listName.Contains("поручение"))
                    {
                        string lastName = listName[0];
                        string firstName;
                        if (listName.Length == 1)
                        {
                            firstName = "Чубака";
                        }
                        else firstName = listName[1];
                        List<Subject> listSubjects = new List<Subject>();
                        Subject sb;
                        int row = 13;
                        string dir = list.Cell("B" + row.ToString()).GetValue<string>();
                        while (!dir.Contains("Итого"))
                        {
                            dir = list.Cell("B" + row.ToString()).GetValue<string>();
                            //dir = list.Cell("G" + row.ToString()).GetValue<string>();
                            string classF = list.Cell("J" + row.ToString()).GetValue<string>();
                            string[] group = list.Cell("G" + row.ToString()).GetValue<string>().Split(',');
                            if ((group[0].Contains("ПМ") || group[0].Contains("ИВТ") || group[0].Contains("ПОМИ") || group[0].Contains("МАТ")) && (classF.Contains("Лекция") || classF.Contains("Лабораторная") || classF.Contains("Практич")))
                            // if ((dir.Contains("Математика") || dir.Contains("Информатика") || dir.Contains("Педагогическое")) && (classF.Contains("Лекция") || classF.Contains("Лабораторная") || classF.Contains("Практич")))
                            {
                                for (int i = 0; i < group.Length; i++)
                                {
                                    if (classF.Contains("Практич")) classF = "Практика";
                                    sb = new Subject(list.Cell("E" + row.ToString()).GetValue<string>(),
                                        list.Cell("O" + row.ToString()).GetValue<int>(),
                                        group[i],
                                        classF);
                                    listSubjects.Add(sb);
                                    if (!ListGroups.ContainsGroup(listGroups, group[i]))
                                    {
                                        listGroups.Add(new Group(group[i]));
                                        var temp = new List<string>();
                                        for (int r = 0; r < 20; r++)
                                        {
                                            temp.Add("");
                                        }
                                        subgroupShedule.Add(new SubgroupSchedule(group[i], temp, temp));
                                    }
                                }
                            }
                            row++;
                        }
                        //MessageBox.Show(listSubjects.Count.ToString());
                        if (listSubjects.Count > 0)
                        {
                            int indEl = ListTeachers.ContainsTeacher(teachlst, lastName, firstName);
                            if (indEl != -1)
                            {
                                foreach (var sbs in listSubjects)
                                {
                                    teachlst[indEl].Subjects.Items.Add(sbs);
                                }
                            }
                            else
                            {
                                teachlst.Add(new Teacher(lastName, firstName, new ListSubjects(listSubjects)));
                            }
                        }
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Кривые у вас файлы");
                    //return;
                }
                }

            ListTeachers AllTeachers;
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\loads.json"))
            {
                string json = file.ReadToEnd();
                AllTeachers = JsonConvert.DeserializeObject<ListTeachers>(json);
            }
            AllTeachers.Update(teachlst);
            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\loads.json"))
                sw.WriteLine(JsonConvert.SerializeObject(AllTeachers));

            //--------------------------------------
            ListGroups AllGroups;
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\groups.json"))
            {
                string json = file.ReadToEnd();
                AllGroups = JsonConvert.DeserializeObject<ListGroups>(json);
            }
            AllGroups.Update(listGroups);
            
            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\groups.json"))
                sw.WriteLine(JsonConvert.SerializeObject(AllGroups));
            //-----------------------------------


            ListSubgroupShedule AllSheduleGroup;
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\subgroupShedule.json"))
            {
                string json = file.ReadToEnd();
                AllSheduleGroup = JsonConvert.DeserializeObject<ListSubgroupShedule>(json);
            }
            AllSheduleGroup.Update(subgroupShedule);
            
            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\subgroupShedule.json"))
                sw.WriteLine(JsonConvert.SerializeObject(AllSheduleGroup));

            MessageBox.Show("Считывание завершено");
        }
    }
}
