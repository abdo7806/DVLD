using DVLDBusinessLayer;
using DVLDPresentationLayer.DrivingLicenses;
using DVLDPresentationLayer.InternationalLicenses;
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

namespace DVLDPresentationLayer.DetainedLicenses
{
    public partial class AddDetainedLicense : Form
    {
        int _LicenseID = -1;
        int _UserID = -1;

        public AddDetainedLicense(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }

        private void userControl81_OnClculationComplete1(int obj)
        {
            _LicenseID = Convert.ToInt32(userControl81.TextSearch);


            _LicenseID = userControl81.LicenseID;
            lblLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            // هل الرحصة محجوزة من قبل
            if (clsDetainedLicenses.IsTheDetainIsReleased(_LicenseID))
            {
                //لقد تم حجز هذا الترخيص من قبل

                MessageBox.Show("This license has been reserved before.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


           // _License = clsLicenses.Find(_LicenseID);

            btnSave.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            ShowLicenseInfo frm = new ShowLicenseInfo(-1, _LicenseID);
            frm.ShowDialog();
        }

        private void AddDetainedLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("yyyy-mm-dd");
            lblCreatedBY.Text = ClsUsers.Find(_UserID).UserName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_LicenseID == -1)// حداد الرخصة اولن
            {
                MessageBox.Show("Select the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Issue to licence?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {


                clsDetainedLicenses DetainedLicense = new clsDetainedLicenses();

                DetainedLicense.LicenseID = _LicenseID;
                DetainedLicense.DetainDate = DateTime.Now;
                DetainedLicense.FineFees = float.Parse(txtFineFees.Text);
                DetainedLicense.CreatedByUserID = _UserID;
                DetainedLicense.IsReleased = false;

                  if (DetainedLicense.AddNewDetainedLicense())// اضافة رخصة محجوزه
                   {
                       //  clsTestAppointments.AddNewFullTestAppointments(LocalDLA1.LocalDLA_ID, _UserID);
                       MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                   }
                   else
                       MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                

               
                btnSave.Enabled = false;
                llShowLicenseInfo.Enabled = true;

                // تحةيل اخفأ مربع البحث عن الرخصة
                userControl81.GroupBoxEnabledFalse();
            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {

            // تحقق مما إذا كانت المدخلة ليست رقمًا
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // إذا كانت المدخلة ليست رقمًا، قم بإلغاء الحدث
                e.Handled = true;
            }
            
        }
    }
}
