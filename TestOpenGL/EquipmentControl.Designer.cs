namespace TestOpenGL
{
    partial class EquipmentControl
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
            this.equipmentListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // equipmentListBox
            // 
            this.equipmentListBox.FormattingEnabled = true;
            this.equipmentListBox.Location = new System.Drawing.Point(0, 0);
            this.equipmentListBox.Name = "equipmentListBox";
            this.equipmentListBox.Size = new System.Drawing.Size(282, 199);
            this.equipmentListBox.TabIndex = 0;
            // 
            // EquipmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.equipmentListBox);
            this.Name = "EquipmentControl";
            this.Size = new System.Drawing.Size(282, 196);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox equipmentListBox;
    }
}
