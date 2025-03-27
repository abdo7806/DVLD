using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace DVLDPresentationLayer.LocalDLA
{
    public partial class TakeTest : Form
    {

        int _TestAppointmentID = -1;
        int _TestTypeID = -1;
        int _UserID = -1;
        bool _NewApplication = false;
        public TakeTest(int TestAppointmentID, int testTypeID, int userID, bool newApplication)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = testTypeID;
            _UserID = userID;
            _NewApplication = newApplication;
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {

 
            clsTestAppointments TestAppointment = clsTestAppointments.Fine(_TestAppointmentID, _TestTypeID);

          clsLocalDLA_View LocalDLA_View = clsLocalDLA_View.Fine(TestAppointment.LocalDLA_ID);

             lblLocalDLA_ID.Text = TestAppointment.LocalDLA_ID.ToString();
             lblApplicationForLicense.Text = LocalDLA_View.ClassName;
             lblName.Text = LocalDLA_View.FullName;
            lblTrial.Text = clsTests.TheNumberOfTestsHeFailed(TestAppointment.LocalDLA_ID, _TestTypeID).ToString();// عدد الاختبرات التي رسب فيها


            lblDate.Text = TestAppointment.AppointmentDate.ToString("yyyy-mm-dd");
            // clsLocalDLA1 _LocalDLA1 = clsLocalDLA1.Find(LocalDLA_ID);

             lblFees.Text = TestAppointment.PaidFees.ToString();// سعر الاختبار

            if(_NewApplication)
            {
                txtNotes.Enabled = false;
                rdFall.Enabled = false;
                rdPasse.Enabled = false;

                //if(TestAppointment.)
            }

            switch (_TestTypeID)
            {
                case 1:
                    groupBox1.Text = "Vision Test";
                    pictureBox4.Image = Resources.Vision_512;
                    break;
                case 2:
                    groupBox1.Text = "Written Test";
                    pictureBox4.Image = Resources.Written_Test_512;
                    break;
                case 3:
                    groupBox1.Text = "Street Test";
                    pictureBox1.Image = Resources.driving_test_512;
                    break;
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTests Test = new clsTests();

            Test.TestAppointmentID = _TestAppointmentID;

            if (rdPasse.Checked)
                Test.TestResult = true;
            else
                Test.TestResult = false;

           
            Test.Notes = txtNotes.Text;

            Test.CreatedByUserID = _UserID;

            if (Test.AddNewTest())
            {
                //اغالق موعد الاختبار 
                clsTestAppointments.ClosingTheTestDate(_TestAppointmentID);
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
            this.Close();

        }

    }
}
