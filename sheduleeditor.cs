using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
//using System.Windows.Media;

namespace Shedule_Editor
{
    public partial class sheduleeditor : Form
    {
        ListTeachers AllTeachers;
        ListSubgroupShedule AllSheduleGroup;
        AudienceGroup AllAudiences;
        ListGroups AllGroups;
        string ActiveGroup;
        string activeDiscipline = "";
        int activeDisX = -1;
        int activeDisY = -1;
        int formwidth;
        int formheight;
        string curDir = Environment.CurrentDirectory;
        List<int> audiences;

        public sheduleeditor()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridViewShedule.Height = formheight = this.Height - 40;
            dataGridViewShedule.Width = formwidth = this.Width;
            dataGridViewShedule.RowTemplate.Height = 46;
            dataGridViewShedule.RowCount = 20;
            dataGridViewShedule.ColumnHeadersHeight = 40;
            dataGridViewShedule.ColumnCount = 2;
            dataGridViewShedule.Columns[1].Width = 100;
            dataGridViewShedule.Columns[1].HeaderText = "Аудитория";
            string[] p = { "Пн", "Вт", "Ср", "Чт", "Пт" };
            for (int i = 0; i < 20; i += 4)
            {
                dataGridViewShedule.Rows[i].HeaderCell.Value = p[i / 4];
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
            //dataGridViewShedule.DefaultCellStyle.SelectionBackColor = dataGridViewShedule.DefaultCellStyle.BackColor;

            listViewFile.Columns.Add("Дисциплина");
            listViewFile.Columns.Add("Преподователь");
            listViewFile.Columns.Add("Тип занятия");
            listViewFile.Columns.Add("Кол-во часов");
            listViewFile.Columns[0].Width = 220;
            listViewFile.Columns[1].Width = 150;
            listViewFile.Columns[2].Width = 150;
            listViewFile.Columns[3].Width = 150;
            listViewFile.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12);

