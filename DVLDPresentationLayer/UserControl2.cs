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
    public partial class UserControl2 : UserControl
    {
                public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public static int _PersonID = -1;



        DataTable _DataTablePeoples = new DataTable();

        clsPeople _Person;

        ClsUsers _Users;
        public UserControl2()
        {
            InitializeComponent();
        }


    

        //عرض بيانات الشخص
        public void ShowDataPerson()
        {
            _Person = clsPeople.Find(_PersonID);
            if (_Person == null)
            {
             //   MessageBox.Show("This form will be closed because No Person with ID = "+ _PersonID );
                this.Refresh();
                return;
            }



            lblPeopleID.Text = _PersonID.ToString();
              lblName.Text = $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";
           
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


        //  إعادة تعيين القيم الافتراضية
        private void ResetDefaultValues()
        {
            lblPeopleID.Text = "???";
            lblName.Text = "???";
            lblNationalNo.Text = "???";

            lblDateOfBirth.Text = "???";
            lblAddress.Text = "???";
            lblPhone.Text = "???";
            lblEmail.Text = "???";

            lblGendor.Text = "???";
            pictureBox1.Image = Resources.Male_512;

        }



        private void button1_Click(object sender, EventArgs e)
        {
            

            if(cbFilterBy.Text == "NationalNo")
            {
        // البحث عن شخص من خلال رقمة الوطني
                _PersonID = clsPeople.FindNationalNo(txtSearch.Text.ToString());

                if (_PersonID == -1)
                {
                    MessageBox.Show("This person does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //  إعادة تعيين القيم الافتراضية
                    ResetDefaultValues();
                }
            }
            else
            {
                _PersonID = Convert.ToInt32(txtSearch.Text);

                if(clsPeople.Find(_PersonID) == null)
                {
                    _PersonID = -1;
                    MessageBox.Show("This person does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //  إعادة تعيين القيم الافتراضية
                    ResetDefaultValues();

                }
            }



            ShowDataPerson();
        }

        // اخفأ الفيلتير
        public void StopSearchingForSomeone()
        {
            if (lblPeopleID.Text != "???")
            {
                groupBox2.Enabled = false;
            }
        }
        private void UserControl2_Load(object sender, EventArgs e)
        {
            ShowDataPerson();

            cbFilterBy.SelectedIndex = 1;

        }

        private void llEsitPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Dispose();
            pictureBox1.Image = null;
            AddAndUpdetPerson frm = new AddAndUpdetPerson(_Person.PeopleID);
            frm.ShowDialog();
            ShowDataPerson();
            this.Refresh();
        }

 
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                btnSearsh.PerformClick();
            }

            if (cbFilterBy.Text == "PersonID")
            {
                // تحقق مما إذا كانت المدخلة ليست رقمًا
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {

                    // إذا كانت المدخلة ليست رقمًا، قم بإلغاء الحدث
                    e.Handled = true;
                }
            }
        }

        public string lblPersonID
        {
            get 
            { 
                if(lblPeopleID.Text != "???")
                    return lblPeopleID.Text; 
                else 
                    return "-1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddAndUpdetPerson frm = new AddAndUpdetPerson(-1);
            frm.DataBack += Form2_DataBack;
            frm.ShowDialog();
        }
        private void Form2_DataBack(object sender, int PersonID)
        {
            _PersonID = PersonID;

            cbFilterBy.SelectedIndex = 0;
            txtSearch.Text = _PersonID.ToString();

            ShowDataPerson();
        }

    }
}
