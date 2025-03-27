using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.DrivingLicenses
{
    public partial class ShowLicenseInfo : Form
    {
        private int _LocalDLA_ID = -1;
        int _LicenseID = -1;
        public ShowLicenseInfo(int LocalDLA_ID,int LicenseID)
        {
            InitializeComponent();
            _LocalDLA_ID = LocalDLA_ID;
            _LicenseID = LicenseID;
        }

        private void ShowLicenseInfo_Load(object sender, EventArgs e)
        {
            if(_LocalDLA_ID != -1)
            userControl61.ShowData(_LocalDLA_ID);// يجيب البيانات حسب رقم الطلي
            else
                userControl61.ShowData2(_LicenseID);// يجيب البيانات حسب رقم الرخصة

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
