using DVLDBusinessLayer;
using DVLDPresentationLayer.LocalDLA;
using DVLDPresentationLayer.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.InternationalLicenses
{
    public partial class ListInternationalLicenses : Form
    {
        DataTable _DataTableLicenses = new DataTable();

        int _UserID = -1;
        public ListInternationalLicenses(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }


        private void _RefreshLocalDLAList()
        {
            _DataTableLicenses = clsInternationalLicenses.GetAllInternationalLicense();

            dataGridView1.DataSource = _DataTableLicenses;
            lblRecords.Text = _DataTableLicenses.Rows.Count.ToString();
            if(_DataTableLicenses.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Width = 150; // تغيير عرض العمود
                dataGridView1.Columns[1].Width = 150; // تغيير عرض العمود
                dataGridView1.Columns[2].Width = 150; // تغيير عرض العمود
                dataGridView1.Columns[3].Width = 150; // تغيير عرض العمود
                dataGridView1.Columns[4].Width = 150; // تغيير عرض العمود
                dataGridView1.Columns[5].Width = 150; // تغيير عرض العمود


            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewInternationalLicenseApplication frm = new AddNewInternationalLicenseApplication(_UserID);
            frm.ShowDialog();
        }

        private void ListInternationalLicenses_Load(object sender, EventArgs e)
        {
            _RefreshLocalDLAList();
            cbFilterBy.SelectedIndex = 0;
            txtSearch.Visible = false;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = clsApplications.Fine((int)dataGridView1.CurrentRow.Cells[1].Value).ApplicantPersonID;
           
            ShowPersonLicenseHistory frm = new ShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        private void mnShowApplicationData_Click(object sender, EventArgs e)
        {
            int personID = clsApplications.Fine((int)dataGridView1.CurrentRow.Cells[1].Value).ApplicantPersonID;
            PersonDetails frm = new PersonDetails(personID);
            frm.ShowDialog();

        }

        private void mnShowLicense_Click(object sender, EventArgs e)
        {
            InternationalLicenseDriverInfo frm = new InternationalLicenseDriverInfo((int)dataGridView1.CurrentRow.Cells[3].Value);
            frm.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtSearch.Visible = false;
                return;
            }

            txtSearch.Visible = true;
        }


        private void SearchByLocalDLAID()// البحث عن كريف اي رقم
        {
            string pattern = @"^\d+$";

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTableLicenses;
                return;
            }

            DataRow[] RowPersens;
            if (Regex.IsMatch(txtSearch.Text.ToString(), pattern))
            {
                RowPersens = _DataTableLicenses.Select($"{cbFilterBy.Text}=" + txtSearch.Text.ToString());
            }
            else
            {
                txtSearch.Text = "";
                RowPersens = _DataTableLicenses.Select($"{cbFilterBy.Text}=" + "- 1");
            }


            DataTable dataTable = _DataTableLicenses.Clone();

            if (RowPersens.Length > 0)
            {
                dataTable = RowPersens.CopyToDataTable();
            }

            dataGridView1.DataSource = dataTable;

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchByLocalDLAID();// البحث عن كريف اي رقم
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // تحقق مما إذا كانت المدخلة ليست رقمًا
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // إذا كانت المدخلة ليست رقمًا، قم بإلغاء الحدث
                e.Handled = true;
            }
        }
    }
}
