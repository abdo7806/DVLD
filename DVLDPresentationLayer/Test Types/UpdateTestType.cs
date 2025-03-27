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

namespace DVLDPresentationLayer.Test_Types
{
    public partial class UpdateTestType : Form
    {
       private int _TestTypeID = -1;
       private clsTestTypes _TestType = new clsTestTypes();
        public UpdateTestType(int TestTypeID)
        {
            InitializeComponent();
             _TestTypeID = TestTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestTypes.Find(_TestTypeID);
            if (_TestType != null)
            {
                lblTestTypeID.Text = _TestType.TestTypeID.ToString();
                txtTestTypeTitle.Text = _TestType.TestTypeTitle.ToString();
                txtTestTypeDescription.Text = _TestType.TestTypeDescription.ToString();
                txtTestTypeFees.Text = _TestType.TestTypeFees.ToString();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {



            _TestType.TestTypeTitle = txtTestTypeTitle.Text;
            _TestType.TestTypeDescription = txtTestTypeDescription.Text;
            _TestType.TestTypeFees = float.Parse(txtTestTypeFees.Text);

            if (_TestType.UpdateTestType())
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
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
        private void txtTestTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtTestTypeTitle);

        }

        private void txtTestTypeDescription_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtTestTypeDescription);

        }

        private void txtTestTypeFees_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtTestTypeFees);
        }
    }
}
