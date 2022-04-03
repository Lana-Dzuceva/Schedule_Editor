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
    }

    class Group
    {
        public string name { get; set; }

        public Group(string name)
        {
            this.name = name;
        }
    }

    class ListGroups
    {
        public List<Group> Groups { get; set; }

        public ListGroups(List<Group> groups)
        {
            Groups = groups;
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
