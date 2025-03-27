using DVLDBusinessLayer;
using DVLDPresentationLayer.DrivingLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.LocalDLA
{
    public partial class frmLocalDLA : Form
    {
        DataTable _DataTableLocalDLA = new DataTable();
        int _UserID = -1;
        public frmLocalDLA(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _RefreshLocalDLAList()
        {
            _DataTableLocalDLA = clsLocalDLA_View.GetAllLocalDLA();

            dataGridView1.DataSource = _DataTableLocalDLA;
            lblRecords.Text = _DataTableLocalDLA.Rows.Count.ToString();
            dataGridView1.Columns[1].Width = 260; // تغيير عرض العمود
            dataGridView1.Columns[3].Width = 300; // تغيير عرض العمود
            dataGridView1.Columns[4].Width = 140; // تغيير عرض العمود
        }

        private void frmLocalDLA_Load(object sender, EventArgs e)
        {
            _RefreshLocalDLAList();
            cbFilterBy.SelectedIndex = 0;
            txtSearch.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddAndUpdateLocalDLA frm = new frmAddAndUpdateLocalDLA(-1, _UserID, -1);
            frm.ShowDialog();
            _RefreshLocalDLAList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtSearch.Visible = false;
                return;
            }

            txtSearch.Visible = true;
        }


        /*البحث ID*/
        private void SearchByLocalDLAID()
        {
            string pattern = @"^\d+$";

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTableLocalDLA;
                return;
            }

            DataRow[] RowPersens;
            if (Regex.IsMatch(txtSearch.Text.ToString(), pattern))
            {
                RowPersens = _DataTableLocalDLA.Select("L.D.LAPPID=" + txtSearch.Text.ToString());
            }
            else
            {
                txtSearch.Text = "";
                RowPersens = _DataTableLocalDLA.Select("L.D.LAPPID=" + "- 1");
            }


            DataTable dataTable = _DataTableLocalDLA.Clone();

            if (RowPersens.Length > 0)
            {
                dataTable = RowPersens.CopyToDataTable();
            }

            dataGridView1.DataSource = dataTable;

        }

        private void SearchByName()
        {

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTableLocalDLA;
                return;
            }


            DataRow[] RowPerson = _DataTableLocalDLA.Select($"{cbFilterBy.Text} like '%{txtSearch.Text.ToString()}%'");

            DataTable dataTable = _DataTableLocalDLA.Clone();

            if (RowPerson.Length > 0)
            {
                dataTable = RowPerson.CopyToDataTable();
                dataTable.DefaultView.Sort = cbFilterBy.Text.ToString();
            }


            dataGridView1.DataSource = dataTable;

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {

            if (cbFilterBy.Text == "L.D.LAPPID")
            {

                SearchByLocalDLAID();
            }
            else
            {
                SearchByName();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.LAPPID")
            {
                // تحقق مما إذا كانت المدخلة ليست رقمًا
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كانت المدخلة ليست رقمًا، قم بإلغاء الحدث
                    e.Handled = true;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // الاغاء الطلب
            clsApplications.CancelApplication((int)dataGridView1.CurrentRow.Cells[0].Value);

        //اغالق موعد الاختبار 
        //    clsTestAppointments.ClosingTheTestDate((int)dataGridView1.CurrentRow.Cells[0].Value);
            _RefreshLocalDLAList();

        }

        private void secheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisionTestApplications frm = new VisionTestApplications((int)dataGridView1.CurrentRow.Cells[0].Value, _UserID);
            frm.ShowDialog();
            _RefreshLocalDLAList();
        }

        private void editToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
           
            int LocalDLA_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (clsTests.DidHeFailTheTest(LocalDLA_ID, 1) == 0)
            {
                mnSecheduleVisionTest.Enabled = true;
                mnSecheduleWrittenTest.Enabled = false;
                mnSecheduleStreetTest.Enabled = false;
            }
            else if(clsTests.DidHeFailTheTest(LocalDLA_ID, 2) == 0)
            {
                mnSecheduleVisionTest.Enabled = false;
                mnSecheduleWrittenTest.Enabled = true;
                mnSecheduleStreetTest.Enabled = false;
            }
            else if (clsTests.DidHeFailTheTest(LocalDLA_ID, 3) == 0)
            {
                mnSecheduleVisionTest.Enabled = false;
                mnSecheduleWrittenTest.Enabled = false;
                mnSecheduleStreetTest.Enabled = true;
            }
            else
            {
                mnSecheduleStreetTest.Enabled = false;
                mnSecheduleVisionTest.Enabled = false;
                mnSecheduleWrittenTest.Enabled = false;
            }

        }

        private void mnSecheduleWrittenTest_Click(object sender, EventArgs e)
        {
            WrittenTestAppointments frm = new WrittenTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value, _UserID);
            frm.ShowDialog();
            _RefreshLocalDLAList();
        }

        private void mnSecheduleStreetTest_Click(object sender, EventArgs e)
        {
            StreetTestAppointments frm = new StreetTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value, _UserID);
            frm.ShowDialog();
            _RefreshLocalDLAList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            int PassedTest = (int)dataGridView1.CurrentRow.Cells[5].Value;

            if (PassedTest == 3)
            {

                mnSecheduleTest.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;

            }
            else
            {
                mnSecheduleTest.Enabled = true;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;



            }

            int ApplicationID = clsLocalDLA1.Find((int)dataGridView1.CurrentRow.Cells[0].Value).ApplicationID;

            if (clsApplications.IsApplicationCompleted(ApplicationID))
            {
                mnShowLicense.Enabled = true;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

                mnEditApplication.Enabled = false;
                mnDeleteApplication.Enabled = false;
                mnCancelApplication.Enabled = false;

            }
            else
            {
                mnSecheduleTest.Enabled = true;

                mnShowLicense.Enabled = false;

                mnEditApplication.Enabled = true;
                mnDeleteApplication.Enabled = true;
                mnCancelApplication.Enabled = true;

            }



            if((string)dataGridView1.CurrentRow.Cells[6].Value == "Cancelled")
            {
                
                mnShowLicense.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                mnSecheduleTest.Enabled = false;
                mnEditApplication.Enabled = false;
                mnCancelApplication.Enabled = false;
            }



        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewLicenses frm = new AddNewLicenses((int)dataGridView1.CurrentRow.Cells[0].Value, _UserID);
            frm.ShowDialog();
            _RefreshLocalDLAList();
        }

        private void mnShowLicense_Click(object sender, EventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo((int)dataGridView1.CurrentRow.Cells[0].Value, -1);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = clsPeople.FindNationalNo((string)dataGridView1.CurrentRow.Cells[2].Value);
          //  int Driv
            ShowPersonLicenseHistory frm = new ShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        private void mnDeleteApplication_Click(object sender, EventArgs e)
        {
            int LocalDLA_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            // لو كان عندة مواعيد اختبار يخرج من الدالة لاني ماقدر احذفة
            if (clsTestAppointments.CountTestAppointmentsByLocalDLA_ID(LocalDLA_ID))
            {
                MessageBox.Show("The Application cannot be deleted\n because you have a test date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }




            if (MessageBox.Show("Are you sure you want to delete this Application?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int ApplicationID = clsLocalDLA1.Find(LocalDLA_ID).ApplicationID;

                //Perform Delele and refresh
                if (clsLocalDLA1.DeleteLocalDLA1(LocalDLA_ID))
                {

                    MessageBox.Show("Application Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clsApplications.DeleteApplication(ApplicationID);
                    _RefreshLocalDLAList();


                }
                else
                    MessageBox.Show("Application is not deleted.", "not deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }



        }

        private void mnShowApplicationData_Click(object sender, EventArgs e)
        {
            ShowLocalDLA frm = new ShowLocalDLA((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void mnEditApplication_Click(object sender, EventArgs e)
        {
            frmAddAndUpdateLocalDLA frm = new frmAddAndUpdateLocalDLA(-1, _UserID, (int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshLocalDLAList();

        }

  
    }
}
