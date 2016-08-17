namespace TestOpenGL.Forms
{
    partial class MapEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.dataListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.depthComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.contentListBox = new System.Windows.Forms.ListBox();
            this.layerComboBox = new System.Windows.Forms.ComboBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.contentControl1 = new TestOpenGL.Forms.Controls.ContentControl();
            this.infoControl2 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl = new TestOpenGL.Forms.Controls.InfoControl();
            this.ad = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl1 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl3 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl4 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl5 = new TestOpenGL.Forms.Controls.InfoControl();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataListView
            // 
            listViewItem1.Tag = "1";
            listViewItem2.Tag = "2";
            this.dataListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.dataListView.Location = new System.Drawing.Point(12, 39);
            this.dataListView.MultiSelect = false;
            this.dataListView.Name = "dataListView";
            this.dataListView.Size = new System.Drawing.Size(269, 182);
            this.dataListView.TabIndex = 0;
            this.dataListView.UseCompatibleStateImageBehavior = false;
            this.dataListView.SelectedIndexChanged += new System.EventHandler(this.dataListView_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Название:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Описание:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(353, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 83);
            this.label4.TabIndex = 5;
            this.label4.Text = "label4";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(287, 171);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(93, 23);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // depthComboBox
            // 
            this.depthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.depthComboBox.FormattingEnabled = true;
            this.depthComboBox.Location = new System.Drawing.Point(399, 189);
            this.depthComboBox.Name = "depthComboBox";
            this.depthComboBox.Size = new System.Drawing.Size(67, 21);
            this.depthComboBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(396, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Глубина:";
            // 
            // contentListBox
            // 
            this.contentListBox.Enabled = false;
            this.contentListBox.FormattingEnabled = true;
            this.contentListBox.Location = new System.Drawing.Point(484, 171);
            this.contentListBox.Name = "contentListBox";
            this.contentListBox.Size = new System.Drawing.Size(120, 69);
            this.contentListBox.Sorted = true;
            this.contentListBox.TabIndex = 9;
            // 
            // layerComboBox
            // 
            this.layerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerComboBox.FormattingEnabled = true;
            this.layerComboBox.Items.AddRange(new object[] {
            "Фоны",
            "Блоки",
            "Декали"});
            this.layerComboBox.Location = new System.Drawing.Point(53, 12);
            this.layerComboBox.Name = "layerComboBox";
            this.layerComboBox.Size = new System.Drawing.Size(132, 21);
            this.layerComboBox.TabIndex = 10;
            this.layerComboBox.SelectedIndexChanged += new System.EventHandler(this.layerComboBox_SelectedIndexChanged);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(287, 198);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(93, 23);
            this.removeButton.TabIndex = 11;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // contentControl1
            // 
            this.contentControl1.Location = new System.Drawing.Point(12, 227);
            this.contentControl1.Name = "contentControl1";
            this.contentControl1.Size = new System.Drawing.Size(326, 120);
            this.contentControl1.TabIndex = 12;
            // 
            // infoControl2
            // 
            this.infoControl2.Location = new System.Drawing.Point(130, 5);
            this.infoControl2.Name = "infoControl2";
            this.infoControl2.Size = new System.Drawing.Size(196, 110);
            this.infoControl2.TabIndex = 0;
            // 
            // infoControl
            // 
            this.infoControl.Location = new System.Drawing.Point(130, 5);
            this.infoControl.Name = "infoControl";
            this.infoControl.Size = new System.Drawing.Size(196, 110);
            this.infoControl.TabIndex = 0;
            // 
            // ad
            // 
            this.ad.Location = new System.Drawing.Point(0, 0);
            this.ad.Name = "ad";
            this.ad.Size = new System.Drawing.Size(210, 130);
            this.ad.TabIndex = 0;
            // 
            // infoControl1
            // 
            this.infoControl1.Location = new System.Drawing.Point(130, 5);
            this.infoControl1.Name = "infoControl1";
            this.infoControl1.Size = new System.Drawing.Size(196, 110);
            this.infoControl1.TabIndex = 0;
            // 
            // infoControl3
            // 
            this.infoControl3.Location = new System.Drawing.Point(130, 5);
            this.infoControl3.Name = "infoControl3";
            this.infoControl3.Size = new System.Drawing.Size(196, 110);
            this.infoControl3.TabIndex = 0;
            // 
            // infoControl4
            // 
            this.infoControl4.Location = new System.Drawing.Point(130, 5);
            this.infoControl4.Name = "infoControl4";
            this.infoControl4.Size = new System.Drawing.Size(196, 110);
            this.infoControl4.TabIndex = 0;
            // 
            // infoControl5
            // 
            this.infoControl5.Location = new System.Drawing.Point(130, 5);
            this.infoControl5.Name = "infoControl5";
            this.infoControl5.Size = new System.Drawing.Size(196, 110);
            this.infoControl5.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Слой:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Содержимое слоя:";
            // 
            // MapEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 353);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.contentControl1);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.layerComboBox);
            this.Controls.Add(this.contentListBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.depthComboBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataListView);
            this.KeyPreview = true;
            this.Name = "MapEditorForm";
            this.Text = "Редактирование карты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form3_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView dataListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ComboBox depthComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox contentListBox;
        private System.Windows.Forms.ComboBox layerComboBox;
        private System.Windows.Forms.Button removeButton;
        private TestOpenGL.Forms.Controls.InfoControl infoControl;
        private TestOpenGL.Forms.Controls.InfoControl infoControl2;
        private TestOpenGL.Forms.Controls.InfoControl ad;
        private TestOpenGL.Forms.Controls.InfoControl infoControl1;
        private TestOpenGL.Forms.Controls.InfoControl infoControl3;
        private TestOpenGL.Forms.Controls.InfoControl infoControl4;
        private TestOpenGL.Forms.Controls.ContentControl contentControl1;
        private TestOpenGL.Forms.Controls.InfoControl infoControl5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}