using DVLDPresentationLayer.Application_Types;
using DVLDPresentationLayer.DetainedLicenses;
using DVLDPresentationLayer.Drivers;
using DVLDPresentationLayer.InternationalLicenses;
using DVLDPresentationLayer.LocalDLA;
using DVLDPresentationLayer.Renew__Local_Driving_Loicense;
using DVLDPresentationLayer.Replacement_for_License;
using DVLDPresentationLayer.Test_Types;
using DVLDPresentationLayer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class Index : Form
    {
        int _UserID = -1;
        public Index(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListDriver frm = new ListDriver();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople frm = new ManagePeople();
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ManageUsers frm = new ManageUsers();
            frm.ShowDialog();
        }


        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserDetails frm = new ShowUserDetails(_UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_Password frm = new Change_Password(_UserID);
            frm.ShowDialog();
        }

        private void manageApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplicationTypes frm = new ManageApplicationTypes();
            frm.ShowDialog();
        }

        private void mangeTesstTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTestTypes frm = new ManageTestTypes();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDLA frm = new frmLocalDLA(_UserID);
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddAndUpdateLocalDLA frm = new frmAddAndUpdateLocalDLA(-1, _UserID, -1);
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewInternationalLicenseApplication frm = new AddNewInternationalLicenseApplication(_UserID);
            frm.ShowDialog();
        }

        private void internationalLicensesApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListInternationalLicenses frm = new ListInternationalLicenses(_UserID);
            frm.ShowDialog();
        }

        private void renewLocalDrivingLoicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLocalDrivingLoicense frm = new RenewLocalDrivingLoicense(_UserID);
            frm.ShowDialog();
        }

        private void replacementForALostDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplacementforLicense frm = new ReplacementforLicense(_UserID);
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDetainedLicenses frm = new ManageDetainedLicenses(_UserID);
            frm.ShowDialog();
        }

        private void detainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDetainedLicense frm = new AddDetainedLicense(_UserID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenses frm = new ReleaseDetainedLicenses(_UserID, -1);
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenses frm = new ReleaseDetainedLicenses(_UserID, -1);
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDLA frm = new frmLocalDLA(_UserID);
            frm.ShowDialog();
        }

  
    }
}

