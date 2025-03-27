using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.InternationalLicenses
{
    public partial class InternationalLicenseDriverInfo : Form
    {
        int _LicenseID = -1;
        public InternationalLicenseDriverInfo(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InternationalLicenseDriverInfo_Load(object sender, EventArgs e)
        {
            if(_LicenseID == -1)
            {
                return;
            }

            // اضهار البيانات الرخصة الدولية

            userControl91.ShowDataIntLicense(_LicenseID);
        }
    }
}
