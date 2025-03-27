using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace DVLDPresentationLayer
{
    public partial class UserControl9 : UserControl
    {
        public UserControl9()
        {
            InitializeComponent();
        }

        private void UserControl9_Load(object sender, EventArgs e)
        {

        }




        // اضهار البيانات الرخصة الدولية
        public void ShowDataIntLicense(int LicenseID)
        {
            clsInternationalLicenses IntLicense = clsInternationalLicenses.Find(LicenseID);
            if (IntLicense == null)
            {
                return;
            }
            clsApplications Application = clsApplications.Fine(IntLicense.ApplicationID);
            clsPeople Person = clsPeople.Find(Application.ApplicantPersonID);

            lblName.Text = Person.GetFullName();
            lblI_Int_LicenseID.Text = IntLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text = IntLicense.LicenseID.ToString();

            lblNationalNo.Text = Person.NationalNo;



            if (Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }

            lblIssueDate.Text = IntLicense.IssueDate.ToString("yyyy-mm-dd");
            lblApplicationID.Text = IntLicense.ApplicationID.ToString();
            lblDateOfBirth.Text = Person.DateOfBirth.ToString("yyyy-mm-dd");
            lblDriverID.Text = IntLicense.DriverID.ToString();
            lblExpirationDate.Text = IntLicense.ExpirationDate.ToString("yyyy-mm-dd");

            if (Person.ImagePath != "" && File.Exists(Person.ImagePath))
            {
                pictureBox1.Load(Person.ImagePath);
            }

            else
            {
                if (Person.Gendor == 0)
                    pictureBox1.Image = Resources.Male_512;
                else
                    pictureBox1.Image = Resources.Female_512;

            }


        }


    }
}
