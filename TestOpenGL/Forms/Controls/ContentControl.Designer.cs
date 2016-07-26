namespace TestOpenGL.Forms.Controls
{
    partial class ContentControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.contentList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.infoControl = new TestOpenGL.Forms.Controls.InfoControl();
            this.SuspendLayout();
            // 
            // contentList
            // 
            this.contentList.FormattingEnabled = true;
            this.contentList.Location = new System.Drawing.Point(5, 20);
            this.contentList.Name = "contentList";
            this.contentList.Size = new System.Drawing.Size(120, 95);
            this.contentList.TabIndex = 0;
            this.contentList.SelectedIndexChanged += new System.EventHandler(this.contentList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Содержимое ячейки:";
            // 
            // infoControl
            // 
            this.infoControl.Location = new System.Drawing.Point(131, 5);
            this.infoControl.Name = "infoControl";
            this.infoControl.Size = new System.Drawing.Size(192, 110);
            this.infoControl.TabIndex = 3;
            // 
            // ContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.infoControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contentList);
            this.Name = "ContentControl";
            this.Size = new System.Drawing.Size(326, 120);
            this.Load += new System.EventHandler(this.ContentControl_Load);
            this.Resize += new System.EventHandler(this.ContentControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox contentList;
        private System.Windows.Forms.Label label1;
        private InfoControl infoControl;
    }
}
