using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.LocalDLA
{
    public partial class StreetTestAppointments : Form
    {
        private int _LocalDLA_ID = -1;
        int _UserID = 0;
        DataTable _DataTableStreetTestApplications = new DataTable();
        public StreetTestAppointments(int LocalDLA_ID, int UserID)
        {
            InitializeComponent();

            _LocalDLA_ID = LocalDLA_ID;
            _UserID = UserID;
        }


        private void _RefreshStreetTestApplicationsAList()
        {

            _DataTableStreetTestApplications = clsTestAppointments.GetAllAppointmentsByIDLocalDLA_ID(_LocalDLA_ID, 3);


            dataGridView1.DataSource = _DataTableStreetTestApplications;
            lblRecords.Text = _DataTableStreetTestApplications.Rows.Count.ToString();
            if (_DataTableStreetTestApplications.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Width = 140; // تغيير عرض العمود
                dataGridView1.Columns[1].Width = 200; // تغيير عرض العمود
                dataGridView1.Columns[2].Width = 140; // تغيير عرض العمود*/
            }

        }


        private void StreetTestAppointments_Load(object sender, EventArgs e)
        {
            userControl41.ShowData(_LocalDLA_ID);
            _RefreshStreetTestApplicationsAList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //هل هناك موعد فعال؟

            if (clsTestAppointments.IsThereAnActiveAppointment(_LocalDLA_ID, 3))
            {
                MessageBox.Show("There is already an open appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            bool NewApplication = false;

            if (_DataTableStreetTestApplications.Rows.Count > 0)
            {

                //هل هناك موعد ناجح
                if (clsTests.DidHeFailTheTest(_LocalDLA_ID, 3) > 0)
                {
                    MessageBox.Show("You cannot book another test date \n because you have already passed", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    NewApplication = true;

                }
            }

            SchedulTest frm = new SchedulTest(_LocalDLA_ID, -1, _UserID, NewApplication, 3);
            frm.ShowDialog();

            _RefreshStreetTestApplicationsAList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool NewApplication = false;


            //هل هناك موعد ناجح

            if (_DataTableStreetTestApplications.Rows.Count > 1 && !clsTestAppointments.Fine((int)dataGridView1.CurrentRow.Cells[0].Value, 3).IsLocked)
            {
                NewApplication = true;

            }

            SchedulTest frm = new SchedulTest(_LocalDLA_ID, (int)dataGridView1.CurrentRow.Cells[0].Value, _UserID, NewApplication, 3);
            frm.ShowDialog();
            _RefreshStreetTestApplicationsAList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsTestAppointments.Fine((int)dataGridView1.CurrentRow.Cells[0].Value, 3).IsLocked)
            {
                string Result = "";

                if (clsTests.Fine((int)dataGridView1.CurrentRow.Cells[0].Value).TestResult)
                {
                    Result = "Passe";
                }
                else
                {
                    Result = "Fall";
                }

                MessageBox.Show("Test Result: " + Result, "Result");
                return;
            }

            bool NewApplication = false;

            //هل هناك موعد ناجح
            if (clsTests.DidHeFailTheTest(_LocalDLA_ID, 3) > 0)
            {
                NewApplication = true;

            }

            TakeTest frm = new TakeTest((int)dataGridView1.CurrentRow.Cells[0].Value, 3, _UserID, NewApplication);
            frm.ShowDialog();
            _RefreshStreetTestApplicationsAList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
