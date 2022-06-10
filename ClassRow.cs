using SpannedDataGridView;
using System;
using System.Windows.Forms;

namespace Shedule_Editor
{
    #region Код чтобы тестить потом классы

    //Obiectum obiectum = new Obiectum("qqq", "1");
    //ClassRow classRow = new ClassRow(RowTypes.Simple, obiectum);
    //MessageBox.Show(classRow.CountOfWeeks.ToString());

    #endregion
    public enum RowTypes
    {
        Simple,
        TwoGroups,
        TwoWeeks,
        TwoGroupsAndTwoWeeks
    }
    public class Obiectum
    {
        public string Subject;
        public string Audience;

        public Obiectum(string subject, string audience)
        {
            if (!int.TryParse(audience, out int res)) throw new Exception("Аудитория должна быть числом");
            Subject = subject;
            Audience = audience;
        }
    }
    public class ClassRow
    {
        RowTypes rowType;
        Obiectum group1week1;
        Obiectum group1week2;
        Obiectum group2week1;
        Obiectum group2week2;
        public RowTypes RowType
        {
            get { return rowType; }
            set
            {
                CountOfWeeks = value == RowTypes.Simple || value == RowTypes.TwoGroups ? 1 : 2;
                CountOfGroups = value == RowTypes.Simple || value == RowTypes.TwoWeeks ? 1 : 2;
                rowType = value;
            }
        }
        public Obiectum Group1week1
        {
            get
            {
                return group1week1;
            }
            set
            {
                group1week1 = value;
            }
        }
        public Obiectum Group1week2
        {
            get
            {
                if (CountOfWeeks == 2) return this.group1week2;
                else throw new Exception("Только 1 неделея");
            }
            set
            {
                if (CountOfWeeks == 2) this.group1week2 = value;
                //else throw new Exception("Только 1 неделея");&& value!= null
            }
        }
        public Obiectum Group2week1
        {
            get
            {
                if (CountOfGroups == 2) return this.group2week1;
                else throw new Exception("Только 1 группа");
            }
            set
            {
                if (CountOfGroups == 2) this.group2week1 = value;
                //else throw new Exception("Только 1 группа");
            }
        }
        public Obiectum Group2week2
        {
            get
            {
                if (CountOfGroups == 2 && CountOfWeeks == 2) return this.group2week2;
                else throw new Exception("Только 1 группа или 1 неделя");
            }
            set
            {
                if (CountOfGroups == 2 && CountOfWeeks == 2) this.group2week2 = value;
                //else throw new Exception("Только 1 группа или 1 неделя");
            }
        }
        public int CountOfWeeks { get; private set; }
        public int CountOfGroups { get; private set; }

        public RowTypes RowTypes
        {
            get => default;
            set
            {
            }
        }

        public Obiectum Obiectum
        {
            get => default;
            set
            {
            }
        }

        public ClassRow(RowTypes rowType, Obiectum group1week1, Obiectum group1week2 = null, Obiectum group2week1 = null, Obiectum group2week2 = null)
        {
            RowType = rowType;
            Group1week1 = group1week1;
            Group1week2 = group1week2;
            Group2week1 = group2week1;
            Group2week2 = group2week2;
        }
        public void hmm(DataGridView dataGrid, int ind)
        {
            (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 3;

            for (int i = 0; i < 4; i++)
            {
                (dataGrid[i, ind] as DataGridViewTextBoxCellEx).RowSpan = 2;
            }
        }
        /// <summary>
        /// Делает внешне датагрид таким каким надо
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="ind">номер строки</param>
        public void VisualizeRow(DataGridView dataGrid, int ind)
        {
            ind -= ind % 2;
            switch (RowType)
            {
                case RowTypes.Simple:
                    hmm(dataGrid, ind);
                    break;
                case RowTypes.TwoGroups:
                    hmm(dataGrid, ind);
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 1;
                    for (int i = 0; i < 4; i++)
                        (dataGrid[i, ind] as DataGridViewTextBoxCellEx).RowSpan = 2;

                    break;
                case RowTypes.TwoWeeks:
                    hmm(dataGrid, ind);
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
                    (dataGrid[3, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
                    (dataGrid[0, ind + 1] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
                    break;
                case RowTypes.TwoGroupsAndTwoWeeks:
                    hmm(dataGrid, ind);
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).ColumnSpan = 1;
                    (dataGrid[0, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
                    (dataGrid[3, ind] as DataGridViewTextBoxCellEx).RowSpan = 1;
                    
                    break;
                default:
                    break;
            }

        }
    }
}
