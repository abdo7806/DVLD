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
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentationLayer.LocalDLA
{
    public partial class frmAddAndUpdateLocalDLA : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        
       // clsPeople _Person;
        int _PersonID = -1;

        int _UserID = -1;

        ClsUsers _User = new ClsUsers();

        clsLicenseClasses _LicenseClasse = new clsLicenseClasses();

        clsLocalDLA1 _LocalDLA1 = new clsLocalDLA1();
        int _LocalDLA_ID = -1;


        clsApplications _Application = new clsApplications();

        public frmAddAndUpdateLocalDLA(int PersonID, int UserID, int localDLA_ID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _UserID = UserID;
            _LocalDLA_ID = localDLA_ID;

            if (_LocalDLA_ID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
        }

        // تعبئة انواع الرخص
        private void _FillLicenseClassesInComoboBox()
        {
           DataTable _DataTableLicenseClasses = clsLicenseClasses.GetAllLicenseClasses();


            foreach (DataRow row in _DataTableLicenseClasses.Rows)
            {

                cbLicenseClass.Items.Add(row["ClassName"]);

            }

            cbLicenseClass.SelectedIndex = 2;

        }

        private void _LoadData()
        {


            _FillLicenseClassesInComoboBox();// تعبئة انواع الرخص

          

            if (_Mode == enMode.AddNew)
            {

                lblMode.Text = "New Local Driving License Applications";

                // عرض بيانات الشخص 

                lblApplicationDate.Text = DateTime.Now.ToString("dd/mm/yyyy");
                lblApplicationFees.Text = clsApplicationTypes.Find(1).ApplicationFees.ToString();// يرجع سعر خدمة طلر الرخصة الجديدة
                lblCreatrdBy.Text = ClsUsers.Find(_UserID).UserName.ToString();// ارجاع اسم المستخدم الدي قام بانشأ الطلب
                return;
            }
           
                lblMode.Text = "Update Local Driving License Applications ";


            _LocalDLA1 = clsLocalDLA1.Find(_LocalDLA_ID);

            _Application = clsApplications.Fine(_LocalDLA1.ApplicationID);

            _PersonID = _Application.ApplicantPersonID;

            UserControl2._PersonID = _PersonID;


            userControl21.ShowDataPerson();

            userControl21.StopSearchingForSomeone();


            lblApplicationID.Text = _Application.ApplicationID.ToString();


            lblApplicationDate.Text = _Application.ApplicationDate.ToString("dd/mm/yyyy");
            lblApplicationFees.Text = clsApplicationTypes.Find(1).ApplicationFees.ToString();// يرجع سعر خدمة طلر الرخصة الجديدة
            lblCreatrdBy.Text = ClsUsers.Find(_UserID).UserName.ToString();// ارجاع اسم المستخدم الدي قام بانشأ الطلب
            cbLicenseClass.SelectedIndex = _LocalDLA1.LicenseClassID - 1;
        }
        private void frmAddAndUpdateLocalDLA_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(_PersonID.ToString() + "   "+ UserControl2._PersonID.ToString());
            _PersonID = -1;
            UserControl2._PersonID = -1;

            this.Close();
        }

        private void btnNaxt_Click(object sender, EventArgs e)
        {


         
            // هل تم اختيار شخص

            if (UserControl2._PersonID == -1)
            {
                MessageBox.Show("You must specify a person.", "Selected another person", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tabControl1.SelectedIndex = 0;
                return;
            }
            tabControl1.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
              _LicenseClasse = clsLicenseClasses.FindClassName(cbLicenseClass.Text);

                _PersonID = UserControl2._PersonID;

            // هل تم اختيار شخص
            if (UserControl2._PersonID == -1)
            {
                MessageBox.Show("You must specify a person.", "Selected another person", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            //التاكد اد لم يكون عندة طلب بنفس هاذا النوع من الرخصة
            if (!clsLocalDLA1.DoYouHaveDrivingLicenseOfThisType(_PersonID, _LicenseClasse.LicenseClassID, 2))
            {
                if(_Mode == enMode.AddNew)
                {
                    _Application.ApplicantPersonID = _PersonID;
                    _Application.ApplicationTypeID = 1;
                    _Application.ApplicationStatus = 1;
                    _Application.PaidFees = clsApplicationTypes.Find(1).ApplicationFees;// يرجع سعر خدمة طلر الرخصة الجديدة;
                    _Application.CreatedByUserID = _UserID;

                    _Application.Save();
                }


                _LocalDLA1.ApplicationID = _Application.ApplicationID;
                _LocalDLA1.LicenseClassID = cbLicenseClass.SelectedIndex + 1;// _LicenseClasse.LicenseClassID;



                if (_LocalDLA1.Save())
                {
                    //  clsTestAppointments.AddNewFullTestAppointments(LocalDLA1.LocalDLA_ID, _UserID);
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                _Mode = enMode.Update;
                lblMode.Text = "Update Local Driving License Applications ";
            }
            else
            {
                MessageBox.Show("Do you have an active application \n for the same license type?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

  

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // هل تم اختيار شخص

            if (UserControl2._PersonID == -1 && tabControl1.SelectedIndex == 1)
                {
                    MessageBox.Show("You must specify a person.", "Selected another person", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    tabControl1.SelectedIndex = 0;
                }
            
        }

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblApplicationFees.Text = clsLicenseClasses.Find(cbLicenseClass.SelectedIndex + 1).ClassFees.ToString();// يرجع سعر خدمة طلر الرخصة الجديدة
        }
    }
}
