using DVLDBusinessLayer;
using DVLDPresentationLayer.DrivingLicenses;
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
    public partial class ReleaseDetainedLicenses : Form
    {
        clsDetainedLicenses _DetainedLicense = new clsDetainedLicenses();
        int _LicenseID = -1;
        int _UserID = -1;
       
        public ReleaseDetainedLicenses(int UserID, int LicenseID)
        {
            InitializeComponent();
            _UserID = UserID;
            _LicenseID = LicenseID;
        }

        private void userControl81_OnClculationComplete1(int obj)
        {
            _LicenseID = Convert.ToInt32(userControl81.TextSearch);


            _LicenseID = userControl81.LicenseID;
            lblLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            // هل الرحصة ليسة محجوزة من قبل
            if (!clsDetainedLicenses.IsTheDetainIsReleased(_LicenseID))
            {
                //هذا الترخيص غير محجوز


                MessageBox.Show("This license is not reserved.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            // _License = clsLicenses.Find(_LicenseID);


            btnSave.Enabled = true;
            _DetainedLicense = clsDetainedLicenses.Find(_LicenseID);

            if(_DetainedLicense == null )
            {
                return;
            }

            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToString("yyyy-mm-dd");
            lblCreatedBY.Text = ClsUsers.Find(_DetainedLicense.CreatedByUserID).UserName;
            lblApplicationFees.Text = clsApplicationTypes.Find(5).ApplicationFees.ToString();// يرجع سعر خدمة طلر الرخصة الجديدة;

            lblFineFees.Text = _DetainedLicense.FineFees.ToString();

            lblTotalFees.Text = (float.Parse(lblApplicationFees.Text) + _DetainedLicense.FineFees).ToString();

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
                _Application.ApplicationTypeID = 5;
                _Application.ApplicationStatus = 3;
                _Application.PaidFees = clsApplicationTypes.Find(5).ApplicationFees;// يرجع سعر خدمة طلر الرخصة الجديدة;
                _Application.CreatedByUserID = _UserID;


                if (!_Application.Save())
                {
                    return;
                }

                //جعل الرخصة القديم غير نشط
              //  clsLicenses.MakeTheLicenseNotActive(_LicenseID);


                _DetainedLicense.IsReleased = true;
                _DetainedLicense.ReleaseDate = DateTime.Now;
                _DetainedLicense.ReleasedByUserID = _UserID;
                _DetainedLicense.ReleaseApplicationID = _Application.ApplicationID;
                

                if (_DetainedLicense.UpdateDetainedLicense())
                {
                    //  clsTestAppointments.AddNewFullTestAppointments(LocalDLA1.LocalDLA_ID, _UserID);
                    MessageBox.Show("\r\nLicensed Renew Successfuly with ID= ." + License.LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                    MessageBox.Show("Error: The license was not renewed successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                lblI_L_ApplicationID.Text = _Application.ApplicationID.ToString();
           
                btnSave.Enabled = false;
                llShowLicenseInfo.Enabled = true;

                // تحةيل اخفأ مربع البحث عن الرخصة
                userControl81.GroupBoxEnabledFalse();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReleaseDetainedLicenses_Load(object sender, EventArgs e)
        {
            if(_LicenseID == -1)
            {
                return;
            }

            userControl81.ShowData(_LicenseID);
            userControl81.TextSearch = _LicenseID.ToString();

            btnSave.Enabled = true;
            _DetainedLicense = clsDetainedLicenses.Find(_LicenseID);

            if (_DetainedLicense == null)
            {
                return;
            }

            lblLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToString("yyyy-mm-dd");
            lblCreatedBY.Text = ClsUsers.Find(_DetainedLicense.CreatedByUserID).UserName;
            lblApplicationFees.Text = clsApplicationTypes.Find(5).ApplicationFees.ToString();// يرجع سعر خدمة طلر الرخصة الجديدة;

            lblFineFees.Text = _DetainedLicense.FineFees.ToString();

            lblTotalFees.Text = (float.Parse(lblApplicationFees.Text) + _DetainedLicense.FineFees).ToString();

            // تحةيل اخفأ مربع البحث عن الرخصة
            userControl81.GroupBoxEnabledFalse();

        }
    }
}
