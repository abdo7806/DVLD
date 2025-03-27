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
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DVLDPresentationLayer.Users
{
    public partial class AddAndUpdateUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        DataTable _DataTablePeoples = new DataTable();

        clsPeople _Person;
        int _PersonID = -1;
        int _UserID = -1;


        ClsUsers _User = new ClsUsers();

        public AddAndUpdateUser(int UserID, int PersonID)
        {
            InitializeComponent();

            _UserID = UserID;
            _PersonID = PersonID;

            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
              //  UserControl2._PersonID = PersonID;
            }

        }


  


        private void _LoadData()
        {
            // عرض بيانات الشخص 


            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                _Person = new clsPeople();
                return;
            }

            _User = ClsUsers.Find(_UserID);

           if (_User == null)
            {
                MessageBox.Show("This form will be closed because No User with ID = " + _PersonID);
                this.Close();

                return;
            }

            UserControl2._PersonID = _PersonID;
            lblMode.Text = "Update New User = " + _User.UserID.ToString();

            userControl21.ShowDataPerson();

            userControl21.StopSearchingForSomeone();

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfrimPassword.Text = _User.Password; 
            chkIsActive.Checked = _User.IsActive;

            txtPassword.Visible = false;
            txtConfrimPassword.Visible = false;


        }

        private void AddAndUpdateUser_Load(object sender, EventArgs e)
        {
            _LoadData();
        }


        //هل الشخص موجود في النظام
        private bool DoesPersonExist()
        {
            _PersonID = Convert.ToInt32(userControl21.lblPersonID);


            //هل الشخص موجود في النظام
            if ((ClsUsers.DoesPersonExist(_PersonID) || _PersonID == -1) && _Mode == enMode.AddNew)
            {
                MessageBox.Show("Selected person already has a Uesr, Choose another one.", "Selected another person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
        private void btnNaxt_Click(object sender, EventArgs e)
        {
            //هل الشخص موجود في النظام
            if (_Mode == enMode.AddNew)
            {
                if (DoesPersonExist())
                {
                    return;
                }
            }

            tabControl1.SelectedIndex = 1;

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
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtUserName);
        }

        private void textPassword1_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtPassword);
        }

        private void txtConfrimPassword_Validating(object sender, CancelEventArgs e)
        {
          

            if (txtConfrimPassword.Text != txtPassword.Text)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            UserControl2._PersonID = -1;
            //_PersonID = -1;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //هل الشخص موجود
            if (DoesPersonExist())
            {
                return;
            }


            if (txtUserName.Text == "" || txtPassword.Text == "" || txtConfrimPassword.Text == "")
            {
                MessageBox.Show("Data is incomplete", "warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _User.PersonID = _PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;


            if (_User.Save())
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            _Mode = enMode.Update;
            lblMode.Text = "Update User ID = " + _User.UserID;
            lblUserID.Text = _User.UserID.ToString();
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //هل الشخص موجود في النظام
            if (_Mode == enMode.AddNew && tabControl1.SelectedIndex == 1)
            {
                if (DoesPersonExist())
                {
                    tabControl1.SelectedIndex = 0;
                }
            }


        }
    }
}
