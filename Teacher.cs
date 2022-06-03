using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule_Editor
{
    class Teacher
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public ListSubjects Subjects { get; set; }

        public Teacher(string lastName, string firstName, ListSubjects listSubjects)
        {
            LastName = lastName;
            FirstName = firstName;
            Subjects = listSubjects;
        }
    }
    class ListSubjects
    {
        public List<Subject> Items { get; set; }

        public ListSubjects(List<Subject> subjects)
        {
            this.Items = subjects;
        }
    }
    class Subject
    {
        public string Name { get; set; }

        public int NumberOfHours { get; set; }

        public string Group { get; set; }
        //
        public string ClassForm { get; set; }

        public Subject(string name, int numberOfHours, string group, string classForm)
        {
            Name = name;
            NumberOfHours = numberOfHours;
            Group = group;
            ClassForm = classForm;
        }
    }
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

}
