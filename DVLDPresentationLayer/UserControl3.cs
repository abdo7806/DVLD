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
    public partial class UserControl3 : UserControl
    {
       // public static int _PersonID = -1;
        public static int _UserID = -1;

        clsPeople _Person;
        ClsUsers _User;
        public UserControl3()
        {
            InitializeComponent();
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {

            _User = ClsUsers.Find(_UserID);

            if (_User == null)
            {
              //  MessageBox.Show("This form will be closed because No User with ID = " + _UserID);
                return;
            }




            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";



            UserControl1._PersonID = _User.PersonID;
            userControl11._LoadData();

        }


        // اعاده التحميل
        private void _ResetPersonInfo()
        {

             userControl11.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

    }
}
