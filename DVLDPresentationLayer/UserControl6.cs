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
using static System.Windows.Forms.AxHost;


namespace DVLDPresentationLayer
{
    public partial class UserControl6 : UserControl
    {
        public UserControl6()
        {
            InitializeComponent();
        }

        private void UserControl6_Load(object sender, EventArgs e)
        {

        }


        // تحديد سبب اصدار الرخصة
        private void IssueReasonType(int IssueReason)
        {
            switch (IssueReason)
            {
                case 1:
                    lblIssueReason.Text = "First Time";
                    break;
                case 2:
                    lblIssueReason.Text = "Renew";
                    break;
                case 3:
                    lblIssueReason.Text = "Replacement for Lost";
                    break;
                case 4:
                    lblIssueReason.Text = "Replacement for Damaged";

                    break;

            }
        }


        // اضهار البيانات من خلال رقم الرخصة
        public void ShowData2(int LicenseID)
        {
            clsLicenses Licenses = clsLicenses.Find(LicenseID);

            if (Licenses == null)
            {
                MessageBox.Show("Could not find License ID = " + Licenses.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsApplications Application = clsApplications.Fine(Licenses.ApplicationID);

            clsPeople Person = clsPeople.Find(Application.ApplicantPersonID);


            
            lblClass.Text = clsLicenseClasses.Find(Licenses.LicenseClass).ClassName;
            lblName.Text = Person.GetFullName();
            lblNationalNo.Text = Person.NationalNo;

      

           // clsLocalDLA1 LocalDLA1 = clsLocalDLA1.Find(LocalDLA_ID);



            if (Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }
            


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


           // clsLicenses Licenses = clsLicenses.FindApplicationID(Application.ApplicationID);
           
            lblLicenseID.Text = Licenses.LicenseID.ToString();

            lblIssueDate.Text = Licenses.IssueDate.ToString("yyyy-mm-dd");

            // تحديد سبب اصدار الرخصة
            IssueReasonType(Licenses.IssueReason);

            if (Licenses.Notes == "")
            lblNotes.Text = "No Notes";
            else
            lblNotes.Text = Licenses.Notes;

            if (Licenses.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

            // هل الرخصة محجوزه
            if (clsDetainedLicenses.IsTheDetainIsReleased(LicenseID))
                lblIsDetained.Text = "Yes";
            else
                lblIsDetained.Text = "No";


            lblDateOfBirth.Text = Person.DateOfBirth.ToString("yyyy-mm-dd");
            lblDriverID.Text = Licenses.DriverID.ToString();
            lblExpirationDate.Text = Licenses.ExpirationDate.ToString("yyyy-mm-dd");


        }



        // اضهار البيانات
        public void ShowData(int LocalDLA_ID)
        {

            clsLocalDLA_View _LocalDLA_View = clsLocalDLA_View.Fine(LocalDLA_ID);
            lblClass.Text = _LocalDLA_View.ClassName;
            lblName.Text = _LocalDLA_View.FullName;
            lblNationalNo.Text = _LocalDLA_View.NationalNo;



            clsLocalDLA1 LocalDLA1 = clsLocalDLA1.Find(LocalDLA_ID);

            clsApplications Application = clsApplications.Fine(LocalDLA1.ApplicationID);

            clsPeople Person = clsPeople.Find(Application.ApplicantPersonID);



            if (Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }



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


            clsLicenses Licenses = clsLicenses.FindApplicationID(Application.ApplicationID);

            lblLicenseID.Text = Licenses.LicenseID.ToString();

            lblIssueDate.Text = Licenses.IssueDate.ToString("yyyy-mm-dd");
            // lblIssueReason.Text = Licenses.IssueReason.ToString();

            if (Licenses.Notes == "")
                lblNotes.Text = "No Notes";
            else
                lblNotes.Text = Licenses.Notes;

            if (Licenses.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

            lblDateOfBirth.Text = Person.DateOfBirth.ToString("yyyy-mm-dd");
            lblDriverID.Text = Licenses.DriverID.ToString();
            lblExpirationDate.Text = Licenses.ExpirationDate.ToString("yyyy-mm-dd");


        }



    }
}
