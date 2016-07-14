namespace TestOpenGL
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GlControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.controlEnabledIndicator = new System.Windows.Forms.PictureBox();
            this.FPSValueLabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.controlEnabledIndicator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // GlControl
            // 
            this.GlControl.AccumBits = ((byte)(0));
            this.GlControl.AutoCheckErrors = false;
            this.GlControl.AutoFinish = false;
            this.GlControl.AutoMakeCurrent = true;
            this.GlControl.AutoSwapBuffers = true;
            this.GlControl.BackColor = System.Drawing.Color.Black;
            this.GlControl.ColorBits = ((byte)(32));
            this.GlControl.DepthBits = ((byte)(16));
            this.GlControl.Location = new System.Drawing.Point(12, 12);
            this.GlControl.Name = "GlControl";
            this.GlControl.Size = new System.Drawing.Size(600, 600);
            this.GlControl.StencilBits = ((byte)(0));
            this.GlControl.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(618, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Визуализировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(618, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(221, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Инвентарь";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(618, 70);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(221, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Тест";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // logListBox
            // 
            this.logListBox.FormattingEnabled = true;
            this.logListBox.Location = new System.Drawing.Point(618, 99);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(221, 95);
            this.logListBox.TabIndex = 6;
            // 
            // controlEnabledIndicator
            // 
            this.controlEnabledIndicator.BackColor = System.Drawing.Color.Red;
            this.controlEnabledIndicator.Location = new System.Drawing.Point(0, 0);
            this.controlEnabledIndicator.Name = "controlEnabledIndicator";
            this.controlEnabledIndicator.Size = new System.Drawing.Size(10, 10);
            this.controlEnabledIndicator.TabIndex = 7;
            this.controlEnabledIndicator.TabStop = false;
            // 
            // FPSValueLabel
            // 
            this.FPSValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FPSValueLabel.Location = new System.Drawing.Point(40, 2);
            this.FPSValueLabel.Name = "FPSValueLabel";
            this.FPSValueLabel.Size = new System.Drawing.Size(47, 10);
            this.FPSValueLabel.TabIndex = 8;
            this.FPSValueLabel.Text = "99";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(618, 217);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(221, 45);
            this.trackBar1.TabIndex = 9;
            this.trackBar1.Value = 60;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 625);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.FPSValueLabel);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GlControl);
            this.Controls.Add(this.controlEnabledIndicator);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.controlEnabledIndicator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public Tao.Platform.Windows.SimpleOpenGlControl GlControl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.PictureBox controlEnabledIndicator;
        private System.Windows.Forms.Label FPSValueLabel;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

