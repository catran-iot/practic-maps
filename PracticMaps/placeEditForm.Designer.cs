namespace PracticMapsProject
{
    partial class placeEdit_Form
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
            this.lbName = new System.Windows.Forms.Label();
            this.tbLongitude = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.lbLongitude = new System.Windows.Forms.Label();
            this.lbLatitude = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(13, 15);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(57, 13);
            this.lbName.TabIndex = 11;
            this.lbName.Text = "Название";
            // 
            // tbLongitude
            // 
            this.tbLongitude.Location = new System.Drawing.Point(93, 64);
            this.tbLongitude.Name = "tbLongitude";
            this.tbLongitude.Size = new System.Drawing.Size(152, 20);
            this.tbLongitude.TabIndex = 12;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(93, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(152, 20);
            this.tbName.TabIndex = 5;
            // 
            // tbLatitude
            // 
            this.tbLatitude.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbLatitude.Location = new System.Drawing.Point(93, 38);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(152, 20);
            this.tbLatitude.TabIndex = 10;
            // 
            // lbLongitude
            // 
            this.lbLongitude.AutoSize = true;
            this.lbLongitude.Location = new System.Drawing.Point(13, 67);
            this.lbLongitude.Name = "lbLongitude";
            this.lbLongitude.Size = new System.Drawing.Size(50, 13);
            this.lbLongitude.TabIndex = 8;
            this.lbLongitude.Text = "Долгота";
            // 
            // lbLatitude
            // 
            this.lbLatitude.AutoSize = true;
            this.lbLatitude.Location = new System.Drawing.Point(13, 41);
            this.lbLatitude.Name = "lbLatitude";
            this.lbLatitude.Size = new System.Drawing.Size(45, 13);
            this.lbLatitude.TabIndex = 9;
            this.lbLatitude.Text = "Широта";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(158, 104);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(25, 104);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "Сохранить";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // placeEdit_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 138);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbLongitude);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbLatitude);
            this.Controls.Add(this.lbLongitude);
            this.Controls.Add(this.lbLatitude);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "placeEdit_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Место";
            this.Shown += new System.EventHandler(this.placeEdit_Form_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbLongitude;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.Label lbLongitude;
        private System.Windows.Forms.Label lbLatitude;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
    }
}