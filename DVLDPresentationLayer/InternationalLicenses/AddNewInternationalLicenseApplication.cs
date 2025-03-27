using DVLDBusinessLayer;
using DVLDPresentationLayer.LocalDLA;
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

namespace DVLDPresentationLayer.InternationalLicenses
{
    public partial class AddNewInternationalLicenseApplication : Form
    {
        clsLicenses _License = new clsLicenses();

        int _UserID = -1;

        int _LicenseID = -1;

        public AddNewInternationalLicenseApplication(int user)
        {
            InitializeComponent();
            _UserID = user;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("yyyy-mm-dd");
            lblIssueDate.Text = DateTime.Now.ToString("yyyy-mm-dd");


            lblFees.Text = clsApplicationTypes.Find(6).ApplicationFees.ToString();

            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToString("yyyy-mm-dd");

            lblCreatedBY.Text = _UserID.ToString();
        }

        private void userControl81_OnClculationComplete1(int obj)
        {
            if (userControl81.TextSearch == "")
            {
                MessageBox.Show("There is no license with this number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LicenseID = Convert.ToInt32(userControl81.TextSearch);

            _License = clsLicenses.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("There is no license with this number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            clsInternationalLicenses IntLicenses = clsInternationalLicenses.Find(userControl81.LicenseID);

            if (IntLicenses != null)
            {
                //لديك بالفعل ترخيص دولي
                MessageBox.Show("This person already has an international license and its ID = " + IntLicenses.InternationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // لازم تطون الرخصة من نوع 3 اي قياده سيارة عادية
            if (_License.LicenseClass != 3)
            {
                MessageBox.Show("You must have a type 3 driving licence, i.e. driving a regular car.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            //هل الرخصة منتهية الصلاحية؟
            if (clsLicenses.IsTheLicenseExpired(_License.LicenseID))
            {
                MessageBox.Show("This license has expired.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            clsLocalDLA1 _LocalDLA1 = clsLocalDLA1.FindByApplicationID(_License.ApplicationID);



            //ليس لدا هاذي الرخصة طلب
            if (_LocalDLA1 == null)
            {
                MessageBox.Show("This license does not have an application.", "Selected another person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LicenseID = userControl81.LicenseID;
            lblLocalLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (_LicenseID == -1)// حداد الرخصة اولن
            {
                MessageBox.Show("Select the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            clsInternationalLicenses InternationalLicenses = clsInternationalLicenses.Find(_LicenseID);


            if (InternationalLicenses != null)//لديك بالفعل ترخيص دولي

            {
                MessageBox.Show("You already have an international licence.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (MessageBox.Show("Are you sure you want to Issue to licence?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                clsLicenses License = clsLicenses.Find(_LicenseID);

                int ApplicationID = License.ApplicationID;

                int PersonID = clsApplications.Fine(ApplicationID).ApplicantPersonID;

                clsApplications _Application = new clsApplications();


                _Application.ApplicantPersonID = PersonID;
                _Application.ApplicationTypeID = 6;
                _Application.ApplicationStatus = 3;
                _Application.PaidFees = clsApplicationTypes.Find(6).ApplicationFees;// يرجع سعر خدمة طلر الرخصة الجديدة;
                _Application.CreatedByUserID = _UserID;

                
                if(!_Application.Save())
                {
                    return;
                }

                InternationalLicenses = new clsInternationalLicenses();

                InternationalLicenses.ApplicationID = _Application.ApplicationID;
                InternationalLicenses.DriverID = License.DriverID;
                InternationalLicenses.LicenseID = _LicenseID;
                InternationalLicenses.ExpirationDate = DateTime.Now.AddYears(1);
                InternationalLicenses.CreatedByUserID = _UserID;

                if (InternationalLicenses.AddNewInternationalLicense())// اضافة رخصة دوليه جديدة
                {
                    //  clsTestAppointments.AddNewFullTestAppointments(LocalDLA1.LocalDLA_ID, _UserID);
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                lblI_L_ApplicationID.Text = InternationalLicenses.ApplicationID.ToString();
                lblI_L_LicenseID.Text = InternationalLicenses.InternationalLicenseID.ToString();
                btnSave.Enabled = false;
                llShowLicenseInfo.Enabled = true;

                // تحةيل اخفأ مربع البحث عن الرخصة
                userControl81.GroupBoxEnabledFalse();

            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int ApplicationID = clsLicenses.Find(_LicenseID).ApplicationID;
            int personID = clsApplications.Fine(ApplicationID).ApplicantPersonID;

            ShowPersonLicenseHistory frm = new ShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternationalLicenseDriverInfo frm = new InternationalLicenseDriverInfo(_LicenseID);
            frm.ShowDialog();

        }

    
    }
}
