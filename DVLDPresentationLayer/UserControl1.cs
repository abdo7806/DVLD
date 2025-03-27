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
using System.Runtime.CompilerServices;

namespace DVLDPresentationLayer
{
    public partial class UserControl1 : UserControl
    {
        public static int _PersonID = -1;

  


        clsPeople _Person;
        public UserControl1()
        {
            InitializeComponent();
        }

 

        public static void setPersonID(int PersonID)
        {
            _PersonID = PersonID;
         
        }

        public void _LoadData()
        {
            _Person = clsPeople.Find(_PersonID);

            if( _Person == null )
            {
                return;
            }

            lblPeopleID.Text = _PersonID.ToString();
            lblName.Text = _Person.GetFullName();

            lblNationalNo.Text = _Person.NationalNo;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd-MM-yyyy");
            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            if (_Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID);



            if (_Person.ImagePath != "" && File.Exists(_Person.ImagePath))
            {
                pictureBox1.Load(_Person.ImagePath);
            }

            else
            {
                if (_Person.Gendor == 0)
                    pictureBox1.Image = Resources.Male_512;
                else
                    pictureBox1.Image = Resources.Female_512;

            }
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

   

        private void llEsitPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Dispose();
            pictureBox1.Image = null;
            AddAndUpdetPerson frm = new AddAndUpdetPerson(_Person.PeopleID);
            frm.ShowDialog();
            _LoadData();
            this.Refresh();
        }


        // لو البيانات مش موجوده يعمل اعاده تحميل
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPeopleID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblName.Text = "[????]";
            pictureBox1.Image = Resources.Male_512;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";

        }

    }
}
