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
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentationLayer
{
    public partial class UserControl5 : UserControl
    {
           public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _TestTypeID = 0;// نوع الاختبار
        int _LocalDLA_ID = 0;//رقم طلب الرخصة الجديدة
        int _UserID = 0;// رقم مستخدم النطام
        bool _NewApplication = false;

        clsTestAppointments _TestAppointments = new clsTestAppointments();

        clsLocalDLA_View _LocalDLA_View = new clsLocalDLA_View();


        // هاذا حدث من اجل قفل الفورم الاساسي
        public event Action<int> OnClculationComplete1;
        protected virtual void Close(int c)
        {
            Action<int> handler = OnClculationComplete1;
            if (handler != null)
            {
                handler(c);
            }
        }

        public UserControl5()
        {
            InitializeComponent();
        }

        private void UserControl5_Load(object sender, EventArgs e)
        {

        }

        // اضهار البيانات
        public void ShowData(int LocalDLA_ID,int UserID,int TestAppointmentID, int TestTypeID, bool NewApplication)
        {



            if (TestAppointmentID != -1)
            {
                _Mode = enMode.Update;
                _TestAppointments.GetModeUpdate();
                _TestAppointments.TestAppointmentID = TestAppointmentID;
                if (NewApplication)
                {
                    lblRetakeTestApplicationID.Text = TestAppointmentID.ToString();
                }
            }

            _TestTypeID = TestTypeID; // نوع الاختبار
            _LocalDLA_ID = LocalDLA_ID;
            _UserID = UserID;


    

                _LocalDLA_View = clsLocalDLA_View.Fine(LocalDLA_ID);
                lblLocalDLA_ID.Text = _LocalDLA_View.LocalDLA_ID.ToString();
                lblApplicationForLicense.Text = _LocalDLA_View.ClassName;
                lblName.Text = _LocalDLA_View.FullName;

                dtDate.Text = _LocalDLA_View.ApplicationDate.ToString();

                clsLocalDLA1 _LocalDLA1 = clsLocalDLA1.Find(LocalDLA_ID);

                lblFees.Text = clsTestTypes.Find(TestTypeID).TestTypeFees.ToString();// سعر الاختبار

            lblTrial.Text = clsTests.TheNumberOfTestsHeFailed(LocalDLA_ID, TestTypeID).ToString();// عدد الاختبرات التي رسب فيها

            /*clsApplications _Application = clsApplications.Fine(_LocalDLA1.ApplicationID);
            lblTotalFees.Text = _Application.PaidFees.ToString();*/

            if (_Mode == enMode.Update)
            {
                // لو كان الموعد مغلق 
                if(clsTestAppointments.Fine(TestAppointmentID, TestTypeID).IsLocked)
                {
                    // مانخلية يعدل على الباقي
                    dtDate.Enabled = false;
                    btnSave.Enabled = false;
                    lblMessage.Visible = true;
                }
            }

            if (NewApplication)// لو احنا نعيد الاختبار
            {
                _NewApplication = NewApplication;
                grbRetakTestInfo.Enabled = true;
                lblRAppFess.Text = clsApplicationTypes.Find(8).ApplicationFees.ToString();// يرجع سعر خدمة طلر الرخصة الجديدة;
                lblTotalFees.Text = (Convert.ToInt32(lblFees.Text) + clsApplicationTypes.Find(8).ApplicationFees).ToString();
            }

        }

       /* public void EnabledFalseRetakTestInfo()
        {
            groupBox1.Enabled = false;
        }*/

        private void btnSave_Click(object sender, EventArgs e)
        {

            _TestAppointments.TestTypeID = _TestTypeID;
            _TestAppointments.LocalDLA_ID = _LocalDLA_View.LocalDLA_ID;
            _TestAppointments.AppointmentDate = dtDate.Value;
            _TestAppointments.PaidFees = clsTestTypes.Find(_TestTypeID).TestTypeFees;// سعر الاختبار
            _TestAppointments.CreatedByUserID = _UserID;
            _TestAppointments.IsLocked = false;


            if(_NewApplication)
            {
                _TestAppointments.PaidFees +=  clsApplicationTypes.Find(8).ApplicationFees;// يرجع سعر خدمة طلر الرخصة الجديدة;

            }


            if (_TestAppointments.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            if (OnClculationComplete1 != null)
            {
                Close(1);// من اجل غلق الفورم الذي يستخدمني
            }
            
        }

      
    }
}
