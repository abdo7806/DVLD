using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.LocalDLA
{
    public partial class VisionTestApplications : Form
    {
       private int _LocalDLA_ID = -1;
        int _UserID = 0;
        DataTable _DataTableVisionTestApplications = new DataTable();
        public VisionTestApplications(int LocalDLA_ID, int UserID)
        {
            InitializeComponent();

            _LocalDLA_ID = LocalDLA_ID;
            _UserID = UserID;
        }


        private void _RefreshVisionTestApplicationsAList()
        {
           // _DataTableVisionTestApplications = clsTestAppointments.GetAllAppointmentsByIDLocalDLA_ID(_LocalDLA_ID, "Vision Test");
            _DataTableVisionTestApplications = clsTestAppointments.GetAllAppointmentsByIDLocalDLA_ID(_LocalDLA_ID, 1);


            dataGridView1.DataSource = _DataTableVisionTestApplications;
            lblRecords.Text = _DataTableVisionTestApplications.Rows.Count.ToString();
            if(_DataTableVisionTestApplications.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Width = 140; // تغيير عرض العمود
                dataGridView1.Columns[1].Width = 200; // تغيير عرض العمود
                dataGridView1.Columns[2].Width = 140; // تغيير عرض العمود*/
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          // UserControl4.x = _xx;
           userControl41.ShowData(_LocalDLA_ID);
            _RefreshVisionTestApplicationsAList();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            //هل هناك موعد فعال؟

            if (clsTestAppointments.IsThereAnActiveAppointment(_LocalDLA_ID, 1))
            {
                MessageBox.Show("There is already an open appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            bool NewApplication = false;

            if (_DataTableVisionTestApplications.Rows.Count > 0)
            {

                //هل هناك موعد ناجح
                if (clsTests.DidHeFailTheTest(_LocalDLA_ID, 1) > 0)
                {
                    MessageBox.Show("You cannot book another test date \n because you have already passed", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    NewApplication = true;

                }
            }

            SchedulTest frm = new SchedulTest(_LocalDLA_ID, -1,_UserID, NewApplication, 1);
            frm.ShowDialog();

            _RefreshVisionTestApplicationsAList();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool NewApplication = false;


            //هل هناك موعد ناجح

            if (_DataTableVisionTestApplications.Rows.Count > 1 && !clsTestAppointments.Fine((int)dataGridView1.CurrentRow.Cells[0].Value, 1).IsLocked)
            {
                NewApplication = true;

            }

            SchedulTest frm = new SchedulTest(_LocalDLA_ID, (int)dataGridView1.CurrentRow.Cells[0].Value, _UserID, NewApplication, 1); 
            frm.ShowDialog();
            _RefreshVisionTestApplicationsAList();


            
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (clsTestAppointments.Fine((int)dataGridView1.CurrentRow.Cells[0].Value, 1).IsLocked)
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
            if (clsTests.DidHeFailTheTest(_LocalDLA_ID, 1) > 0)
            {
                NewApplication = true;

            }

            TakeTest frm = new TakeTest((int)dataGridView1.CurrentRow.Cells[0].Value, 1, _UserID, NewApplication);
            frm.ShowDialog();
            _RefreshVisionTestApplicationsAList();

        }
    }
}
