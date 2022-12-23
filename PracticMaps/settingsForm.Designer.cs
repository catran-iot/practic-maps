namespace PracticMapsProject
{
    partial class settings_Form
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btMaxDepthColor = new System.Windows.Forms.Button();
            this.btMinDepthColor = new System.Windows.Forms.Button();
            this.tbMaxDepth = new System.Windows.Forms.TextBox();
            this.tbMinDepth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btSelectionColor = new System.Windows.Forms.Button();
            this.tbSelectionWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btPlaceColor = new System.Windows.Forms.Button();
            this.tbPlaceSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbPointSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbLowDepth = new System.Windows.Forms.CheckBox();
            this.cbGreatDepth = new System.Windows.Forms.CheckBox();
            this.btBadGreatDepthColor = new System.Windows.Forms.Button();
            this.tbBadLowDepth = new System.Windows.Forms.TextBox();
            this.btBadLowDepthColor = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tbBadGreatDepth = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(144, 302);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "Применить";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btMaxDepthColor);
            this.groupBox1.Controls.Add(this.btMinDepthColor);
            this.groupBox1.Controls.Add(this.tbMaxDepth);
            this.groupBox1.Controls.Add(this.tbMinDepth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 81);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подсветка глубин";
            // 
            // btMaxDepthColor
            // 
            this.btMaxDepthColor.BackColor = System.Drawing.Color.Black;
            this.btMaxDepthColor.Location = new System.Drawing.Point(293, 43);
            this.btMaxDepthColor.Name = "btMaxDepthColor";
            this.btMaxDepthColor.Size = new System.Drawing.Size(23, 23);
            this.btMaxDepthColor.TabIndex = 10;
            this.btMaxDepthColor.UseVisualStyleBackColor = false;
            // 
            // btMinDepthColor
            // 
            this.btMinDepthColor.BackColor = System.Drawing.Color.Black;
            this.btMinDepthColor.Location = new System.Drawing.Point(293, 17);
            this.btMinDepthColor.Name = "btMinDepthColor";
            this.btMinDepthColor.Size = new System.Drawing.Size(23, 23);
            this.btMinDepthColor.TabIndex = 11;
            this.btMinDepthColor.UseVisualStyleBackColor = false;
            // 
            // tbMaxDepth
            // 
            this.tbMaxDepth.Location = new System.Drawing.Point(145, 45);
            this.tbMaxDepth.Name = "tbMaxDepth";
            this.tbMaxDepth.Size = new System.Drawing.Size(100, 20);
            this.tbMaxDepth.TabIndex = 8;
            // 
            // tbMinDepth
            // 
            this.tbMinDepth.Location = new System.Drawing.Point(145, 19);
            this.tbMinDepth.Name = "tbMinDepth";
            this.tbMinDepth.Size = new System.Drawing.Size(100, 20);
            this.tbMinDepth.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Макс. глубина";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(251, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "м";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "м";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Мин. глубина";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btSelectionColor);
            this.groupBox2.Controls.Add(this.tbSelectionWidth);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btPlaceColor);
            this.groupBox2.Controls.Add(this.tbPlaceSize);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tbPointSize);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 108);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Маркеры";
            // 
            // btSelectionColor
            // 
            this.btSelectionColor.BackColor = System.Drawing.Color.Black;
            this.btSelectionColor.Location = new System.Drawing.Point(293, 69);
            this.btSelectionColor.Name = "btSelectionColor";
            this.btSelectionColor.Size = new System.Drawing.Size(23, 23);
            this.btSelectionColor.TabIndex = 13;
            this.btSelectionColor.UseVisualStyleBackColor = false;
            // 
            // tbSelectionWidth
            // 
            this.tbSelectionWidth.Location = new System.Drawing.Point(145, 71);
            this.tbSelectionWidth.Name = "tbSelectionWidth";
            this.tbSelectionWidth.Size = new System.Drawing.Size(100, 20);
            this.tbSelectionWidth.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Выделение";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(251, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "пикс";
            // 
            // btPlaceColor
            // 
            this.btPlaceColor.BackColor = System.Drawing.Color.Black;
            this.btPlaceColor.Location = new System.Drawing.Point(293, 43);
            this.btPlaceColor.Name = "btPlaceColor";
            this.btPlaceColor.Size = new System.Drawing.Size(23, 23);
            this.btPlaceColor.TabIndex = 9;
            this.btPlaceColor.UseVisualStyleBackColor = false;
            // 
            // tbPlaceSize
            // 
            this.tbPlaceSize.Location = new System.Drawing.Point(145, 45);
            this.tbPlaceSize.Name = "tbPlaceSize";
            this.tbPlaceSize.Size = new System.Drawing.Size(100, 20);
            this.tbPlaceSize.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Размер маркера места";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(251, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "пикс";
            // 
            // tbPointSize
            // 
            this.tbPointSize.Location = new System.Drawing.Point(145, 19);
            this.tbPointSize.Name = "tbPointSize";
            this.tbPointSize.Size = new System.Drawing.Size(100, 20);
            this.tbPointSize.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Размер маркера точки";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(251, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "пикс";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbLowDepth);
            this.groupBox3.Controls.Add(this.cbGreatDepth);
            this.groupBox3.Controls.Add(this.btBadGreatDepthColor);
            this.groupBox3.Controls.Add(this.tbBadLowDepth);
            this.groupBox3.Controls.Add(this.btBadLowDepthColor);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.tbBadGreatDepth);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(12, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(333, 83);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Некорректные глубины";
            // 
            // cbLowDepth
            // 
            this.cbLowDepth.AutoSize = true;
            this.cbLowDepth.Checked = true;
            this.cbLowDepth.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.cbLowDepth.Location = new System.Drawing.Point(16, 21);
            this.cbLowDepth.Name = "cbLowDepth";
            this.cbLowDepth.Size = new System.Drawing.Size(163, 17);
            this.cbLowDepth.TabIndex = 9;
            this.cbLowDepth.Text = "Отмечать глубины меньше";
            this.cbLowDepth.UseVisualStyleBackColor = true;
            // 
            // cbGreatDepth
            // 
            this.cbGreatDepth.AutoSize = true;
            this.cbGreatDepth.Checked = true;
            this.cbGreatDepth.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.cbGreatDepth.Location = new System.Drawing.Point(16, 50);
            this.cbGreatDepth.Name = "cbGreatDepth";
            this.cbGreatDepth.Size = new System.Drawing.Size(161, 17);
            this.cbGreatDepth.TabIndex = 9;
            this.cbGreatDepth.Text = "Отмечать глубины больше";
            this.cbGreatDepth.UseVisualStyleBackColor = true;
            // 
            // btBadGreatDepthColor
            // 
            this.btBadGreatDepthColor.BackColor = System.Drawing.Color.Black;
            this.btBadGreatDepthColor.Location = new System.Drawing.Point(293, 46);
            this.btBadGreatDepthColor.Name = "btBadGreatDepthColor";
            this.btBadGreatDepthColor.Size = new System.Drawing.Size(23, 23);
            this.btBadGreatDepthColor.TabIndex = 8;
            this.btBadGreatDepthColor.UseVisualStyleBackColor = false;
            this.btBadGreatDepthColor.Click += new System.EventHandler(this.btBadGreatDepthColor_Click);
            // 
            // tbBadLowDepth
            // 
            this.tbBadLowDepth.Location = new System.Drawing.Point(185, 19);
            this.tbBadLowDepth.Name = "tbBadLowDepth";
            this.tbBadLowDepth.Size = new System.Drawing.Size(60, 20);
            this.tbBadLowDepth.TabIndex = 7;
            // 
            // btBadLowDepthColor
            // 
            this.btBadLowDepthColor.BackColor = System.Drawing.Color.Black;
            this.btBadLowDepthColor.Location = new System.Drawing.Point(293, 17);
            this.btBadLowDepthColor.Name = "btBadLowDepthColor";
            this.btBadLowDepthColor.Size = new System.Drawing.Size(23, 23);
            this.btBadLowDepthColor.TabIndex = 8;
            this.btBadLowDepthColor.UseVisualStyleBackColor = false;
            this.btBadLowDepthColor.Click += new System.EventHandler(this.btBadLowDepthColor_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(251, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "м";
            // 
            // tbBadGreatDepth
            // 
            this.tbBadGreatDepth.Location = new System.Drawing.Point(185, 48);
            this.tbBadGreatDepth.Name = "tbBadGreatDepth";
            this.tbBadGreatDepth.Size = new System.Drawing.Size(60, 20);
            this.tbBadGreatDepth.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(251, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "м";
            // 
            // settings_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 334);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "settings_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Shown += new System.EventHandler(this.settings_Form_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btMaxDepthColor;
        private System.Windows.Forms.Button btMinDepthColor;
        private System.Windows.Forms.TextBox tbMaxDepth;
        private System.Windows.Forms.TextBox tbMinDepth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btSelectionColor;
        private System.Windows.Forms.TextBox tbSelectionWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btPlaceColor;
        private System.Windows.Forms.TextBox tbPlaceSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbPointSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbLowDepth;
        private System.Windows.Forms.CheckBox cbGreatDepth;
        private System.Windows.Forms.Button btBadGreatDepthColor;
        private System.Windows.Forms.TextBox tbBadLowDepth;
        private System.Windows.Forms.Button btBadLowDepthColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbBadGreatDepth;
        private System.Windows.Forms.Label label12;
    }
}