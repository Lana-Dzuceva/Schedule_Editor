using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule_Editor
{
    class Audience
    {
        public int Number { get; set; }
        public int NumberOfComputers { get; set; }
        public bool ChalkBoard { get; set; }
        public bool MarkerBoard { get; set; }
        public int CountOfSeats { get; set; }
        //public int CountOfTables { get; set; }
        public bool Projector { get; set; }
        public bool IsUsed { get; set; }
        public Audience(int number, int numberOfComputers, bool chalkBoard, bool markerBoard, int countOfSeats, bool projector)
        {
            Number = number;
            NumberOfComputers = numberOfComputers;
            ChalkBoard = chalkBoard;
            MarkerBoard = markerBoard;
            CountOfSeats = countOfSeats;
            //CountOfTables = countOfTables;
            Projector = projector;
            IsUsed = false;
        }
        public override string ToString()
        {
            return Number + " кабинет:" + " кoмпьютеров: " + NumberOfComputers + ", доска: " + ChalkBoard
                + ", посадочные места: " + CountOfSeats +
                ", проектор: " + Projector;
        }
    }

    class AudienceGroup
    {
        public List<Audience> Audiences { get; set; }
        public int Count { get; set; }
        public AudienceGroup()
        {
            Audiences = new List<Audience>();
            Count = 0;
        }
        public void Add(Audience audience)
        {
            Audiences.Add(audience);
            Count++;
        }
        public int[] GetNumbersOfAudiences()
        {
            int[] numbers = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                numbers[i] = Audiences[i].Number;
            }
            return numbers;
        }

    }
}

