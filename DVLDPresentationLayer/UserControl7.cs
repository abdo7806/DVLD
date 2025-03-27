using DVLDBusinessLayer;
using DVLDPresentationLayer.DrivingLicenses;
using DVLDPresentationLayer.InternationalLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDPresentationLayer
{
    public partial class UserControl7 : UserControl
    {
        public UserControl7()
        {
            InitializeComponent();
        }

        DataTable _DataTableLicenses = new DataTable();

        // الرخص المحلية
        public void _RefreshLicensesList(int DriverID)
        {


            _DataTableLicenses = clsLicenses.GetAllLicenseByID(DriverID);

            dataGridView1.DataSource = _DataTableLicenses;
            lblRecords.Text = _DataTableLicenses.Rows.Count.ToString();
           if (_DataTableLicenses.Rows.Count > 0)
            {
                dataGridView1.Columns[2].Width = 240; // تغيير عرض العمود
                dataGridView1.Columns[3].Width = 150; // تغيير عرض العمود
                dataGridView1.Columns[4].Width = 150; // تغيير عرض العمود
            }
        }


        //  الرخص الدولية
        public void _RefreshInternationalLicensesList(int DriverID)
        {


            _DataTableLicenses = clsInternationalLicenses.GetAllInternationalLicenseByID(DriverID);

            dataGridView2.DataSource = _DataTableLicenses;
            lblRecords2.Text = _DataTableLicenses.Rows.Count.ToString();
            if(_DataTableLicenses.Rows.Count > 0)
            {
                dataGridView2.Columns[3].Width = 150; // تغيير عرض العمود
                dataGridView2.Columns[4].Width = 150; // تغيير عرض العمود
            }

        }

      

        private void mnShowLicense_Click(object sender, EventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo(-1,(int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