            //убираем мерцание и свойства выделения
            listViewFile.HoverSelection = false;
            listViewFile.FullRowSelect = true;
            Type type = listViewFile.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listViewFile, true, null);

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
                string json = file.ReadToEnd();
                AllSheduleGroup = JsonConvert.DeserializeObject<ListSubgroupShedule>(json);
            }

            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\loads.json"))
            {
                string json = file.ReadToEnd();
                AllTeachers = JsonConvert.DeserializeObject<ListTeachers>(json);
            }


            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\groups.json"))
            {
                string json = file.ReadToEnd();
                AllGroups = JsonConvert.DeserializeObject<ListGroups>(json);
            }
            using (StreamReader file = new StreamReader(curDir + @"\..\..\Files\audienceGroup.json"))
            {
                string json = file.ReadToEnd();
                AllAudiences = JsonConvert.DeserializeObject<AudienceGroup>(json);
            }
        }
        void ShowListViewGroup()
        {
            listViewGroup.Groups.Add(new ListViewGroup("Lana"));
            listViewGroup.Groups[0].Items.Add(new ListViewItem("Stas"));
            listViewGroup.Groups[0].Header = "hmm";
            listViewGroup.Groups.Add(new ListViewGroup("Lana2"));
            listViewGroup.Groups[1].Items.Add(new ListViewItem("NotStas"));
            foreach (var item in AllGroups.Groups)
            {
                ListViewItem group = new ListViewItem(item.Name);
                listViewGroup.Items.Add(group);
                listViewGroup.Groups[1].Items.Add(group);
            }

        }
        // считываем данные с файлов и заполняем лист групп
        private void FormShedule_Load(object sender, EventArgs e)
        {

            var infFileShGroup = new FileInfo(curDir + @"\..\..\Files\subgroupShedule.json");
            if (infFileShGroup.Length == 0)
                AddLoads.GenerateNewLoads();
            updateWorkLoads();


            ShowListViewGroup();

            dataGridViewAudience.Hide();
            //listViewAudienceDescription.Items.Add("описание аудитории");
            listViewAudienceDescription.Hide();


            audiences = new List<int>();
            foreach (var item in AllAudiences.Audiences)
            {
                audiences.Add(item.Number);
            }
            AudienceCheck();
        }

        // при нажатии на группу заполняется таблица с расписанием и отображаюся нагрузки преподователей в листе преподователей
        private void listViewGroup_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Теперь это бесполезно?? 
                for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
                {
                    dataGridViewShedule.Rows[i].Cells[0].Value = "";
                    dataGridViewShedule.Rows[i].Cells[1].Value = "";
                }
                //
                listViewFile.Items.Clear();
                var it = listViewGroup.SelectedItems[0];
                ActiveGroup = it.Text;
                dataGridViewShedule.Columns[0].HeaderText = ActiveGroup;
                ShowShedule();
                ShowLoads();
                DisciplineCheck();
            }
            catch (Exception)
            { }

        }

        void Save()
        {
            
            if (ActiveGroup != null)
            {
                List<string> ls = new List<string>();
                List<string> audiences = new List<string>();
                for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
                {
                    ls.Add(dataGridViewShedule.Rows[i].Cells[0].Value.ToString());
                    
                    audiences.Add(dataGridViewShedule.Rows[i].Cells[1].Value.ToString());

                }

                SubgroupSchedule sb = new SubgroupSchedule(ActiveGroup, ls, audiences);

                bool r = false;
                foreach (var item in AllSheduleGroup.Shedule)
                {
                    if (item.Name == ActiveGroup)
                    {
                        item.ScheduleFieldsSubjects = ls;
                        item.ScheduleFieldsAudiences = audiences;
                        r = true;
                        break;
                    }
                }

                if (!r)
                {//Когда это случается?
                    AllSheduleGroup.Shedule.Add(sb);
                    MessageBox.Show("qqqqq");
                }
                var curDir = Environment.CurrentDirectory;
                var sg = JsonConvert.SerializeObject(AllSheduleGroup);
                //Console.WriteLine(sg);
                using (StreamWriter sw = new StreamWriter(curDir + @"\..\..\Files\subgroupShedule.json"))
                    sw.WriteLine(sg);

                DisciplineCheck();
            }
        }
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void dataGridViewShedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //dataGridViewShedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                //listViewFile.Items.Clear();
                //ShowLoads();
                //DisciplineCheck();
            }
            catch
            {

            }
        }

        private void DisciplineCheck()
        {
            for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
            {
                string s = dataGridViewShedule.Rows[i].Cells[0].Value.ToString();
                for (int j = 0; j < listViewFile.Items.Count; j++)
                {
                    string t = listViewFile.Items[j].SubItems[0].Text + " " + listViewFile.Items[j].SubItems[1].Text + " " + listViewFile.Items[j].SubItems[2].Text;
                    if (s == t)
                    {
                        listViewFile.Items.RemoveAt(j);
                    }
                }
            }
        }
        // функция отображения аудиторий в его датагриде
        void AudienceCheck()
        {
            for (int ind = 0, row = 0, col = 0; ind < audiences.Count; ind++)
            {
                bool f = false;
                for (int i = 0; i < dataGridViewShedule.Columns.Count && !f; i++)
                {
                    for (int r = 0; r < dataGridViewShedule.Rows.Count && !f; r++)
                    {
                        //if (dataGridViewShedule[i, r].Value.ToString() == audiences[ind].ToString())
                        //f = true;
                    }
                }
                if (!f) dataGridViewAudience[col, row].Value = audiences[ind];
                col++;
                if (col == dataGridViewAudience.Columns.Count)
                {
                    col = 0;
                    row++;
                }
            }
        }
        private void ShowLoads()
        {
            listViewFile.Items.Clear();
            foreach (var item in AllTeachers.Teachers)
            {
                foreach (var sub in item.Subjects.Items)
                {
                    if (sub.Group == ActiveGroup)
                    {
                        ListViewItem lds = new ListViewItem(sub.Name);
                        lds.SubItems.Add(item.LastName + " " + item.FirstName);
                        lds.SubItems.Add(sub.ClassForm);
                        lds.SubItems.Add(sub.NumberOfHours.ToString());
                        listViewFile.Items.Add(lds);
                    }
                }
            }
        }
        private void ShowShedule()
        {
            foreach (var item in AllSheduleGroup.Shedule)
            {
                if (item.Name == ActiveGroup)
                {
                    //MessageBox.Show("FIND");
                    for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
                    {
                        dataGridViewShedule.Rows[i].Cells[0].Value = item.ScheduleFieldsSubjects[i];
                        dataGridViewShedule.Rows[i].Cells[1].Value = item.ScheduleFieldsAudiences[i];
                    }
                    break;
                }
            }

        }

        private void listViewFile_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int indexSource = listViewFile.Items.IndexOf(listViewFile.GetItemAt(e.X, e.Y));
                string s = listViewFile.Items[indexSource].SubItems[0].Text + " " + listViewFile.Items[indexSource].SubItems[1].Text + " " + listViewFile.Items[indexSource].SubItems[2].Text;
                listViewFile.DoDragDrop(s, DragDropEffects.Copy);
            }
            catch
            {

            }
        }

        private void dataGridViewShedule_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridViewShedule.ClearSelection();
        }
        private void listViewFile_DragDrop(object sender, DragEventArgs e)
        {
            activeDiscipline = "";
            ShowLoads();
            DisciplineCheck();
        }
        private void listViewFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }


        private void dataGridViewShedule_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string cellvalue = e.Data.GetData(typeof(string)) as string;
                Point cursorLocation = this.PointToClient(new Point(e.X, e.Y));

                DataGridView.HitTestInfo hittest = dataGridViewShedule.HitTest(cursorLocation.X, cursorLocation.Y - 24);


                if (hittest.ColumnIndex != -1
                    && hittest.RowIndex != -1)
                {
                    if (int.TryParse(cellvalue, out int _) && hittest.ColumnIndex == 1 && AllSheduleGroup.IsAudienceEmpty(cellvalue, hittest.RowIndex % 4) ||
                        !int.TryParse(cellvalue, out int _) && hittest.ColumnIndex == 0 && AllSheduleGroup.IsLectorFree(cellvalue, hittest.RowIndex % 4))
                    //(hittest.ColumnIndex != activeDisX || hittest.RowIndex != activeDisY))
                    {
                        activeDiscipline = dataGridViewShedule[hittest.ColumnIndex, hittest.RowIndex].Value.ToString();
                        dataGridViewShedule[hittest.ColumnIndex, hittest.RowIndex].Value = cellvalue;
                    }
                }

                ShowLoads();
                DisciplineCheck();
                Save();
            }
            catch
            {// MessageBox.Show("ERROR");
            }
        }
        private void dataGridViewShedule_DragEnter(object sender, DragEventArgs e)
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
                DataGridView.HitTestInfo info = dataGridViewShedule.HitTest(e.X, e.Y);

                string s = dataGridViewShedule[info.ColumnIndex, info.RowIndex].Value.ToString();
                activeDisX = info.ColumnIndex;
                activeDisY = info.RowIndex;
                if (!string.IsNullOrEmpty(s))
                {
                    dataGridViewShedule.DoDragDrop(s, DragDropEffects.Copy);
                    dataGridViewShedule[info.ColumnIndex, info.RowIndex].Value = activeDiscipline;
                    activeDiscipline = "";
                    activeDisX = -1;
                    activeDisY = -1;
                    listViewFile.DoDragDrop(s, DragDropEffects.Copy);
                    dataGridViewAudience.DoDragDrop(s, DragDropEffects.Copy);
                    Save();
                }
            }
            catch (Exception)
            { }
        }

        private void AudiencesForm_Click(object sender, EventArgs e)
        {
            listViewFile.Hide();
            dataGridViewAudience.Show();
            listViewAudienceDescription.Show();
        }

        private void ShedulesForm_Click(object sender, EventArgs e)
        {
            listViewFile.Show();
            dataGridViewAudience.Hide();
            listViewAudienceDescription.Hide();
        }

        private void dataGridViewAudience_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                DataGridView.HitTestInfo info = dataGridViewAudience.HitTest(e.X, e.Y);
                string s = dataGridViewAudience[info.ColumnIndex, info.RowIndex].Value.ToString();
                if (!string.IsNullOrEmpty(s))
                {
                    foreach (var item in AllAudiences.Audiences)
                    {
                        if (item.Number.ToString() == s)
                        {
                            listViewAudienceDescription.Items.Clear();
                            ListViewItem listViewItem = new ListViewItem(s);
                            listViewItem.SubItems.Add(item.CountOfSeats.ToString());
                            listViewItem.SubItems.Add(item.ChalkBoard ? "Есть" : "Нет");
                            listViewItem.SubItems.Add(item.MarkerBoard ? "Есть" : "Нет");
                            listViewItem.SubItems.Add(item.NumberOfComputers.ToString());
                            listViewItem.SubItems.Add(item.Projector ? "Есть" : "Нет");
                            listViewAudienceDescription.Items.Add(listViewItem);
                        }

                    }
                    dataGridViewShedule.DoDragDrop(s, DragDropEffects.Copy);
                    dataGridViewAudience[info.ColumnIndex, info.RowIndex].Value = "";

                }
                AudienceCheck();
            }
            catch (Exception)
            {
            }

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

        private void AddLoadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLoads.GenerateNewLoads();
            updateWorkLoads();
            listViewGroup.Items.Clear();
            ShowListViewGroup();
        }


    }
}
