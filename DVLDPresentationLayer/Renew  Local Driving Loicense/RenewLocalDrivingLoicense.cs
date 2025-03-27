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

namespace DVLDPresentationLayer.Renew__Local_Driving_Loicense
{
    public partial class RenewLocalDrivingLoicense : Form
    {

        clsLicenses _License = new clsLicenses();

        int _UserID = -1;

        int _LicenseID = -1;
        public RenewLocalDrivingLoicense(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RenewLocalDrivingLoicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("yyyy-mm-dd");
            lblIssueDate.Text = DateTime.Now.ToString("yyyy-mm-dd");


            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToString("yyyy-mm-dd");


            lblApplicationFees.Text = clsApplicationTypes.Find(2).ApplicationFees.ToString();
            lblLicenseFees.Text = clsApplicationTypes.Find(1).ApplicationFees.ToString();
            lblTotalFees.Text = (Convert.ToDouble(lblApplicationFees.Text) + Convert.ToDouble(lblLicenseFees.Text)).ToString();

            lblCreatedBY.Text = _UserID.ToString();
        }

        private void userControl81_OnClculationComplete1(int obj)
        {


            _LicenseID = userControl81.LicenseID;


            //هل الرخصة منتهية الصلاحية؟
            if (clsLicenses.IsTheLicenseExpired(_LicenseID))
            {
                
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;

                MessageBox.Show("Selected License is not yet expired, it will expire on:\n." + clsLicenses.Find(_LicenseID).ExpirationDate.ToString("yyyy-mm-dd"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            lblOldLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
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

        private void btnSave_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("Are you sure you want to Issue to licence?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                clsLicenses License = clsLicenses.Find(_LicenseID);

                int ApplicationID = License.ApplicationID;

                int PersonID = clsApplications.Fine(ApplicationID).ApplicantPersonID;

                clsApplications _Application = new clsApplications();


                _Application.ApplicantPersonID = PersonID;
                _Application.ApplicationTypeID = 2;
                _Application.ApplicationStatus = 3;
                _Application.PaidFees = clsApplicationTypes.Find(2).ApplicationFees;// يرجع سعر خدمة طلر الرخصة الجديدة;
                _Application.CreatedByUserID = _UserID;

                _Application.Save();

                if (!_Application.Save())
                {
                    return;
                }
                //جعل الرخصة القديم غير نشط
                clsLicenses.MakeTheLicenseNotActive(_LicenseID);

                License.Notes = txtNotes.Text;
                License.IssueDate = DateTime.Now;
            
                License.ExpirationDate = DateTime.Now.AddYears(clsLicenseClasses.Find(License.LicenseClass).DefaultValidityLength) ;
                License.IssueReason = 2;// سبب الاصدار

               if (License.AddNewLicense())// اضافة رخصة جديده من نوع تجديد رخصة
                {
                    //  clsTestAppointments.AddNewFullTestAppointments(LocalDLA1.LocalDLA_ID, _UserID);
                    MessageBox.Show("\r\nLicensed Renew Successfuly with ID= ."+ License.LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                    MessageBox.Show("Error: The license was not renewed successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                lblR_L_ApplicationID.Text = _Application.ApplicationID.ToString();
                lblIRenewwedLicenseID.Text = License.LicenseID.ToString();
                btnSave.Enabled = false;
                llShowLicenseInfo.Enabled = true;

                // تحةيل اخفأ مربع البحث عن الرخصة
                userControl81.GroupBoxEnabledFalse();
              
            }
        }

       
    }
}
