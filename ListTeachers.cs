using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shedule_Editor
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
                if(ContainsTeacher(Teachers, teacher.LastName, teacher.FirstName) == -1) Teachers.Add(teacher);
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

        public List<string> ScheduleFieldsSubjects { get; set; }
        public List<string> ScheduleFieldsAudiences { get; set; }

        public SubgroupSchedule(string name, List<string> strings, List<string> numbers)
        {
            Name = name;
            ScheduleFieldsSubjects = strings;
            ScheduleFieldsAudiences = numbers;
        }


    }

    class ListSubgroupShedule
    {
        public List<SubgroupSchedule> Shedule { get; set; }

        public ListSubgroupShedule(List<SubgroupSchedule> shedule)
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
                if (subGroup.ScheduleFieldsSubjects[numberOfLecture].Contains(secName)) return false;
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
    }
}
