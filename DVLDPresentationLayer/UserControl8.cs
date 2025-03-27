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

namespace DVLDPresentationLayer
{
    public partial class UserControl8 : UserControl
    {
        int _LocalDLA_ID = -1;
        int _LicenseID = -1;

        clsLicenses _License;



        // من اجل اضافة قيم او ارجاع قيم للفورم
        public event Action<int> OnClculationComplete1;
        protected virtual void Close(int c)
        {
            Action<int> handler = OnClculationComplete1;
            if (handler != null)
            {
                handler(c);
            }
        }

        public UserControl8()
        {
            InitializeComponent();
        }

        private void btnSearsh_Click(object sender, EventArgs e)
        {
            
            if (txtSearch.Text == "")
            {
                //أدخل رقم الترخيص

                MessageBox.Show("Enter the license number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LicenseID = Convert.ToInt32(txtSearch.Text);


            _License = clsLicenses.Find(_LicenseID);


            //لا يوجد ترخيص بهذا الرقم

            if (_License == null)
            {
                MessageBox.Show("There is no License with this Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            userControl61.ShowData2(_LicenseID);


            if (OnClculationComplete1 != null)
            {
                Close(1);
            }


        }


       public void ShowData(int LicenseID)
        {

            _LicenseID = LicenseID;


            _License = clsLicenses.Find(_LicenseID);


            //لا يوجد ترخيص بهذا الرقم

            if (_License == null)
            {
                MessageBox.Show("There is no License with this Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            userControl61.ShowData2(_LicenseID);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // تحقق مما إذا كانت المدخلة ليست رقمًا
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // إذا كانت المدخلة ليست رقمًا، قم بإلغاء الحدث
                e.Handled = true;
            }
            // من اجل اول ما اضغط ينتر يبحث
            if (e.KeyChar == (char)13)
            {

                btnSearsh.PerformClick();
            }
        }

        // ارجاع رقم الرخصة
        public int LicenseID
        {
            get { return _License.LicenseID; }
        }

        public string TextSearch
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }

        }

        //  اخفأ مربع البحث عن الرخصة
        public void GroupBoxEnabledFalse()
        {
            groupBox2.Enabled = false;

        }





        private void UserControl8_Load(object sender, EventArgs e)
        {

        }
    }
}
