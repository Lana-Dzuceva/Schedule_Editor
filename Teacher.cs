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
}
