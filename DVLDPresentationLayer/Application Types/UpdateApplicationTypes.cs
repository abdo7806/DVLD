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

namespace DVLDPresentationLayer.Application_Types
{
    public partial class UpdateApplicationTypes : Form
    {
        int _ApplicationTypeID = -1;
        clsApplicationTypes _ApplicationType = new clsApplicationTypes();
        public UpdateApplicationTypes(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }


        private void UpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationTypes.Find(_ApplicationTypeID);
            if(_ApplicationType == null)
            {
                MessageBox.Show("This form will be closed because No User with ID = " + _ApplicationTypeID);

                return;
            }
            lblApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtApplicationTypeTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtApplicationFees.Text = _ApplicationType.ApplicationFees.ToString();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtApplicationTypeTitle.Text == "" || txtApplicationFees.Text == "")
            {
                MessageBox.Show("Data is incomplete", "warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = txtApplicationTypeTitle.Text;
            _ApplicationType.ApplicationFees = float.Parse(txtApplicationFees.Text);

            if (_ApplicationType.UpdateApplicationType())
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
        private void txtApplicationTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtApplicationTypeTitle);
        }

        private void txtApplicationFees_Validating(object sender, CancelEventArgs e)
        {
            ErrorNationalNo(sender, e, txtApplicationFees);
        }
    }
}
