﻿using ClosedXML.Excel;
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
                openFileDialog.Filter = "(*.xlsm)|*.xlsm|(*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Выберите файл с нагрузками преподавателей";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    xlPath = openFileDialog.FileName;
                }
            }

            List<Group> listGroups = new List<Group>();
            List<Teacher> teachlst = new List<Teacher>();
            List<SubgroupSchedule> subgroupShedule = new List<SubgroupSchedule>();
            XLWorkbook book = new XLWorkbook(xlPath);


            var lists = book.Worksheets;
            foreach (var list in lists)
            {
                string[] listName = list.Name.Split(new string[] { " ", "  ", "   " }, StringSplitOptions.RemoveEmptyEntries);
                if (!listName.Contains("Вакансия") && !listName.Contains("поручение"))
                {
                    string lastName = listName[0];
                    string firstName = listName[1];
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
                                if (!ListGroups.ContainsGroups(listGroups, group[i]))
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

            ListTeachers ListT = new ListTeachers(teachlst);
            var lt = JsonConvert.SerializeObject(ListT);
            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\loads.json"))
                sw.WriteLine(lt);

            ListGroups Gr = new ListGroups(listGroups);
            var gr = JsonConvert.SerializeObject(Gr);
            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\groups.json"))
                sw.WriteLine(gr);

            ListSubgroupShedule SGS = new ListSubgroupShedule(subgroupShedule);
            var fsg = JsonConvert.SerializeObject(SGS);
            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\subgroupShedule.json"))
                sw.WriteLine(fsg);

            MessageBox.Show("Считывание завершено");
        }
    }
}