using DVLDBusinessLayer;
using DVLDPresentationLayer.DrivingLicenses;
using DVLDPresentationLayer.LocalDLA;
using DVLDPresentationLayer.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class UserControl4 : UserControl
    {
        public int _PersonID = -1;
        clsLocalDLA_View _LocalDLA_View;
        public UserControl4()
        {
            InitializeComponent();
        }

        private void UserControl4_Load(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void ShowData(int LocalDLA_ID)
        {



            _LocalDLA_View = clsLocalDLA_View.Fine(LocalDLA_ID);
            lblLocalDLA_ID.Text = _LocalDLA_View.LocalDLA_ID.ToString();
            lblApplicationForLicense.Text = _LocalDLA_View.ClassName;
            lblPassedTests.Text = _LocalDLA_View.PassedTestCount.ToString() + "/3";
            lblStatus.Text = _LocalDLA_View.Status;
            lblApplication.Text = _LocalDLA_View.FullName;
            lblDate.Text = _LocalDLA_View.ApplicationDate.ToString("yyyy-mm-dd");
            lblSrartDate.Text = _LocalDLA_View.ApplicationDate.ToString("yyyy-mm-dd");
            lblType.Text = clsApplicationTypes.Find(1).ApplicationTypeTitle;


            if(_LocalDLA_View.Status == "Completed")
            {
                llShowLicenseInfo.Enabled = true;
            }
            else
            {
                llShowLicenseInfo.Enabled = false;
            }

            clsLocalDLA1 _LocalDLA1 = clsLocalDLA1.Find(LocalDLA_ID);

            clsApplications _Application = clsApplications.Fine(_LocalDLA1.ApplicationID);
            lblApplicationID.Text = _LocalDLA1.ApplicationID.ToString();
            lblFees.Text = _Application.PaidFees.ToString();

            lblCreatedBY.Text = ClsUsers.Find(_Application.CreatedByUserID).UserName;

            _PersonID = clsPeople.FindNationalNo(_LocalDLA_View.NationalNo);
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails frm = new PersonDetails(_PersonID);
            frm.ShowDialog();
        }

        public void EnterDataLicense(ref clsLicenses License)
        {
           // License.
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblStatus.Text == "Completed")
            {
                ShowLicenseInfo frm = new ShowLicenseInfo(_LocalDLA_View.LocalDLA_ID, -1);
                frm.ShowDialog();
            }
        }
    }
}
