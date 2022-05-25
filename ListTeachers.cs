using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    class SubgroupSchedule
    {
        public string Name { get; set; }

        public List<string> ScheduleFieldsSubjects1 { get; set; }
        public List<string> ScheduleFieldsAudiences { get; set; }

        public List<string> ScheduleFieldsSubjects2 { get; set; }
        public List<string> ScheduleFieldsAudiences2 { get; set; }

        public SubgroupSchedule(string name, List<string> strings, List<string> numbers, List<string> strings2, List<string> numbers2)
        {
            Name = name;
            ScheduleFieldsSubjects1 = strings;
            ScheduleFieldsAudiences = numbers;
            ScheduleFieldsSubjects2 = strings2;
            ScheduleFieldsAudiences2 = numbers2;
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
            foreach (var subGroup in Shedule)
            {
                if (subGroup.ScheduleFieldsAudiences[numberOfLecture] == number) return false;

            }
            return true;
        }
        public bool IsLectorFree(string secName, int numberOfLecture)
        {
            foreach (var subGroup in Shedule)
            {
                if (subGroup.ScheduleFieldsSubjects1[numberOfLecture].Contains(secName)) return false;
            }
            foreach (var subGroup in Shedule)
            {
                if (subGroup.ScheduleFieldsSubjects2[numberOfLecture].Contains(secName)) return false;
            }
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
            foreach (var group in Shedule)
            {
                for (int i = 0; i < group.ScheduleFieldsSubjects1.Count; i++)
                {
                    if (string.IsNullOrEmpty(group.ScheduleFieldsSubjects1[i]) || string.IsNullOrEmpty(group.ScheduleFieldsAudiences[i])) return false;
                }
                for (int i = 0; i < group.ScheduleFieldsSubjects2.Count; i++)
                {
                    if (string.IsNullOrEmpty(group.ScheduleFieldsSubjects2[i]) || string.IsNullOrEmpty(group.ScheduleFieldsAudiences[i])) return false;
                }
            }
            return true;
        }
    }
}
