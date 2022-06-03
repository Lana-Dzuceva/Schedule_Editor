using System;
using System.Collections.Generic;

namespace Schedule_Editor
{
   
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
        //public static bool ContainsGroup(List<Group> lst, string gr)
        //{
        //    for (int i = 0; i < lst.Count; i++)
        //    {
        //        if (lst[i].Name == gr) return true;
        //    }
        //    return false;
        //}
        public bool ContainsGroup(string gr)
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
                if (!this.ContainsGroup(group.Name))
                {
                    Groups.Add(group);
                }
            }
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
