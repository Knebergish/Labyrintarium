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
            this.ControlEnabledIndicator = new System.Windows.Forms.PictureBox();
            this.FPSValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ControlEnabledIndicator)).BeginInit();
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
            // ControlEnabledIndicator
            // 
            this.ControlEnabledIndicator.BackColor = System.Drawing.Color.Red;
            this.ControlEnabledIndicator.Location = new System.Drawing.Point(0, 1);
            this.ControlEnabledIndicator.Name = "ControlEnabledIndicator";
            this.ControlEnabledIndicator.Size = new System.Drawing.Size(10, 10);
            this.ControlEnabledIndicator.TabIndex = 7;
            this.ControlEnabledIndicator.TabStop = false;
            // 
            // FPSValue
            // 
            this.FPSValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FPSValue.Location = new System.Drawing.Point(40, 2);
            this.FPSValue.Name = "FPSValue";
            this.FPSValue.Size = new System.Drawing.Size(47, 10);
            this.FPSValue.TabIndex = 8;
            this.FPSValue.Text = "99";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 625);
            this.Controls.Add(this.FPSValue);
            this.Controls.Add(this.logListBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GlControl);
            this.Controls.Add(this.ControlEnabledIndicator);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ControlEnabledIndicator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;

        /// <summary>
        /// Элемент формы, на котором рисуется игровое изображение.
        /// </summary>
        public Tao.Platform.Windows.SimpleOpenGlControl GlControl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        /// <summary>
        /// Компонент listBox для вывода игрового лога.
        /// </summary>
        public System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.PictureBox ControlEnabledIndicator;
        private System.Windows.Forms.Label FPSValue;
    }
}

