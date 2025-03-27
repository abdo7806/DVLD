using DVLDBusinessLayer;
using DVLDPresentationLayer.DrivingLicenses;
using DVLDPresentationLayer.LocalDLA;
using DVLDPresentationLayer.Renew__Local_Driving_Loicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.Replacement_for_License
{
    public partial class ReplacementforLicense : Form
    {
        int _LicenseID = -1;
        int _UserID = -1;

        public ReplacementforLicense(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
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

        private void ReplacementforLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString("yyyy-mm-dd");


          if (rdDamagedLicense.Checked)
            {
                lblApplicationFees.Text = clsApplicationTypes.Find(4).ApplicationFees.ToString();
                lblMode.Text = "Replacement for  Damaged License";
                this.Text = "Replacement for Damaged License";
            }
            else
            {
                lblApplicationFees.Text = clsApplicationTypes.Find(3).ApplicationFees.ToString();
                lblMode.Text = "Replacement for  Lost License";
                this.Text = "Replacement for Lost License"; lblApplicationFees.Text = clsApplicationTypes.Find(3).ApplicationFees.ToString();
            }

                lblApplicationFees.Text = clsApplicationTypes.Find(4).ApplicationFees.ToString();
            lblCreatedBY.Text = _UserID.ToString();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo(-1, _LicenseID);
            frm.ShowDialog();
        }

        private void userControl81_OnClculationComplete1(int obj)
        {
            _LicenseID = userControl81.LicenseID;



            //هل الرخصة منتهية فعالة؟
            if (clsLicenses.IsTheLicenseIsActive(_LicenseID))
            {

                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                //الترخيص المحدد ليس نشطًا بعد، اختر ترخيصًا نشطًا

                MessageBox.Show("Selected License is not yet Active, choose an Active License.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblOldLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = true;
        }

        private void rdDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationTypes.Find(4).ApplicationFees.ToString();
            lblMode.Text = "Replacement for  Damaged License";
            this.Text = "Replacement for Damaged License";
        }

        private void rdLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationTypes.Find(3).ApplicationFees.ToString();
            lblMode.Text = "Replacement for  Lost License";
            this.Text = "Replacement for Lost License";
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

                _Application.ApplicationStatus = 3;
                _Application.CreatedByUserID = _UserID;


                if (rdDamagedLicense.Checked)
                {
                    _Application.PaidFees = clsApplicationTypes.Find(4).ApplicationFees;// يرجع سعر خدمة طلر الرخصة الجديدة;

                    _Application.ApplicationTypeID = 4;
                    License.IssueReason = 4;// سبب الاصدار بدل تالف
                }
                else
                {
                    _Application.PaidFees = clsApplicationTypes.Find(3).ApplicationFees;// يرجع سعر خدمة طلر الرخصة الجديدة;

                    _Application.ApplicationTypeID = 3;
                    License.IssueReason = 3;// سبب الاصدار بدل فاقد
                }


                // اضافة طلب من نوع بدل فاقد او بدل تالف

                if (!_Application.Save())
                {
                    return;
                }


                //جعل الرخصة القديم غير نشط
                clsLicenses.MakeTheLicenseNotActive(_LicenseID);



                // اضافة رخصه جديده سوى كانات بدل فاقد او بدل تالف

                if (License.AddNewLicense())
                {
                    //  clsTestAppointments.AddNewFullTestAppointments(LocalDLA1.LocalDLA_ID, _UserID);
                    MessageBox.Show("\r\nLicensed Renew Successfuly with ID= ." + License.LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    clsInternationalLicenses.EditingTheLicenseNumberInTheInternationalLicense(License.LicenseID, _LicenseID);
                }
                else
                    MessageBox.Show("Error: The license was not renewed successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                lblL_L_ApplicationID.Text = _Application.ApplicationID.ToString();
                lblIReplacementLicenseID.Text = License.LicenseID.ToString();
                btnSave.Enabled = false;
                llShowLicenseInfo.Enabled = true;

                //  اخفأ مربع البحث عن الرخصة الفيلتير
                userControl81.GroupBoxEnabledFalse();

                _LicenseID = License.LicenseID;
            }
        }
    }
}
