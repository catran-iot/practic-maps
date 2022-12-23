namespace PracticMapsProject
{
    partial class moveSelection_Form
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
            this.cbSession = new System.Windows.Forms.ComboBox();
            this.cbTrack = new System.Windows.Forms.ComboBox();
            this.lbSession = new System.Windows.Forms.Label();
            this.lbTrack = new System.Windows.Forms.Label();
            this.lbCaption = new System.Windows.Forms.Label();
            this.btMove = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbSession
            // 
            this.cbSession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSession.FormattingEnabled = true;
            this.cbSession.Location = new System.Drawing.Point(62, 33);
            this.cbSession.Name = "cbSession";
            this.cbSession.Size = new System.Drawing.Size(247, 21);
            this.cbSession.TabIndex = 0;
            this.cbSession.SelectedIndexChanged += new System.EventHandler(this.cbSession_SelectedIndexChanged);
            // 
            // cbTrack
            // 
            this.cbTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrack.FormattingEnabled = true;
            this.cbTrack.Location = new System.Drawing.Point(62, 60);
            this.cbTrack.Name = "cbTrack";
            this.cbTrack.Size = new System.Drawing.Size(247, 21);
            this.cbTrack.TabIndex = 1;
            // 
            // lbSession
            // 
            this.lbSession.AutoSize = true;
            this.lbSession.Location = new System.Drawing.Point(12, 36);
            this.lbSession.Name = "lbSession";
            this.lbSession.Size = new System.Drawing.Size(44, 13);
            this.lbSession.TabIndex = 2;
            this.lbSession.Text = "Сессия";
            // 
            // lbTrack
            // 
            this.lbTrack.AutoSize = true;
            this.lbTrack.Location = new System.Drawing.Point(12, 63);
            this.lbTrack.Name = "lbTrack";
            this.lbTrack.Size = new System.Drawing.Size(32, 13);
            this.lbTrack.TabIndex = 2;
            this.lbTrack.Text = "Трек";
            // 
            // lbCaption
            // 
            this.lbCaption.AutoSize = true;
            this.lbCaption.Location = new System.Drawing.Point(12, 9);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(156, 13);
            this.lbCaption.TabIndex = 2;
            this.lbCaption.Text = "Выберите место назначения:";
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(119, 94);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(92, 23);
            this.btMove.TabIndex = 3;
            this.btMove.Text = "Скопировать";
            this.btMove.UseVisualStyleBackColor = true;
            this.btMove.Click += new System.EventHandler(this.btMove_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(217, 94);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(92, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // moveSelection_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 129);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.lbTrack);
            this.Controls.Add(this.lbCaption);
            this.Controls.Add(this.lbSession);
            this.Controls.Add(this.cbTrack);
            this.Controls.Add(this.cbSession);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "moveSelection_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Скопировать выделенные точки";
            this.Shown += new System.EventHandler(this.moveSelection_Form_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSession;
        private System.Windows.Forms.ComboBox cbTrack;
        private System.Windows.Forms.Label lbSession;
        private System.Windows.Forms.Label lbTrack;
        private System.Windows.Forms.Label lbCaption;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btCancel;
    }
}