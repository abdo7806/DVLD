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

namespace DVLDPresentationLayer.LocalDLA
{
    public partial class SchedulTest : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        int _LocalDLA_ID = -1;
        int _TestAppointmentID = -1;
        int _UserID = -1;
        bool _NewApplication = false;// هل سوفا يوعيد الاختبار
        int _TestTypeID = -1;
        public SchedulTest(int localDLA_ID, int TestAppointmentID, int userID, bool newApplication, int TestTypeID)
        {
            InitializeComponent();
            _LocalDLA_ID = localDLA_ID;
            _TestAppointmentID = TestAppointmentID;
            _UserID = userID;
            _NewApplication = newApplication;
            _TestTypeID = TestTypeID;

            if (_TestAppointmentID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
        }

        private void SchedulTest_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew)
            {
            userControl51.ShowData(_LocalDLA_ID, _UserID,_TestAppointmentID, _TestTypeID, _NewApplication);
            }
            else
            {
                userControl51.ShowData(_LocalDLA_ID, _UserID,_TestAppointmentID, _TestTypeID, _NewApplication);

            }

           

            if (_NewApplication || _Mode == enMode.Update)
            {
                lblMode.Text = "Schedul Retake Test";
            }

            switch (_TestTypeID)
            {
                case 1:groupBox1.Text = "Vision Test";
                    pictureBox1.Image = Resources.Vision_512;
                        break;
                case 2:
                    groupBox1.Text = "Written Test";
                    pictureBox1.Image = Resources.Written_Test_512;
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

        // هاذا حدث في user Control يغلق الفورم الاذي يستخدمة
        private void userControl51_OnClculationComplete1(int obj)
        {
            this.Close();

        }
    }
}
