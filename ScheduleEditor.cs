using Newtonsoft.Json;
using Shedule_Editor;
using SpannedDataGridView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
//using System.Windows.Media;

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
        int formwidth;
        int formheight;
        string curDir = Environment.CurrentDirectory;
        int CountOfWeeksInYear = 39;
        int CountOfWeeksIn1Semester = 18;
        public ScheduleEditor()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridViewShedule.Height = formheight = this.Height - 40;
            dataGridViewShedule.Width = formwidth = this.Width;
            dataGridViewShedule.RowTemplate.Height = 23;
            for (int i = 0; i < 4; i++)
            {
                dataGridViewShedule.Columns.Add(new SpannedDataGridView.DataGridViewTextBoxColumnEx());

            }


            dataGridViewShedule.RowCount = 40;
            dataGridViewShedule.ColumnHeadersHeight = 40;
            //dataGridViewShedule.ColumnCount = 4;

            int w = dataGridViewShedule.Width - dataGridViewShedule.RowHeadersWidth;
            dataGridViewShedule.Columns[0].Width = (int)(0.1 * w);
            dataGridViewShedule.Columns[1].Width = (int)(0.4 * w);
            dataGridViewShedule.Columns[2].Width = (int)(0.4 * w);
            dataGridViewShedule.Columns[3].Width = (int)(0.1 * w);
            //dataGridViewShedule.Columns[0].Width = 100;
            //dataGridViewShedule.Columns[1].Width = 289;
            //dataGridViewShedule.Columns[2].Width = 289;
            //dataGridViewShedule.Columns[3].Width = 100;
            //string ans = "";
            //for (int i = 0; i < 4; i++)
            //{
            //    ans += dataGridViewShedule.Columns[i].Width.ToString() + " ";
            //}
            //MessageBox.Show(ans);
            //dataGridViewShedule.Columns[1].HeaderText = "Аудитория";
            string[] weekDays = { "Пн", "Вт", "Ср", "Чт", "Пт" };
            for (int i = 0; i < 20; i += 4)
            {
                dataGridViewShedule.Rows[i].HeaderCell.Value = weekDays[i / 4];
            }
            for (int i = 0; i < dataGridViewShedule.Columns.Count; i++)
            {
                dataGridViewShedule.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int col = 0; col < dataGridViewShedule.Columns.Count; col++)
            {
                for (int row = 0; row < dataGridViewShedule.Rows.Count; row++)
                {
                    dataGridViewShedule[col, row].Value = "";
                }
            }
            for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
            {
                ReturnToBaseRow(i);
            }
            //dataGridViewShedule.DefaultCellStyle.SelectionBackColor = dataGridViewShedule.DefaultCellStyle.BackColor;

            listViewSubjects.Columns.Add("Дисциплина");
            listViewSubjects.Columns.Add("Преподователь");
            listViewSubjects.Columns.Add("Тип занятия");
            listViewSubjects.Columns.Add("Кол-во часов");
            listViewSubjects.Columns[0].Width = 220;
            listViewSubjects.Columns[1].Width = 150;
            listViewSubjects.Columns[2].Width = 150;
            listViewSubjects.Columns[3].Width = 150;
            listViewSubjects.Font = new Font(FontFamily.GenericSansSerif, 12);

            //listViewGroup.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12);

            //убираем мерцание и свойства выделения
            listViewSubjects.HoverSelection = false;
            listViewSubjects.FullRowSelect = true;
            Type type = listViewSubjects.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listViewSubjects, true, null);

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
        void ReturnToBaseRow(int row)
        {
            (dataGridViewShedule[0, row] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
            if (row % 2 == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    (dataGridViewShedule[i, row] as DataGridViewTextBoxCellEx).RowSpan = 2;
                }
            }

        }
        void UpdateWorkLoads()
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
        void fill()
        {
            List<SubgroupSchedule> subgroupSchedules = new List<SubgroupSchedule>();

            foreach (var gr in AllGroups.Groups)
            {
                subgroupSchedules.Add(new SubgroupSchedule(gr.Name));
            }
            AllScheduleGroup = new ListSubgroupSchedule(subgroupSchedules);
            using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\subgroupShedule.json"))
                sw.WriteLine(JsonConvert.SerializeObject(AllScheduleGroup));

            //foreach (var subGrouSchedule in AllScheduleGroup.Shedule)
            //{
            //    for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
            //    {
            //        subGrouSchedule.ScheduleFieldsSubjectsSubGroup1[i] = "";
            //        subGrouSchedule.ScheduleFieldsAudiencesSubGroup1[i] = "";
            //        subGrouSchedule.ScheduleFieldsSubjectsSubGroup2[i] = "";
            //        subGrouSchedule.ScheduleFieldsAudiencesSubGroup2[i] = "";

            //    }
            //}
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
        private void FormShedule_Load(object sender, EventArgs e)
        {

            var infFileShGroup = new FileInfo(curDir + @"\..\..\Files\subgroupShedule.json");
            if (infFileShGroup.Length == 0)
                AddLoads.GenerateNewLoads();
            UpdateWorkLoads();
            ShowListViewGroup();

            dataGridViewAudience.Hide();
            listViewAudienceDescription.Hide();

            ShowAudiences();
            //fill();
            //Save();
            //MessageBox.Show((dataGridViewShedule[0, 1] as DataGridViewTextBoxCellEx).OwnerCell == dataGridViewShedule[0, 1]);
            //MessageBox.Show(((dataGridViewShedule[0, 1] as DataGridViewTextBoxCellEx).OwnerCell == dataGridViewShedule[0, 1]).ToString());
        }

        // при нажатии на группу заполняется таблица с расписанием и отображаюся нагрузки преподователей в листе преподователей
        private void listViewGroup_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsSplitting()) CancelSplit();
            try
            {
                ActiveGroup = listViewGroup.SelectedItems[0].Text;
                dataGridViewShedule.Columns[0].HeaderText = ActiveGroup;
                ShowShedule();
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
                    for (int i = 0; i < dataGridViewShedule.Rows.Count; i += 2)
                    {

                        //subGrouSchedule.ScheduleFieldsSubjectsSubGroup1[i] = new ScheduleString(dataGridViewShedule.Rows[i].Cells[1].Value.ToString());
                        //subGrouSchedule.ScheduleFieldsAudiencesSubGroup1[i] = dataGridViewShedule.Rows[i].Cells[0].Value.ToString();
                        //subGrouSchedule.ScheduleFieldsSubjectsSubGroup2[i] = dataGridViewShedule.Rows[i].Cells[2].Value.ToString() ?? "";
                        //subGrouSchedule.ScheduleFieldsAudiencesSubGroup2[i] = dataGridViewShedule.Rows[i].Cells[3].Value.ToString() ?? "";

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
            if (IsSplitting()) CancelSplit();
            if (!AllScheduleGroup.IsScheduleFilled())
            {
                var res = MessageBox.Show("Не все поля расписания заполнены. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.No) return;
            }
            SaveSchedule.Save();
        }

        private void DisciplineCheck()
        {
            for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
            {
                string s = dataGridViewShedule.Rows[i].Cells[0].Value.ToString();
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
        //bool isVerticalSplitted(int row)
        //{

        //}
        //bool IsHorizontalSplitted(int Row)
        //{
        //    return
        //}
        private void ShowShedule()
        {
            foreach (var item in AllScheduleGroup.Shedule)
            {
                if (item.Name == ActiveGroup)
                {
                    for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
                    {

                        //    dataGridViewShedule.Rows[i].Cells[1].Value = item.ScheduleFieldsSubjectsSubGroup1[i] ?? "";
                        //    dataGridViewShedule.Rows[i].Cells[0].Value = item.ScheduleFieldsAudiencesSubGroup1[i] ?? "";
                    }
                    break;
                }
            }


        }
        private string ListViewItemToString(ListViewItem item)
        {
            return $"{item.SubItems[0].Text} {item.SubItems[1].Text} {item.SubItems[2].Text}";
        }
        private void dataGridViewShedule_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridViewShedule.ClearSelection();
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
                dataGridViewShedule.DoDragDrop(aud, DragDropEffects.Copy);

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
        void CancelSplit()
        {
            поВертикалиToolStripMenuItem.Checked = false;
            поГоризонталиToolStripMenuItem.Checked = false;
            соединитьToolStripMenuItem.Checked = false;
            Cursor = Cursors.Default;
        }
        bool IsSplitting()
        {
            return поГоризонталиToolStripMenuItem.Checked || поВертикалиToolStripMenuItem.Checked || соединитьToolStripMenuItem.Checked;
        }
        void ClearRow(int row)
        {
            for (int i = 0; i < dataGridViewShedule.ColumnCount; i++)
            {
                dataGridViewShedule[i, row].Value = "";
            }
        }
        private void dataGridViewShedule_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo info = dataGridViewShedule.HitTest(e.X, e.Y);
                string s = dataGridViewShedule[info.ColumnIndex, info.RowIndex].Value.ToString();
                if (поВертикалиToolStripMenuItem.Checked)
                {
                    ReturnToBaseRow(info.RowIndex);
                    var temp = (dataGridViewShedule[0, info.RowIndex - info.RowIndex % 2] as DataGridViewTextBoxCellEx);
                    temp.ColumnSpan = 1;
                    
                    for (int i = 0; i < 4; i++)
                    {
                        (dataGridViewShedule[i, info.RowIndex - info.RowIndex % 2] as DataGridViewTextBoxCellEx).RowSpan = 2;
                    }
                    
                    ClearRow(info.RowIndex - info.RowIndex % 2);
                    ClearRow(info.RowIndex - info.RowIndex % 2 + 1);
                    CancelSplit();
                    return;
                }
                if (поГоризонталиToolStripMenuItem.Checked)
                {
                    ReturnToBaseRow(info.RowIndex);
                    var temp = (dataGridViewShedule[0, info.RowIndex - info.RowIndex % 2] as DataGridViewTextBoxCellEx);
                    temp.RowSpan = 1;

                    if ((dataGridViewShedule[0, info.RowIndex - info.RowIndex % 2] as DataGridViewTextBoxCellEx).ColumnSpan != 1)
                    {
                        (dataGridViewShedule[0, info.RowIndex - info.RowIndex % 2] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
                        (dataGridViewShedule[0, info.RowIndex - info.RowIndex % 2 + 1] as DataGridViewTextBoxCellEx).ColumnSpan = 3;
                    }
                    temp = (dataGridViewShedule[3, info.RowIndex - info.RowIndex % 2] as DataGridViewTextBoxCellEx);
                    temp.RowSpan = 1;
                    ClearRow(info.RowIndex - info.RowIndex % 2);
                    ClearRow(info.RowIndex - info.RowIndex % 2 + 1);
                    CancelSplit();
                    return;
                }
                if (соединитьToolStripMenuItem.Checked)
                {
                    //dataGridViewShedule[0, info.RowIndex].Value = "1";
                    //dataGridViewShedule[1, info.RowIndex].Value = "2";
                    //MessageBox.Show("|" + dataGridViewShedule[0, info.RowIndex].Value.ToString() + " | " + dataGridViewShedule[0, info.RowIndex].Value.ToString() + "|");
                    //MessageBox.Show((dataGridViewShedule[0, info.RowIndex - info.RowIndex % 2] as DataGridViewTextBoxCellEx).OwnerCell.Value.ToString());
                    ReturnToBaseRow(info.RowIndex - info.RowIndex % 2);
                    ClearRow(info.RowIndex - info.RowIndex % 2);
                    ClearRow(info.RowIndex - info.RowIndex % 2 + 1);
                    CancelSplit();
                    return;
                }
                if (!string.IsNullOrEmpty(s))
                {
                    dataGridViewShedule.DoDragDrop(s, DragDropEffects.Copy);
                    dataGridViewShedule[info.ColumnIndex, info.RowIndex].Value = activeDiscipline;
                    activeDiscipline = "";
                    listViewSubjects.DoDragDrop(s, DragDropEffects.Copy);
                    dataGridViewAudience.DoDragDrop(s, DragDropEffects.Copy);
                    Save();
                }
            }
            catch (Exception)
            { }
        }

        bool IsRowBaseType(int row)
        {
            return (dataGridViewShedule[0, row - row % 2] as DataGridViewTextBoxCellEx).ColumnSpan != 1 && (dataGridViewShedule[0, row - row % 2] as DataGridViewTextBoxCellEx).RowSpan != 1;
        }

        bool IsRowSplittedVertically(int row)
        {
            return (dataGridViewShedule[0, row - row % 2] as DataGridViewTextBoxCellEx).ColumnSpan == 1;
        }
        private void dataGridViewShedule_DragDrop(object sender, DragEventArgs e)
        {
            try//tut
            {
                string cellValue = e.Data.GetData(typeof(string)) as string;
                Point cursorLocation = this.PointToClient(new Point(e.X, e.Y));

                DataGridView.HitTestInfo hittest = dataGridViewShedule.HitTest(cursorLocation.X, cursorLocation.Y - 24);
                //MessageBox.Show(HasLectorFreeHours(cellvalue).ToString());
                if (hittest.RowIndex != -1)
                {
                    if (IsRowBaseType(hittest.RowIndex))
                    {

                    }
                    if (IsRowSplittedVertically(hittest.RowIndex))
                    {
                        bool IsAudience = int.TryParse(cellValue, out int _) && (hittest.ColumnIndex == 0 || hittest.ColumnIndex == 3);
                        bool IsLector = !int.TryParse(cellValue, out int _) && (hittest.ColumnIndex == 1 || hittest.ColumnIndex == 2);
                        if (IsAudience && AllScheduleGroup.IsAudienceEmpty(cellValue, hittest.RowIndex - hittest.RowIndex % 2) ||
                        IsLector && AllScheduleGroup.IsLectorFree(cellValue, hittest.RowIndex) && HasLectorFreeHours(cellValue))
                        {


                        }
                        //bool IsAudience = int.TryParse(cellvalue, out int _) && hittest.ColumnIndex == 1;
                        //bool IsLector = !int.TryParse(cellvalue, out int _) && hittest.ColumnIndex == 0;
                        if (IsAudience && AllScheduleGroup.IsAudienceEmpty(cellValue, hittest.RowIndex) ||
                        IsLector && AllScheduleGroup.IsLectorFree(cellValue, hittest.RowIndex) && HasLectorFreeHours(cellValue))
                        {
                            activeDiscipline = dataGridViewShedule[hittest.ColumnIndex, hittest.RowIndex].Value.ToString();
                            dataGridViewShedule[hittest.ColumnIndex, hittest.RowIndex].Value = cellValue;
                        }
                    }
                }

                ShowSubjects();
                //DisciplineCheck();
                Save();
            }
            catch
            { }
        }
        private void dataGridViewShedule_DragEnter(object sender, DragEventArgs e)
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
            if (IsSplitting()) CancelSplit();
            listViewSubjects.Show();
            dataGridViewAudience.Hide();
            listViewAudienceDescription.Hide();
        }

        private void AddLoadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSplitting()) CancelSplit();
            AddLoads.GenerateNewLoads();
            UpdateWorkLoads();
            ShowListViewGroup();
        }

        private void SeparateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSplitting()) CancelSplit();
        }

        private void поВертикалиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поВертикалиToolStripMenuItem.Checked = true;
            Cursor = Cursors.VSplit;
        }

        private void поГоризонталиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            поГоризонталиToolStripMenuItem.Checked = true;
            Cursor = Cursors.HSplit;
        }

        private void соединитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Obiectum obiectum = new Obiectum("qqq", "1");
            ClassRow classRow = new ClassRow(RowTypes.TwoGroupsAndTwoWeeks, obiectum);
            //MessageBox.Show(classRow.CountOfWeeks.ToString());
            classRow.VisualizeRow(dataGridViewShedule, 0);
            if (IsSplitting())
            {
                CancelSplit();
                return;
            }
            соединитьToolStripMenuItem.Checked = true;
            Cursor = Cursors.SizeAll;
        }
    }
}
