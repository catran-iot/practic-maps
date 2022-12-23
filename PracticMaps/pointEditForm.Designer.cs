namespace PracticMapsProject
{
    partial class pointEdit_Form
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
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbLatitude = new System.Windows.Forms.Label();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.lbLongitude = new System.Windows.Forms.Label();
            this.tbLongitude = new System.Windows.Forms.TextBox();
            this.lbDepth = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.tbDepth = new System.Windows.Forms.TextBox();
            this.lbCaption = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.lbTrack = new System.Windows.Forms.Label();
            this.tbSession = new System.Windows.Forms.TextBox();
            this.tbTrack = new System.Windows.Forms.TextBox();
            this.lbCorrection = new System.Windows.Forms.Label();
            this.tbCorrection = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(21, 250);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "Сохранить";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(154, 250);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lbLatitude
            // 
            this.lbLatitude.AutoSize = true;
            this.lbLatitude.Location = new System.Drawing.Point(9, 92);
            this.lbLatitude.Name = "lbLatitude";
            this.lbLatitude.Size = new System.Drawing.Size(45, 13);
            this.lbLatitude.TabIndex = 2;
            this.lbLatitude.Text = "Широта";
            // 
            // tbLatitude
            // 
            this.tbLatitude.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbLatitude.Location = new System.Drawing.Point(89, 89);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(152, 20);
            this.tbLatitude.TabIndex = 3;
            // 
            // lbLongitude
            // 
            this.lbLongitude.AutoSize = true;
            this.lbLongitude.Location = new System.Drawing.Point(9, 118);
            this.lbLongitude.Name = "lbLongitude";
            this.lbLongitude.Size = new System.Drawing.Size(50, 13);
            this.lbLongitude.TabIndex = 2;
            this.lbLongitude.Text = "Долгота";
            // 
            // tbLongitude
            // 
            this.tbLongitude.Location = new System.Drawing.Point(89, 115);
            this.tbLongitude.Name = "tbLongitude";
            this.tbLongitude.Size = new System.Drawing.Size(152, 20);
            this.tbLongitude.TabIndex = 4;
            // 
            // lbDepth
            // 
            this.lbDepth.AutoSize = true;
            this.lbDepth.Location = new System.Drawing.Point(9, 144);
            this.lbDepth.Name = "lbDepth";
            this.lbDepth.Size = new System.Drawing.Size(65, 13);
            this.lbDepth.TabIndex = 2;
            this.lbDepth.Text = "Глубина (м)";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(9, 196);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(40, 13);
            this.lbTime.TabIndex = 2;
            this.lbTime.Text = "Время";
            // 
            // tbDepth
            // 
            this.tbDepth.Location = new System.Drawing.Point(89, 141);
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.Size = new System.Drawing.Size(152, 20);
            this.tbDepth.TabIndex = 5;
            // 
            // lbCaption
            // 
            this.lbCaption.AutoSize = true;
            this.lbCaption.Location = new System.Drawing.Point(9, 15);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(44, 13);
            this.lbCaption.TabIndex = 4;
            this.lbCaption.Text = "Сессия";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(89, 193);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(152, 20);
            this.tbTime.TabIndex = 6;
            // 
            // lbTrack
            // 
            this.lbTrack.AutoSize = true;
            this.lbTrack.Location = new System.Drawing.Point(9, 41);
            this.lbTrack.Name = "lbTrack";
            this.lbTrack.Size = new System.Drawing.Size(32, 13);
            this.lbTrack.TabIndex = 4;
            this.lbTrack.Text = "Трек";
            // 
            // tbSession
            // 
            this.tbSession.Location = new System.Drawing.Point(89, 12);
            this.tbSession.Name = "tbSession";
            this.tbSession.ReadOnly = true;
            this.tbSession.Size = new System.Drawing.Size(152, 20);
            this.tbSession.TabIndex = 0;
            // 
            // tbTrack
            // 
            this.tbTrack.Location = new System.Drawing.Point(89, 38);
            this.tbTrack.Name = "tbTrack";
            this.tbTrack.ReadOnly = true;
            this.tbTrack.Size = new System.Drawing.Size(152, 20);
            this.tbTrack.TabIndex = 1;
            // 
            // lbCorrection
            // 
            this.lbCorrection.AutoSize = true;
            this.lbCorrection.Location = new System.Drawing.Point(9, 170);
            this.lbCorrection.Name = "lbCorrection";
            this.lbCorrection.Size = new System.Drawing.Size(79, 13);
            this.lbCorrection.TabIndex = 2;
            this.lbCorrection.Text = "Коррекция (м)";
            // 
            // tbCorrection
            // 
            this.tbCorrection.Location = new System.Drawing.Point(89, 167);
            this.tbCorrection.Name = "tbCorrection";
            this.tbCorrection.ReadOnly = true;
            this.tbCorrection.Size = new System.Drawing.Size(152, 20);
            this.tbCorrection.TabIndex = 5;
            // 
            // pointEdit_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 288);
            this.Controls.Add(this.lbTrack);
            this.Controls.Add(this.lbCaption);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.tbCorrection);
            this.Controls.Add(this.tbDepth);
            this.Controls.Add(this.tbTrack);
            this.Controls.Add(this.tbLongitude);
            this.Controls.Add(this.tbSession);
            this.Controls.Add(this.tbLatitude);
            this.Controls.Add(this.lbCorrection);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbDepth);
            this.Controls.Add(this.lbLongitude);
            this.Controls.Add(this.lbLatitude);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "pointEdit_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Точка";
            this.Shown += new System.EventHandler(this.pointEdit_Form_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lbLatitude;
        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.Label lbLongitude;
        private System.Windows.Forms.TextBox tbLongitude;
        private System.Windows.Forms.Label lbDepth;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.TextBox tbDepth;
        private System.Windows.Forms.Label lbCaption;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label lbTrack;
        private System.Windows.Forms.TextBox tbSession;
        private System.Windows.Forms.TextBox tbTrack;
        private System.Windows.Forms.Label lbCorrection;
        private System.Windows.Forms.TextBox tbCorrection;
    }
}