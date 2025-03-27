using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.Users
{
    public partial class ShowUserDetails : Form
    {
        int _UserID = -1;
        public ShowUserDetails(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            UserControl3._UserID = _UserID;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowUserDetails_Load(object sender, EventArgs e)
        {
            UserControl3._UserID = _UserID;

        }
    }
}
