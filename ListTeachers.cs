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

        public static bool ContainsGroups(List<Group> lst, string gr)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Name == gr) return true;
            }
            return false;
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
                for (int i = 0; i < 5; i++)
                {
                    if (subGroup.ScheduleFieldsAudiences[numberOfLecture + i * 4] == number) return false;
                }
                //foreach (var item in subGroup.ScheduleFieldsAudiences)
                //{
                //    if (item == number) return false;
                //}
            }
            return true;
        }
        bool IsLectorFree(string secName, int numberOfLecture)
        {
            foreach (var subGroup in Shedule)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (subGroup.ScheduleFieldsSubjects[numberOfLecture + i * 4].Contains(secName)) return false;
                }
                //if (subGroup.ScheduleFieldsSubjects[index].Contains(secName)) return false;
            }
            return true;
        }
    }
}
