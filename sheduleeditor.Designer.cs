﻿
namespace Shedule_Editor
{
    partial class sheduleeditor
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sheduleeditor));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.dataGridViewShedule = new System.Windows.Forms.DataGridView();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SaveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AudiencesForm = new System.Windows.Forms.ToolStripMenuItem();
            this.ShedulesForm = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.listViewGroup = new System.Windows.Forms.ListView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.dataGridViewAudience = new System.Windows.Forms.DataGridView();
            this.listViewAudienceDescription = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShedule)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudience)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewShedule
            // 
            this.dataGridViewShedule.AllowDrop = true;
            this.dataGridViewShedule.AllowUserToAddRows = false;
            this.dataGridViewShedule.AllowUserToDeleteRows = false;
            this.dataGridViewShedule.AllowUserToResizeColumns = false;
            this.dataGridViewShedule.AllowUserToResizeRows = false;
            this.dataGridViewShedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewShedule.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewShedule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewShedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewShedule.ColumnHeadersHeight = 30;
            this.dataGridViewShedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewShedule.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewShedule.EnableHeadersVisualStyles = false;
            this.dataGridViewShedule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridViewShedule.Location = new System.Drawing.Point(0, 24);
            this.dataGridViewShedule.MultiSelect = false;
            this.dataGridViewShedule.Name = "dataGridViewShedule";
            this.dataGridViewShedule.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewShedule.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewShedule.RowHeadersWidth = 60;
            this.dataGridViewShedule.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewShedule.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewShedule.ShowEditingIcon = false;
            this.dataGridViewShedule.Size = new System.Drawing.Size(472, 421);
            this.dataGridViewShedule.TabIndex = 0;
            this.dataGridViewShedule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewShedule_CellDoubleClick);
            this.dataGridViewShedule.SelectionChanged += new System.EventHandler(this.dataGridViewShedule_SelectionChanged);
            this.dataGridViewShedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridViewShedule_DragDrop);
            this.dataGridViewShedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridViewShedule_DragEnter);
            this.dataGridViewShedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewShedule_MouseDown);
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "image.png");
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "image.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveFileToolStripMenuItem,
            this.AudiencesForm,
            this.ShedulesForm});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(841, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SaveFileToolStripMenuItem
            // 
            this.SaveFileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SaveFileToolStripMenuItem.Name = "SaveFileToolStripMenuItem";
            this.SaveFileToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.SaveFileToolStripMenuItem.Text = "Сохранить в файл";
            this.SaveFileToolStripMenuItem.Click += new System.EventHandler(this.SaveFileToolStripMenuItem_Click);
            // 
            // AudiencesForm
            // 
            this.AudiencesForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AudiencesForm.Name = "AudiencesForm";
            this.AudiencesForm.Size = new System.Drawing.Size(79, 24);
            this.AudiencesForm.Text = "Аудитории";
            this.AudiencesForm.Click += new System.EventHandler(this.AudiencesForm_Click);
            // 
            // ShedulesForm
            // 
            this.ShedulesForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ShedulesForm.Name = "ShedulesForm";
            this.ShedulesForm.Size = new System.Drawing.Size(104, 24);
            this.ShedulesForm.Text = "Преподаватели";
            this.ShedulesForm.Click += new System.EventHandler(this.ShedulesForm_Click);
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(472, 24);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(2, 421);
            this.splitter3.TabIndex = 6;
            this.splitter3.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Location = new System.Drawing.Point(474, 24);
            this.splitter1.MaximumSize = new System.Drawing.Size(4, 2);
            this.splitter1.MinimumSize = new System.Drawing.Size(3, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 2);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // listViewFile
            // 
            this.listViewFile.AllowDrop = true;
            this.listViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFile.FullRowSelect = true;
            this.listViewFile.HideSelection = false;
            this.listViewFile.HoverSelection = true;
            this.listViewFile.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewFile.Location = new System.Drawing.Point(477, 24);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.ShowGroups = false;
            this.listViewFile.Size = new System.Drawing.Size(364, 421);
            this.listViewFile.TabIndex = 8;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.Details;
            this.listViewFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFile_DragDrop);
            this.listViewFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFile_DragEnter);
            this.listViewFile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewFile_MouseDown);
            // 
            // listViewGroup
            // 
            this.listViewGroup.BackColor = System.Drawing.Color.White;
            this.listViewGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewGroup.HideSelection = false;
            this.listViewGroup.Location = new System.Drawing.Point(477, 173);
            this.listViewGroup.Name = "listViewGroup";
            this.listViewGroup.Size = new System.Drawing.Size(364, 272);
            this.listViewGroup.TabIndex = 10;
            this.listViewGroup.UseCompatibleStateImageBehavior = false;
            this.listViewGroup.View = System.Windows.Forms.View.List;
            this.listViewGroup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewGroup_MouseClick);
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(477, 171);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(364, 2);
            this.splitter2.TabIndex = 11;
            this.splitter2.TabStop = false;
            // 
            // dataGridViewAudience
            // 
            this.dataGridViewAudience.AllowUserToAddRows = false;
            this.dataGridViewAudience.AllowUserToDeleteRows = false;
            this.dataGridViewAudience.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAudience.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAudience.ColumnHeadersVisible = false;
            this.dataGridViewAudience.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAudience.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewAudience.Location = new System.Drawing.Point(477, 24);
            this.dataGridViewAudience.MultiSelect = false;
            this.dataGridViewAudience.Name = "dataGridViewAudience";
            this.dataGridViewAudience.ReadOnly = true;
            this.dataGridViewAudience.RowHeadersVisible = false;
            this.dataGridViewAudience.RowTemplate.Height = 30;
            this.dataGridViewAudience.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAudience.Size = new System.Drawing.Size(364, 147);
            this.dataGridViewAudience.TabIndex = 12;
            this.dataGridViewAudience.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridViewAudience_DragDrop);
            this.dataGridViewAudience.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridViewAudience_DragEnter);
            this.dataGridViewAudience.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewAudience_MouseDown);
            // 
            // listViewAudienceDescription
            // 
            this.listViewAudienceDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewAudienceDescription.HideSelection = false;
            this.listViewAudienceDescription.Location = new System.Drawing.Point(477, 74);
            this.listViewAudienceDescription.Name = "listViewAudienceDescription";
            this.listViewAudienceDescription.Size = new System.Drawing.Size(364, 97);
            this.listViewAudienceDescription.TabIndex = 13;
            this.listViewAudienceDescription.UseCompatibleStateImageBehavior = false;
            // 
            // sheduleeditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(841, 445);
            this.Controls.Add(this.listViewAudienceDescription);
            this.Controls.Add(this.dataGridViewAudience);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.listViewGroup);
            this.Controls.Add(this.listViewFile);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.dataGridViewShedule);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "sheduleeditor";
            this.Text = "SheduleEditor";
            this.Load += new System.EventHandler(this.FormShedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShedule)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAudience)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewShedule;
        public System.Windows.Forms.ImageList imageListLarge;
        public System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem SaveFileToolStripMenuItem;
        private System.Windows.Forms.ListView listViewGroup;
        private System.Windows.Forms.Splitter splitter2;
        public System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ToolStripMenuItem AudiencesForm;
        private System.Windows.Forms.ToolStripMenuItem ShedulesForm;
        private System.Windows.Forms.DataGridView dataGridViewAudience;
        private System.Windows.Forms.ListView listViewAudienceDescription;
    }
}
