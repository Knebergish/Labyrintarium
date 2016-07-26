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
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contentControl1 = new TestOpenGL.Forms.Controls.ContentControl();
            this.infoControl2 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl = new TestOpenGL.Forms.Controls.InfoControl();
            this.ad = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl1 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl3 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl4 = new TestOpenGL.Forms.Controls.InfoControl();
            this.infoControl5 = new TestOpenGL.Forms.Controls.InfoControl();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 39);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(269, 182);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Название:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Описание:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(380, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 83);
            this.label4.TabIndex = 5;
            this.label4.Text = "label4";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(188, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Add";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(307, 256);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(67, 21);
            this.comboBox1.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(304, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Глубина:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 227);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 69);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 9;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Фоны",
            "Блоки",
            "Декали"});
            this.comboBox2.Location = new System.Drawing.Point(12, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(132, 21);
            this.comboBox2.TabIndex = 10;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Remove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contentControl1
            // 
            this.contentControl1.Location = new System.Drawing.Point(287, 117);
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
            // MapEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 325);
            this.Controls.Add(this.contentControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
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

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private TestOpenGL.Forms.Controls.InfoControl infoControl;
        private TestOpenGL.Forms.Controls.InfoControl infoControl2;
        private TestOpenGL.Forms.Controls.InfoControl ad;
        private TestOpenGL.Forms.Controls.InfoControl infoControl1;
        private TestOpenGL.Forms.Controls.InfoControl infoControl3;
        private TestOpenGL.Forms.Controls.InfoControl infoControl4;
        private TestOpenGL.Forms.Controls.ContentControl contentControl1;
        private TestOpenGL.Forms.Controls.InfoControl infoControl5;
    }
}