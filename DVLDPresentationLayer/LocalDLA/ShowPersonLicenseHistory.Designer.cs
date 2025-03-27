namespace DVLDPresentationLayer.LocalDLA
{
    partial class ShowPersonLicenseHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPersonLicenseHistory));
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.userControl21 = new DVLDPresentationLayer.UserControl2();
            this.userControl71 = new DVLDPresentationLayer.UserControl7();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(581, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 45);
            this.label3.TabIndex = 190;
            this.label3.Text = "License History";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLDPresentationLayer.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox4.Location = new System.Drawing.Point(14, 218);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(289, 219);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 191;
            this.pictureBox4.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1153, 771);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(119, 48);
            this.btnClose.TabIndex = 196;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // userControl21
            // 
            this.userControl21.Location = new System.Drawing.Point(309, 57);
            this.userControl21.Name = "userControl21";
            this.userControl21.Size = new System.Drawing.Size(926, 438);
            this.userControl21.TabIndex = 195;
            // 
            // userControl71
            // 
            this.userControl71.BackColor = System.Drawing.Color.White;
            this.userControl71.Location = new System.Drawing.Point(12, 501);
            this.userControl71.Name = "userControl71";
            this.userControl71.Size = new System.Drawing.Size(1135, 335);
            this.userControl71.TabIndex = 197;
            // 
            // ShowPersonLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1274, 848);
            this.Controls.Add(this.userControl71);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.userControl21);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ShowPersonLicenseHistory";
            this.Text = "License History";
            this.Load += new System.EventHandler(this.ShowPersonLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        private UserControl2 userControl21;
        private System.Windows.Forms.Button btnClose;
        private UserControl7 userControl71;
    }
}