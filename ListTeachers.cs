using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Schedule_Editor
{
    class ListTeachers
    {
        public List<Teacher> Teachers { get; set; }

        public ListTeachers(List<Teacher> teachers)
        {
            Teachers = teachers;
        }
        public void Update(List<Teacher> teachers)
        {
            foreach (Teacher teacher in teachers)
            {
                if (ContainsTeacher(Teachers, teacher.LastName, teacher.FirstName) == -1) Teachers.Add(teacher);
            }
            //Teachers.AddRange(teachers);
        }
        public static int ContainsTeacher(List<Teacher> lst, string lastName, string firstName)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].LastName == lastName && lst[i].FirstName == firstName) return i;
            }
            return -1;
        }
    }

    class Group
    {
        public string Name { get; set; }

        public Group(string name)
        {
            this.Name = name;
        }
    }

    class ListGroups
    {
        public List<Group> Groups { get; set; }

        public ListGroups(List<Group> groups)
        {
            Groups = groups;
        }
        /// <summary>
        /// подсказка к функции во время вызова
        /// 
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="gr"></param>
        /// <returns></returns>
        public static bool ContainsGroup(List<Group> lst, string gr)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Name == gr) return true;
            }
            return false;
        }
        public bool ContainsGroup1(string gr)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Name == gr) return true;
            }
            return false;
        }
        public void Update(List<Group> groups)
        {
            foreach (Group group in groups)
            {
                if (!this.ContainsGroup1(group.Name))
                {
                    Groups.Add(group);
                }
            }
            //Groups.AddRange(groups);
        }
    }
    class ScheduleString
    {
        public string FirstWeek { get; set; }
        public string SecondWeek { get; set; }
        
        public ScheduleString(string firstWeek, string secondWeek = "")
        {
            FirstWeek = firstWeek;
            SecondWeek = secondWeek;
        }
    }
    enum ScheduleRows
    {
        VerticalSplitted,
        HorizontalSplitted,
        NonSplitted
    }
    class SubgroupSchedule
    {
        public string Name { get; set; }

        public List<ScheduleString> ScheduleFieldsSubjectsSubGroup1 { get; set; }
        public List<ScheduleString> ScheduleFieldsAudiencesSubGroup1 { get; set; }

        public List<ScheduleString> ScheduleFieldsSubjectsSubGroup2 { get; set; }
        public List<ScheduleString> ScheduleFieldsAudiencesSubGroup2 { get; set; }


        [JsonConstructor]
        public SubgroupSchedule(string name, List<ScheduleString> strings, List<ScheduleString> numbers, List<ScheduleString> strings2, List<ScheduleString> numbers2)
        {
            Name = name;
            ScheduleFieldsSubjectsSubGroup1 = strings;
            ScheduleFieldsAudiencesSubGroup1 = numbers;
            ScheduleFieldsSubjectsSubGroup2 = strings2;
            ScheduleFieldsAudiencesSubGroup2 = numbers2;
        }
        public SubgroupSchedule(string name)
        {
            Name = name;
            List<ScheduleString> temp = new List<ScheduleString>(20);
            for (int i = 0; i < 20; i++)
            {
                temp.Add(new ScheduleString("", ""));
                
            }
            ScheduleFieldsSubjectsSubGroup1 = temp;
            ScheduleFieldsAudiencesSubGroup1 = temp;
            ScheduleFieldsSubjectsSubGroup2 = temp;
            ScheduleFieldsAudiencesSubGroup2 = temp;
        }


    }

    class ListSubgroupSchedule
    {
        public List<SubgroupSchedule> Shedule { get; set; }

        
        public ListSubgroupSchedule(List<SubgroupSchedule> shedule)
        {
            Shedule = shedule;
        }
        
        public bool IsAudienceEmpty(string number, int numberOfLecture)
        {
            //foreach (var subGroup in Shedule)
            //{
            //    if (subGroup.ScheduleFieldsAudiencesSubGroup1[numberOfLecture] == number) return false;

            //}
            return true;
        }
        public bool IsLectorFree(string secName, int numberOfLecture)
        {
            //foreach (var subGroup in Shedule)
            //{
            //    if (subGroup.ScheduleFieldsSubjectsSubGroup1[numberOfLecture].Contains(secName)) return false;
            //}
            //foreach (var subGroup in Shedule)
            //{
            //    if (subGroup.ScheduleFieldsSubjectsSubGroup2[numberOfLecture].Contains(secName)) return false;
            //}
            return true;
        }

       
        public bool ContainsSubGroup(string subgroupName)
        {
            foreach (var subgroup in Shedule)
            {
                if (subgroup.Name == subgroupName) return true;
            }
            return false;
        }
        public void Update(List<SubgroupSchedule> shedule)
        {
            foreach (var subgroup in shedule)
            {
                if (!this.ContainsSubGroup(subgroup.Name)) Shedule.Add(subgroup);
            }
        }
        public bool IsScheduleFilled()
        {
            //foreach (var group in Shedule)
            //{
            //    for (int i = 0; i < group.ScheduleFieldsSubjectsSubGroup1.Count; i++)
            //    {
            //        if (string.IsNullOrEmpty(group.ScheduleFieldsSubjectsSubGroup1[i]) || string.IsNullOrEmpty(group.ScheduleFieldsAudiencesSubGroup1[i])) return false;
            //    }
            //    for (int i = 0; i < group.ScheduleFieldsSubjectsSubGroup2.Count; i++)
            //    {
            //        if (string.IsNullOrEmpty(group.ScheduleFieldsSubjectsSubGroup2[i]) || string.IsNullOrEmpty(group.ScheduleFieldsAudiencesSubGroup1[i])) return false;
            //    }
            //}
            return true;
        }
    }
}
