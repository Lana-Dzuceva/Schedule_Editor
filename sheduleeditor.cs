using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media;

namespace Shedule_Editor
{
    public partial class sheduleeditor : Form
    {
        ListTeachers AllTeachers;
        ListSubgroupShedule AllSheduleGroup;
        string ActiveGroup;
        int formwidth;
        int formheight;
        
        public sheduleeditor()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dataGridViewShedule.Height = formheight = this.Height - 40;
            dataGridViewShedule.Width = formwidth = this.Width;
            dataGridViewShedule.RowTemplate.Height = 46;
            dataGridViewShedule.RowCount = 20;
            dataGridViewShedule.ColumnHeadersHeight = 40;
            string[] p = { "Пн", "Вт", "Ср", "Чт", "Пт" };
            for (int i = 0; i < 20; i += 4)
            {
                dataGridViewShedule.Rows[i].HeaderCell.Value = p[i / 4];
            }

            listViewFile.Columns.Add("Дисциплина");
            listViewFile.Columns.Add("Преподователь");
            listViewFile.Columns.Add("Тип занятия");
            listViewFile.Columns.Add("Кол-во часов");
            listViewFile.Columns[0].Width = 90;
            listViewFile.Columns[1].Width = 120;
            listViewFile.Columns[2].Width = 100;
            listViewFile.Columns[3].Width = 100;

            for (int i = 0; i < dataGridViewShedule.Columns.Count; i++)
            {
                dataGridViewShedule.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //dataGridViewShedule.DefaultCellStyle.SelectionBackColor = dataGridViewShedule.DefaultCellStyle.BackColor;

            //убираем мерцание и свойства выделения
            listViewFile.HoverSelection = false;
            listViewFile.FullRowSelect = true;
            Type type = listViewFile.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listViewFile, true, null);
        }

        // считываем данные с файлов и заполняем лист групп
        private void FormShedule_Load(object sender, EventArgs e)
        {
            using (StreamReader file = new StreamReader("newloads.json"))
            {
                string json = file.ReadToEnd();
                AllTeachers = JsonConvert.DeserializeObject<ListTeachers>(json);
            }

            ListGroups AllGroup;
            using (StreamReader file = new StreamReader("groups.json"))
            {
                string json = file.ReadToEnd();
                AllGroup = JsonConvert.DeserializeObject<ListGroups>(json);
            }

            using (StreamReader file = new StreamReader("subgroupShedule.json"))
            {
                string json = file.ReadToEnd();
                AllSheduleGroup = JsonConvert.DeserializeObject<ListSubgroupShedule>(json);
            }

            foreach (var item in AllTeachers.Teachers)
            {
                foreach (var sub in item.Subjects.Items)
                {
                    bool r = false;
                    foreach (var grp in AllGroup.Groups)
                    {
                        if (grp.name == sub.Group)
                        {
                            r = true;
                        }
                    }
                    if (!r)
                    {
                        Group newGroup = new Group(sub.Group);
                        AllGroup.Groups.Add(newGroup);
                    }
                }
            }

            foreach (var item in AllGroup.Groups)
            {
                ListViewItem group = new ListViewItem(item.name);
                listViewGroup.Items.Add(group);
            }
            //isFormLoaded = true;
        }

        // при нажатии на группу заполняется таблица с расписанием и отображаюся нагрузки преподователей в листе преподователей
        private void listViewGroup_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
                {
                    dataGridViewShedule.Rows[i].Cells[0].Value = "";
                }
                listViewFile.Items.Clear();
                var it = listViewGroup.SelectedItems[0];
                ActiveGroup = it.Text;
                dataGridViewShedule.Columns[0].HeaderText = ActiveGroup;
                ShowShedule();
                ShowLoads();
                DisciplineCheck();
            }
            catch
            {

            }
        }

        void Save()
        {
            if (ActiveGroup != null)
            {
                List<string> ls = new List<string>();
                for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
                {
                    if (dataGridViewShedule.Rows[i].Cells[0].Value == null)
                    {
                        ls.Add("");
                    }
                    else
                        ls.Add(dataGridViewShedule.Rows[i].Cells[0].Value.ToString());
                }

                SubgroupSchedule sb = new SubgroupSchedule(ActiveGroup, ls);

                bool r = false;
                foreach (var item in AllSheduleGroup.Shedule)
                {
                    if (item.Name == ActiveGroup)
                    {
                        item.Strings = ls;
                        r = true;
                    }
                }

                if (!r)
                {
                    AllSheduleGroup.Shedule.Add(sb);
                }

                var sg = JsonConvert.SerializeObject(AllSheduleGroup);
                //Console.WriteLine(sg);
                using (StreamWriter sw = new StreamWriter("subgroupShedule.json"))
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
                dataGridViewShedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                listViewFile.Items.Clear();
                ShowLoads();
                DisciplineCheck();
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
                    for (int i = 0; i < dataGridViewShedule.Rows.Count; i++)
                    {
                        dataGridViewShedule.Rows[i].Cells[0].Value = item.Strings[i];
                    }
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

                DataGridView.HitTestInfo hittest = dataGridViewShedule.HitTest(cursorLocation.X, cursorLocation.Y - 20);
                if (hittest.ColumnIndex != -1
                    && hittest.RowIndex != -1)
                    dataGridViewShedule[hittest.ColumnIndex, hittest.RowIndex].Value = cellvalue;
                
                ShowLoads();
                DisciplineCheck();
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
        private void dataGridViewShedule_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dataGridViewShedule.HitTest(e.X, e.Y);
            string s = dataGridViewShedule[info.ColumnIndex, info.RowIndex].Value.ToString();
            if (!string.IsNullOrEmpty(s))
            {
                dataGridViewShedule.DoDragDrop(s, DragDropEffects.Copy);
                dataGridViewShedule[info.ColumnIndex, info.RowIndex].Value = "";
                listViewFile.DoDragDrop(s, DragDropEffects.Copy);
            }
        }

        private void AudiencesForm_Click(object sender, EventArgs e)
        {
            listViewFile.Hide();
        }

        private void ShedulesForm_Click(object sender, EventArgs e)
        {
            listViewFile.Show();
        }
    }
}
    