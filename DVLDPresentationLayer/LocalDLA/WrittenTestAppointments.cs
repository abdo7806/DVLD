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
    public partial class WrittenTestAppointments : Form
    {
        private int _LocalDLA_ID = -1;
        int _UserID = 0;
        DataTable _DataTableWrittenTestApplications = new DataTable();
        public WrittenTestAppointments(int LocalDLA_ID, int UserID)
        {
            InitializeComponent();
            _LocalDLA_ID = LocalDLA_ID;
            _UserID = UserID;
        }

    

        private void _RefreshWrittenTestAppointmentsList()
        {
            //_DataTableWrittenTestApplications = clsTestAppointments.GetAllAppointmentsByIDLocalDLA_ID(_LocalDLA_ID, "Written (Theory) Test");
            _DataTableWrittenTestApplications = clsTestAppointments.GetAllAppointmentsByIDLocalDLA_ID(_LocalDLA_ID, 2);


            dataGridView1.DataSource = _DataTableWrittenTestApplications;
            lblRecords.Text = _DataTableWrittenTestApplications.Rows.Count.ToString();
            if (_DataTableWrittenTestApplications.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Width = 140; // تغيير عرض العمود
                dataGridView1.Columns[1].Width = 200; // تغيير عرض العمود
                dataGridView1.Columns[2].Width = 140; // تغيير عرض العمود*/
            }

        }

        private void WrittenTestAppointments_Load(object sender, EventArgs e)
        {
            userControl41.ShowData(_LocalDLA_ID);
            _RefreshWrittenTestAppointmentsList();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //هل هناك موعد فعال؟

            if (clsTestAppointments.IsThereAnActiveAppointment(_LocalDLA_ID, 2))
            {
                MessageBox.Show("There is already an open appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            bool NewApplication = false;

            if (_DataTableWrittenTestApplications.Rows.Count > 0)
            {

                //هل هناك موعد ناجح
                if (clsTests.DidHeFailTheTest(_LocalDLA_ID, 2) > 0)
                {
                    MessageBox.Show("You cannot book another test date \n because you have already passed", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    NewApplication = true;

                }
            }

            SchedulTest frm = new SchedulTest(_LocalDLA_ID, -1, _UserID, NewApplication, 2);
            frm.ShowDialog();

            _RefreshWrittenTestAppointmentsList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool NewApplication = false;


            //هل هناك موعد ناجح

            if (_DataTableWrittenTestApplications.Rows.Count > 1 && !clsTestAppointments.Fine((int)dataGridView1.CurrentRow.Cells[0].Value, 2).IsLocked)
            {
                NewApplication = true;

            }

            SchedulTest frm = new SchedulTest(_LocalDLA_ID, (int)dataGridView1.CurrentRow.Cells[0].Value, _UserID, NewApplication, 2);
            frm.ShowDialog();
            _RefreshWrittenTestAppointmentsList();


        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsTestAppointments.Fine((int)dataGridView1.CurrentRow.Cells[0].Value, 2).IsLocked)
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
            if (clsTests.DidHeFailTheTest(_LocalDLA_ID, 1) > 2)
            {
                NewApplication = true;

            }

            TakeTest frm = new TakeTest((int)dataGridView1.CurrentRow.Cells[0].Value, 2, _UserID, NewApplication);
            frm.ShowDialog();
            _RefreshWrittenTestAppointmentsList();
        }

   
    }
}
