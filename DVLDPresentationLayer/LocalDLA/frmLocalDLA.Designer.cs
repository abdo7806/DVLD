namespace DVLDPresentationLayer.LocalDLA
{
    partial class frmLocalDLA
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocalDLA));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnShowApplicationData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnEditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.mnCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSecheduleTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSecheduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSecheduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSecheduleStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.issueDrivingLicenseFirstTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(343, 289);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(204, 28);
            this.txtSearch.TabIndex = 145;
            this.txtSearch.Visible = false;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 21);
            this.label3.TabIndex = 144;
            this.label3.Text = "Filter By:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.LAPPID",
            "NationalNo",
            "FullName",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(123, 289);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(204, 29);
            this.cbFilterBy.TabIndex = 143;
            this.cbFilterBy.TabStop = false;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(25, 790);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(21, 24);
            this.lblRecords.TabIndex = 142;
            this.lblRecords.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 790);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 24);
            this.label2.TabIndex = 141;
            this.label2.Text = "# Records:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(439, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(610, 48);
            this.label1.TabIndex = 140;
            this.label1.Text = "Local Driving License Applications";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(29, 333);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1402, 440);
            this.dataGridView1.TabIndex = 138;
            this.dataGridView1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnShowApplicationData,
            this.mnEditApplication,
            this.mnDeleteApplication,
            this.mnCancelApplication,
            this.mnSecheduleTest,
            this.toolStripMenuItem2,
            this.issueDrivingLicenseFirstTimeToolStripMenuItem,
            this.mnShowLicense,
            this.showPersonLicenseHistoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(342, 314);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // mnShowApplicationData
            // 
            this.mnShowApplicationData.Image = global::DVLDPresentationLayer.Properties.Resources.PersonDetails_321;
            this.mnShowApplicationData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnShowApplicationData.Name = "mnShowApplicationData";
            this.mnShowApplicationData.Size = new System.Drawing.Size(341, 38);
            this.mnShowApplicationData.Text = "Show Application Data";
            this.mnShowApplicationData.Click += new System.EventHandler(this.mnShowApplicationData_Click);
            // 
            // mnEditApplication
            // 
            this.mnEditApplication.Image = global::DVLDPresentationLayer.Properties.Resources.edit_32;
            this.mnEditApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnEditApplication.Name = "mnEditApplication";
            this.mnEditApplication.Size = new System.Drawing.Size(341, 38);
            this.mnEditApplication.Text = "Edit Application";
            this.mnEditApplication.Click += new System.EventHandler(this.mnEditApplication_Click);
            // 
            // mnDeleteApplication
            // 
            this.mnDeleteApplication.Image = global::DVLDPresentationLayer.Properties.Resources.Delete_32_2;
            this.mnDeleteApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnDeleteApplication.Name = "mnDeleteApplication";
            this.mnDeleteApplication.Size = new System.Drawing.Size(341, 38);
            this.mnDeleteApplication.Text = "Delete Application";
            this.mnDeleteApplication.Click += new System.EventHandler(this.mnDeleteApplication_Click);
            // 
            // mnCancelApplication
            // 
            this.mnCancelApplication.Image = global::DVLDPresentationLayer.Properties.Resources.Delete_32;
            this.mnCancelApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnCancelApplication.Name = "mnCancelApplication";
            this.mnCancelApplication.Size = new System.Drawing.Size(341, 38);
            this.mnCancelApplication.Text = "Cancel Application";
            this.mnCancelApplication.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // mnSecheduleTest
            // 
            this.mnSecheduleTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnSecheduleVisionTest,
            this.mnSecheduleWrittenTest,
            this.mnSecheduleStreetTest});
            this.mnSecheduleTest.Image = global::DVLDPresentationLayer.Properties.Resources.Schedule_Test_32;
            this.mnSecheduleTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnSecheduleTest.Name = "mnSecheduleTest";
            this.mnSecheduleTest.Size = new System.Drawing.Size(341, 38);
            this.mnSecheduleTest.Text = "Sechedule Test";
            this.mnSecheduleTest.MouseEnter += new System.EventHandler(this.editToolStripMenuItem_MouseEnter);
            // 
            // mnSecheduleVisionTest
            // 
            this.mnSecheduleVisionTest.Image = global::DVLDPresentationLayer.Properties.Resources.Vision_Test_32;
            this.mnSecheduleVisionTest.Name = "mnSecheduleVisionTest";
            this.mnSecheduleVisionTest.Size = new System.Drawing.Size(278, 36);
            this.mnSecheduleVisionTest.Text = "Sechedule Vision Test";
            this.mnSecheduleVisionTest.Click += new System.EventHandler(this.secheduleVisionTestToolStripMenuItem_Click);
            // 
            // mnSecheduleWrittenTest
            // 
            this.mnSecheduleWrittenTest.Image = global::DVLDPresentationLayer.Properties.Resources.Written_Test_32;
            this.mnSecheduleWrittenTest.Name = "mnSecheduleWrittenTest";
            this.mnSecheduleWrittenTest.Size = new System.Drawing.Size(278, 36);
            this.mnSecheduleWrittenTest.Text = "Sechedule Written Test";
            this.mnSecheduleWrittenTest.Click += new System.EventHandler(this.mnSecheduleWrittenTest_Click);
            // 
            // mnSecheduleStreetTest
            // 
            this.mnSecheduleStreetTest.Image = global::DVLDPresentationLayer.Properties.Resources.Street_Test_32;
            this.mnSecheduleStreetTest.Name = "mnSecheduleStreetTest";
            this.mnSecheduleStreetTest.Size = new System.Drawing.Size(278, 36);
            this.mnSecheduleStreetTest.Text = "Sechedule Street Test";
            this.mnSecheduleStreetTest.Click += new System.EventHandler(this.mnSecheduleStreetTest_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(338, 6);
            // 
            // issueDrivingLicenseFirstTimeToolStripMenuItem
            // 
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.New_Driving_License_321;
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Name = "issueDrivingLicenseFirstTimeToolStripMenuItem";
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Size = new System.Drawing.Size(341, 38);
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Text = "Issue Driving License (First Time)";
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Click += new System.EventHandler(this.issueDrivingLicenseFirstTimeToolStripMenuItem_Click);
            // 
            // mnShowLicense
            // 
            this.mnShowLicense.Image = global::DVLDPresentationLayer.Properties.Resources.License_View_32;
            this.mnShowLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnShowLicense.Name = "mnShowLicense";
            this.mnShowLicense.Size = new System.Drawing.Size(341, 38);
            this.mnShowLicense.Text = "Show License";
            this.mnShowLicense.Click += new System.EventHandler(this.mnShowLicense_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::DVLDPresentationLayer.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicenseHistoryToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(341, 38);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLDPresentationLayer.Properties.Resources.Local_32;
            this.pictureBox2.Location = new System.Drawing.Point(836, 72);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 53);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 148;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Image = global::DVLDPresentationLayer.Properties.Resources.New_Application_641;
            this.button1.Location = new System.Drawing.Point(1324, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 96);
            this.button1.TabIndex = 147;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.btnClose.Location = new System.Drawing.Point(1295, 779);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(136, 48);
            this.btnClose.TabIndex = 146;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDPresentationLayer.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(603, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 139;
            this.pictureBox1.TabStop = false;
            // 
            // frmLocalDLA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1451, 840);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmLocalDLA";
            this.Text = "frmLocalDLA";
            this.Load += new System.EventHandler(this.frmLocalDLA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnShowApplicationData;
        private System.Windows.Forms.ToolStripMenuItem mnEditApplication;
        private System.Windows.Forms.ToolStripMenuItem mnDeleteApplication;
        private System.Windows.Forms.ToolStripMenuItem mnCancelApplication;
        private System.Windows.Forms.ToolStripMenuItem mnSecheduleTest;
        private System.Windows.Forms.ToolStripMenuItem mnSecheduleVisionTest;
        private System.Windows.Forms.ToolStripMenuItem mnSecheduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem mnSecheduleStreetTest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem issueDrivingLicenseFirstTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnShowLicense;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
    }
}