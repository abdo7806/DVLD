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

namespace DVLDPresentationLayer.DrivingLicenses
{
    public partial class AddNewLicenses : Form
    {
        private int _LocalDLA_ID = -1;
        int _UserID = 0;
        clsLocalDLA1 _LocalDLA1;
        public AddNewLicenses(int LocalDLA_ID, int UserID)
        {
            InitializeComponent();
            _LocalDLA_ID = LocalDLA_ID;
            _UserID = UserID;
        }

        private void AddNewLicenses_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();

            _LocalDLA1 = clsLocalDLA1.Find(_LocalDLA_ID);

            if (_LocalDLA1 == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDLA1.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            userControl41.ShowData(_LocalDLA_ID);
        }



        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            clsLicenses License = new clsLicenses();

            int ApplicationID = clsLocalDLA1.Find(_LocalDLA_ID).ApplicationID;

            int personID = clsApplications.Fine(ApplicationID).ApplicantPersonID;

            clsDrivers Driver = clsDrivers.Find(personID);

            License.ApplicationID = ApplicationID;
            // License.DriverID = personID;

            License.LicenseClass = clsLocalDLA1.Find(_LocalDLA_ID).LicenseClassID;
            License.ExpirationDate = License.ExpirationDate.AddYears(clsLicenseClasses.Find(License.LicenseClass).DefaultValidityLength);
            License.PaidFees = clsApplications.Fine(ApplicationID).PaidFees;
            License.CreatedByUserID = _UserID;

            if (txtNotes.Text == "")
                License.Notes = "";
            else
                License.Notes = txtNotes.Text;


            if (Driver != null)
            {
               // MessageBox.Show("A");

                License.DriverID = Driver.DriverID;


            }
            else
            {
              //  MessageBox.Show("B");

                Driver = new clsDrivers();
                Driver.PersonID = personID;
                Driver.CreatedByUserID = _UserID;

                Driver.AddNewDriver();// اضافة سائق

                License.DriverID = Driver.DriverID;


            }

            // اضافة رخصة
            if (License.AddNewLicense())
            {
                // اقفال الطلب
                clsApplications.CompletedApplication(ApplicationID);

                MessageBox.Show("License Issued Successfully weth License ID = " + License.LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();
            }
            else
                MessageBox.Show("Error: License Issued Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
