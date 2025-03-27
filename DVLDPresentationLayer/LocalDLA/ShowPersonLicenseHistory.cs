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

namespace DVLDPresentationLayer.LocalDLA
{
    public partial class ShowPersonLicenseHistory : Form
    {
        int _PersonID = -1;
        private int _LocalDLA_ID = -1;

        string _NationalNo = "";
         
        public ShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }

        private void ShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {


            if (_PersonID == -1)
            {
                return;
            }


            UserControl2._PersonID = _PersonID;     

        //عرض بيانات الشخص
            userControl21.ShowDataPerson();

            userControl21.StopSearchingForSomeone();// اخفأ الفيلتير

            clsDrivers Driver = clsDrivers.Find(_PersonID);

            if(Driver == null)
            {
                return;
            }

            int DriverID = Driver.DriverID;

            userControl71._RefreshLicensesList(DriverID);// الرخصة المحلية
            userControl71._RefreshInternationalLicensesList(DriverID); // الرخصة الدولية

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
