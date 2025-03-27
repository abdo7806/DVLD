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
using System.IO;

using DVLDPresentationLayer.Properties;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentationLayer
{
    public partial class AddAndUpdetPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersenID);

        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _PersonID;


        DataTable _DataTablePeoples = new DataTable();

        clsPeople _Person;

        string _Amtdae = "";// امتداد الصورة القدبمة

        private string selectedFilePath = "";

        public AddAndUpdetPerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;

            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        // يعبي لي البلدان
        private void _FillCountriesInComoboBox()
        {
            _DataTablePeoples = clsCountry.GetAllCountries();


            foreach (DataRow row in _DataTablePeoples.Rows)
            {

                cbCountry.Items.Add(row["CountryName"]);

            }

            cbCountry.SelectedIndex = 190;// من اجل تكون الافترضية اليمن

        }

        private void _LoadData()
        {

            // يعبي لي البلدان
            _FillCountriesInComoboBox();

            // يجيب لي اخر تريخ قبل 18 سنة
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);// اكبر عمر 


            if (_Mode == enMode.AddNew)
            {

                lblMode.Text = "Add New Person";
                _Person = new clsPeople();
                return;
            }

            _Person = clsPeople.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                this.Close();

                return;
            }


            lblMode.Text = "Edit Contact ID = " + _PersonID;
            lblPeopleID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cbCountry.SelectedIndex = _Person.NationalityCountryID - 1;

            if (_Person.Gendor == 0)
            {
                pictureBox1.Image = Resources.Male_512;
                rdMale.Checked = true;
            }
            else
            {
                pictureBox1.Image = Resources.Female_512;

                rdFemale.Checked = true;
            }

            if (_Person.ImagePath != "")
            {
                if (File.Exists(_Person.ImagePath))
                {
                    pictureBox1.Load(_Person.ImagePath);
                    llRemoveImage.Visible = true;
                    _Amtdae = _Person.ImagePath;


                    if (pictureBox1.Image != Resources.Male_512 ||
                        pictureBox1.Image != Resources.Female_512)
                    {
                        selectedFilePath = openFileDialog1.FileName;
                    }

                }
            }


        }
        private void AddAndUpdetPerson_Load(object sender, EventArgs e)
        {

            _LoadData();
        }

        string dir = "";
        private void llOpenFileDialog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                selectedFilePath = openFileDialog1.FileName;

                // MessageBox.Show("Selected Image is:" + selectedFilePath);


                pictureBox1.Load(selectedFilePath);



                string dir = $"C:\\DVLD People Image\\{txtFirstName.Text.ToString()} {txtLastName.Text.ToString()}.jpg";

                if (_Person.ImagePath == "")
                {
                    _Person.ImagePath = dir;
                }
                else
                {
                    //  _Amtdae = dir;
                    if (File.Exists(_Person.ImagePath))
                    {
                        File.Delete(_Person.ImagePath);
                    }

                    _Person.ImagePath = dir;

                }

                llRemoveImage.Visible = true;

            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Image = null;
            if (rdMale.Checked)
            {
                pictureBox1.Image = Resources.Male_512;
            }

            else
                pictureBox1.Image = Resources.Female_512;



            llRemoveImage.Visible = false;





        }

        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
   

            if (_Person.ImagePath == "")
                pictureBox1.Image = Resources.Male_512;
        }

        private void rdFemale_CheckedChanged(object sender, EventArgs e)
        {
     

            if (_Person.ImagePath == "")
                pictureBox1.Image = Resources.Female_512;

        }


        // رسالة التحذير من اجل ادخال كل البيانات
        private void ErrorNationalNo(object sender, CancelEventArgs e, System.Windows.Forms.TextBox txt)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {

                e.Cancel = true;
                txt.Focus();

                errorProvider1.SetError(txt, "Enter data");



            }
            else
            {
                e.Cancel = false;


                errorProvider1.SetError(txt, "");


            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtFirstName);
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtSecondName);

        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtLastName);

        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtPhone);
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtAddress);
        }

        // للتاكد ان الرقم الوطني ليسا موجود داخل قاعدة البيانات
        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (clsPeople.TestNationalNo(txtNationalNo.Text.ToString()) || string.IsNullOrEmpty(txtNationalNo.Text))
            {

                e.Cancel = true;
                txtNationalNo.Focus();

                errorProvider1.SetError(txtNationalNo, "required");



            }
            else
            {
                e.Cancel = false;


                errorProvider1.SetError(txtNationalNo, "");


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dir = $"C:\\DVLD People Image\\{txtFirstName.Text.ToString()} {txtLastName.Text.ToString()}.jpg";

            _Person.NationalNo = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Address = txtAddress.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;

            _Person.NationalityCountryID = clsCountry.Find(cbCountry.Text.ToString());




            _Person.ImagePath = dir;

            if (_Person.ImagePath != "")
            {

                if (File.Exists(_Amtdae) && _Person.ImagePath != _Amtdae)
                {

                    //  pictureBox1.Dispose();
                    //pictureBox1.Image = null;



                    File.Copy(_Amtdae, _Person.ImagePath.ToString(), true);
                    pictureBox1.Load(_Person.ImagePath);
                    File.Delete(_Amtdae);


                }
                else if (!File.Exists(_Person.ImagePath) && selectedFilePath != "")
                {

                    File.Copy(selectedFilePath, _Person.ImagePath.ToString(), true);
                }
            }




            _Amtdae = _Person.ImagePath.ToString();


            if (rdMale.Checked)
            {
                _Person.Gendor = 0;
            }
            else
            {
                _Person.Gendor = 1;
            }


            if (_Person.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
            lblMode.Text = "Update Person ID = " + _Person.PeopleID;
            lblPeopleID.Text = _Person.PeopleID.ToString();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pictureBox1.Dispose();
            pictureBox1.Image = null;
            DataBack?.Invoke(this, _Person.PeopleID);
            this.Close();

        }

        //التئكد من صجة الايمال
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if(txtEmail.Text == "")
            {
                return;
            }
            
            if (!Regex.IsMatch(txtEmail.Text.ToString(), pattern))
            {
                // ErrorNationalNo(sender, e, txtEmail);

                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "invailed Email Format");
            }

            else
            {
                e.Cancel = false;


                errorProvider1.SetError(txtEmail, "");


            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
