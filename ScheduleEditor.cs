using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Schedule_Editor
{
    public partial class ScheduleEditor : Form
    {
        ListTeachers AllTeachers;
        ListSubgroupSchedule AllScheduleGroup;
        AudienceGroup AllAudiences;
        ListGroups AllGroups;
        string ActiveGroup;
        string activeDiscipline = "";
        string curDir = Environment.CurrentDirectory;
        int CountOfWeeksInYear = 39;
        int CountOfWeeksIn1Semester = 18;
        public ScheduleEditor()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridViewSсhedule.Height = this.Height - 40;
            dataGridViewSсhedule.Width = this.Width;
            dataGridViewSсhedule.RowTemplate.Height = 46;
            dataGridViewSсhedule.RowCount = 20;
            dataGridViewSсhedule.ColumnHeadersHeight = 40;
            dataGridViewSсhedule.ColumnCount = 2;
            dataGridViewSсhedule.Columns[1].Width = 100;
            dataGridViewSсhedule.Columns[1].HeaderText = "Аудитория";
            string[] weekDays = { "Пн", "Вт", "Ср", "Чт", "Пт" };
            for (int i = 0; i < 20; i += 4)
            {
                dataGridViewSсhedule.Rows[i].HeaderCell.Value = weekDays[i / 4];
            }
            for (int i = 0; i < dataGridViewSсhedule.Columns.Count; i++)
            {
                dataGridViewSсhedule.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int col = 0; col < dataGridViewSсhedule.Columns.Count; col++)
            {
                for (int row = 0; row < dataGridViewSсhedule.Rows.Count; row++)
                {
                    dataGridViewSсhedule[col, row].Value = "";
                }
            }
            
            listViewSubjects.Columns.Add("Дисциплина");
            listViewSubjects.Columns.Add("Преподователь");
            listViewSubjects.Columns.Add("Тип занятия");
            listViewSubjects.Columns.Add("Кол-во часов");
            listViewSubjects.Columns[0].Width = 220;
            listViewSubjects.Columns[1].Width = 150;
            listViewSubjects.Columns[2].Width = 150;
            listViewSubjects.Columns[3].Width = 150;
            listViewSubjects.Font = new Font(FontFamily.GenericSansSerif, 12);

            //убираем мерцание и свойства выделения
            listViewSubjects.HoverSelection = false;
            listViewSubjects.FullRowSelect = true;
            Type type = listViewSubjects.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listViewSubjects, true, null);
            //
            foreach (DataGridViewRow row in dataGridViewAudience.Rows)
            {
                row.Height = 40;
            }
            dataGridViewAudience.RowCount = 5;
            dataGridViewAudience.ColumnCount = 10;
            dataGridViewAudience.BackgroundColor = Color.White;

            listViewAudienceDescription.Height = 350;
            foreach (var item in new string[] { "Номер", "Количество мест", "Меловая доска", "Маркерная доска", "Количество компьютеров", "Проектор" })
            {
                listViewAudienceDescription.Columns.Add(item, 120);
            }
        }

        void updateWorkLoads()
        {
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\subgroupShedule.json"))
            {
                AllScheduleGroup = JsonConvert.DeserializeObject<ListSubgroupSchedule>(file.ReadToEnd());
            }
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\loads.json"))
            {
                AllTeachers = JsonConvert.DeserializeObject<ListTeachers>(file.ReadToEnd());
            }
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\groups.json"))
            {
                AllGroups = JsonConvert.DeserializeObject<ListGroups>(file.ReadToEnd());
            }
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\audienceGroup.json"))
            {
                AllAudiences = JsonConvert.DeserializeObject<AudienceGroup>(file.ReadToEnd());
            }
        }
        void ShowListViewGroup()
        {
            listViewGroup.Items.Clear();
            int year = DateTime.Now.Year - 2000;
            listViewGroup.View = View.Tile;
            for (int i = 1; i < 5; i++)
            {
                listViewGroup.Groups.Add(new ListViewGroup($"{i} курс"));
            }
            listViewGroup.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            foreach (var item in AllGroups.Groups)
            {
                int groupInd = year - 1 - Convert.ToInt32(item.Name.Split('-')[1]);
                ListViewItem group = new ListViewItem(item.Name);
                listViewGroup.Items.Add(group);
                listViewGroup.Groups[groupInd].Items.Add(group);
            }

        }
        // считываем данные с файлов и заполняем лист групп
        private void FormSchedule_Load(object sender, EventArgs e)
        {

            var infFileShGroup = new FileInfo(curDir + @"\..\..\Files\subgroupShedule.json");
            if (infFileShGroup.Length == 0)
                AddLoads.GenerateNewLoads();
            updateWorkLoads();
            ShowListViewGroup();

            dataGridViewAudience.Hide();
            listViewAudienceDescription.Hide();

            ShowAudiences();
        }

        // при нажатии на группу заполняется таблица с расписанием и отображаюся нагрузки преподователей в листе преподователей
        private void listViewGroup_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ActiveGroup = listViewGroup.SelectedItems[0].Text;
                dataGridViewSсhedule.Columns[0].HeaderText = ActiveGroup;
                ShowSchedule();
                ShowSubjects();
                //DisciplineCheck();
            }
            catch (Exception)
            { }
        }

        void Save()
        {
            if (ActiveGroup == null) return;

            foreach (var subGrouSchedule in AllScheduleGroup.Shedule)
            {
                if (subGrouSchedule.Name == ActiveGroup)
                {
                    for (int i = 0; i < dataGridViewSсhedule.Rows.Count; i++)
                    {
                        subGrouSchedule.ScheduleFieldsSubjects[i] = dataGridViewSсhedule.Rows[i].Cells[0].Value.ToString();
                        subGrouSchedule.ScheduleFieldsAudiences[i] = dataGridViewSсhedule.Rows[i].Cells[1].Value.ToString();
                    }
                    break;
                }
            }

            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\subgroupShedule.json"))
                sw.WriteLine(JsonConvert.SerializeObject(AllScheduleGroup));

            //DisciplineCheck();
        }
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSchedule.Save();
        }

        private void DisciplineCheck()
        {
            for (int i = 0; i < dataGridViewSсhedule.Rows.Count; i++)
            {
                string s = dataGridViewSсhedule.Rows[i].Cells[0].Value.ToString();
                for (int j = 0; j < listViewSubjects.Items.Count; j++)
                {
                    string t = ListViewItemToString(listViewSubjects.Items[j]);
                    if (s == t)
                    {
                        listViewSubjects.Items.RemoveAt(j);
                    }
                }
            }
        }
        // функция отображения аудиторий в его датагриде
        void ShowAudiences()
        {
            for (int ind = 0, row = 0, col = 0; ind < AllAudiences.Count; ind++)
            {
                dataGridViewAudience[col, row].Value = AllAudiences.Audiences[ind].Number;
                col++;
                if (col == dataGridViewAudience.Columns.Count)
                {
                    col = 0;
                    row++;
                }
            }
        }
        private void ShowSubjects()
        {
            listViewSubjects.Items.Clear();
            foreach (var teacher in AllTeachers.Teachers)
            {
                foreach (var sub in teacher.Subjects.Items)
                {
                    if (sub.Group == ActiveGroup)
                    {
                        ListViewItem lvi = new ListViewItem(sub.Name);
                        lvi.SubItems.Add(teacher.LastName + " " + teacher.FirstName);
                        lvi.SubItems.Add(sub.ClassForm);
                        lvi.SubItems.Add(sub.NumberOfHours.ToString());
                        listViewSubjects.Items.Add(lvi);
                    }
                }
            }
        }
        private void ShowSchedule()
        {
            foreach (var item in AllScheduleGroup.Shedule)
            {
                if (item.Name == ActiveGroup)
                {
                    for (int i = 0; i < dataGridViewSсhedule.Rows.Count; i++)
                    {
                        dataGridViewSсhedule[0, i].Value = item.ScheduleFieldsSubjects[i];
                        dataGridViewSсhedule[1, i].Value = item.ScheduleFieldsAudiences[i];
                    }
                    break;
                }
            }

        }
        private string ListViewItemToString(ListViewItem item)
        {
            return $"{item.SubItems[0].Text} {item.SubItems[1].Text} {item.SubItems[2].Text}";
        }
        private void dataGridViewSchedule_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridViewSсhedule.ClearSelection();
        }
        public bool HasLectorFreeHours(string lector)
        {
            foreach (ListViewItem item in listViewSubjects.Items)
            {
                if (ListViewItemToString(item) == lector)
                {
                    return Convert.ToInt32(item.SubItems[3].Text) >= CountOfWeeksIn1Semester;//Тут
                }
            }
            return false;
        }
        void ShowAudienceDescription(string aud)
        {
            if (!string.IsNullOrEmpty(aud))
            {
                foreach (var item in AllAudiences.Audiences)
                {
                    if (item.Number.ToString() == aud)
                    {
                        listViewAudienceDescription.Items.Clear();
                        ListViewItem listViewItem = new ListViewItem(aud);
                        listViewItem.SubItems.Add(item.CountOfSeats.ToString());
                        listViewItem.SubItems.Add(item.ChalkBoard ? "Есть" : "Нет");
                        listViewItem.SubItems.Add(item.MarkerBoard ? "Есть" : "Нет");
                        listViewItem.SubItems.Add(item.NumberOfComputers.ToString());
                        listViewItem.SubItems.Add(item.Projector ? "Есть" : "Нет");
                        listViewAudienceDescription.Items.Add(listViewItem);
                    }
                }
            }
        }
        private void dataGridViewAudience_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo info = dataGridViewAudience.HitTest(e.X, e.Y);
                string aud = dataGridViewAudience[info.ColumnIndex, info.RowIndex].Value.ToString();
                ShowAudienceDescription(aud);
                dataGridViewSсhedule.DoDragDrop(aud, DragDropEffects.Copy);

            }
            catch (Exception)
            { }
        }

        private void dataGridViewAudience_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void dataGridViewAudience_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string cellvalue = e.Data.GetData(typeof(string)) as string;
                Point cursorLocation = this.PointToClient(new Point(e.X, e.Y));

                DataGridView.HitTestInfo hittest = dataGridViewAudience.HitTest(cursorLocation.X, cursorLocation.Y - 20);
                if (hittest.ColumnIndex != -1
                    && hittest.RowIndex != -1)
                    dataGridViewAudience[hittest.ColumnIndex, hittest.RowIndex].Value = cellvalue;

                Save();
            }
            catch
            { }
        }

        private void listViewSubjects_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int indexSource = listViewSubjects.Items.IndexOf(listViewSubjects.GetItemAt(e.X, e.Y));
                string s = ListViewItemToString(listViewSubjects.Items[indexSource]);
                listViewSubjects.DoDragDrop(s, DragDropEffects.Copy);
            }
            catch
            { }
        }
        private void listViewSubjects_DragDrop(object sender, DragEventArgs e)
        {
            activeDiscipline = "";
            ShowSubjects();
            //DisciplineCheck();
        }
        private void listViewSubjects_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void dataGridViewShedule_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo info = dataGridViewSсhedule.HitTest(e.X, e.Y);
                string s = dataGridViewSсhedule[info.ColumnIndex, info.RowIndex].Value.ToString();
                if (!string.IsNullOrEmpty(s))
                {
                    dataGridViewSсhedule.DoDragDrop(s, DragDropEffects.Copy);
                    dataGridViewSсhedule[info.ColumnIndex, info.RowIndex].Value = activeDiscipline;
                    activeDiscipline = "";
                    listViewSubjects.DoDragDrop(s, DragDropEffects.Copy);
                    dataGridViewAudience.DoDragDrop(s, DragDropEffects.Copy);
                    Save();
                }
            }
            catch (Exception)
            { }
        }


        private void dataGridViewShedule_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string cellvalue = e.Data.GetData(typeof(string)) as string;
                Point cursorLocation = this.PointToClient(new Point(e.X, e.Y));
                DataGridView.HitTestInfo hittest = dataGridViewSсhedule.HitTest(cursorLocation.X, cursorLocation.Y - 24);
                
                if (hittest.RowIndex != -1)
                {
                    bool IsAudience = int.TryParse(cellvalue, out int _) && hittest.ColumnIndex == 1;
                    bool IsLector = !int.TryParse(cellvalue, out int _) && hittest.ColumnIndex == 0;
                    if (IsAudience && AllScheduleGroup.IsAudienceEmpty(cellvalue, hittest.RowIndex) ||
                        IsLector && AllScheduleGroup.IsLectorFree(cellvalue, hittest.RowIndex) && HasLectorFreeHours(cellvalue))
                    {
                        activeDiscipline = dataGridViewSсhedule[hittest.ColumnIndex, hittest.RowIndex].Value.ToString();
                        dataGridViewSсhedule[hittest.ColumnIndex, hittest.RowIndex].Value = cellvalue;
                    }
                }

                ShowSubjects();
                //DisciplineCheck();
                Save();
            }
            catch
            { }
        }
        private void dataGridViewSchedule_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void AudiencesForm_Click(object sender, EventArgs e)
        {
            listViewSubjects.Hide();
            dataGridViewAudience.Show();
            listViewAudienceDescription.Show();
        }

        private void ShedulesForm_Click(object sender, EventArgs e)
        {
            listViewSubjects.Show();
            dataGridViewAudience.Hide();
            listViewAudienceDescription.Hide();
        }
        
        private void AddLoadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLoads.GenerateNewLoads();
            updateWorkLoads();
            ShowListViewGroup();
        }

    }
}
