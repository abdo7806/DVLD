using DVLDBusinessLayer;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentationLayer.Users
{
    public partial class Change_Password : Form
    {
        int _UserID = -1;
        int _PersonID = -1;
        ClsUsers _User = new ClsUsers();

        public Change_Password(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            UserControl3._UserID = _UserID;

        }


        // اعادة تحميل
        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfrimPassword.Text = "";
            txtCurrentPassword.Focus();
        }
        private void Change_Password_Load(object sender, EventArgs e)
        {
            UserControl3._UserID = _UserID;
            _ResetDefualtValues();
            _User = ClsUsers.Find(_UserID);

        }


        // رسالة التحذير من اجل ادخال كل البيانات
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {

                e.Cancel = true;
                txtNewPassword.Focus();

                errorProvider1.SetError(txtNewPassword, "The password is incorrect");

            }
            else
            {
                e.Cancel = false;


                errorProvider1.SetError(txtNewPassword, "");
            }
        }

        private void txtConfrimPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfrimPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                txtConfrimPassword.Focus();
                errorProvider1.SetError(txtConfrimPassword, "Password mismatch");
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfrimPassword, "");
            }
        }

        // دالة تشفير او تجزيء كلمة السر
        static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            // ارجاع كلمة السر مشفرة

            string password = ComputeHash(txtCurrentPassword.Text.Trim());


            if (string.IsNullOrEmpty(txtCurrentPassword.Text) || password != _User.Password)
            {

                e.Cancel = true;
                txtCurrentPassword.Focus();

                errorProvider1.SetError(txtCurrentPassword, "Enter data");

            }
            else
            {
                e.Cancel = false;

                errorProvider1.SetError(txtCurrentPassword, "");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCurrentPassword.Text == "" || txtNewPassword.Text == "" || txtConfrimPassword.Text == "")
            {
                MessageBox.Show("Data is incomplete", "warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(_User == null)
            {
                return;
            }

            _User.Password = txtNewPassword.Text;


            if (_User.Save())
            {
                MessageBox.Show("Password Change Successfully.", "Save");
                _ResetDefualtValues();
            }
            else
                MessageBox.Show("Error: Password Is not Change Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            UserControl3._UserID = -1;
            this.Close();
        }

     
    }
}
