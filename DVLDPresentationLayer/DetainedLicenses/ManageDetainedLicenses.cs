using DVLDBusinessLayer;
using DVLDPresentationLayer.InternationalLicenses;
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

namespace DVLDPresentationLayer.DetainedLicenses
{
    public partial class ManageDetainedLicenses : Form
    {
        DataTable _DataTableDetainedLicenses = new DataTable();
        int _UserID = -1;
        public ManageDetainedLicenses(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }


        private void _RefreshLocalDLAList()
        {
            _DataTableDetainedLicenses = clsDetainedLicenses.GetAllDetainedLicenses();

            dataGridView1.DataSource = _DataTableDetainedLicenses;
            lblRecords.Text = _DataTableDetainedLicenses.Rows.Count.ToString();
            if (_DataTableDetainedLicenses.Rows.Count > 0)
            {
                dataGridView1.Columns[2].Width = 140; // تغيير عرض العمود
                dataGridView1.Columns[5].Width = 140; // تغيير عرض العمود
                dataGridView1.Columns[7].Width = 260; // تغيير عرض العمود
            }
        }
        private void ManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshLocalDLAList();
            cbFilterBy.SelectedIndex = 0;
            txtSearch.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetained_Click(object sender, EventArgs e)
        {
            AddDetainedLicense frm = new AddDetainedLicense(_UserID);
            frm.ShowDialog();
            _RefreshLocalDLAList();

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenses frm = new ReleaseDetainedLicenses(_UserID, -1);
            frm.ShowDialog();
            _RefreshLocalDLAList();

        }

        private void mnShowApplicationData_Click(object sender, EventArgs e)
        {
            int personID = clsPeople.FindNationalNo((string)dataGridView1.CurrentRow.Cells[6].Value);
            PersonDetails frm = new PersonDetails(personID);
            frm.ShowDialog();
        }

        private void mnShowLicense_Click(object sender, EventArgs e)
        {
            InternationalLicenseDriverInfo frm = new InternationalLicenseDriverInfo((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = clsPeople.FindNationalNo((string)dataGridView1.CurrentRow.Cells[6].Value);

            ShowPersonLicenseHistory frm = new ShowPersonLicenseHistory(personID);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            if (!clsDetainedLicenses.IsTheDetainIsReleased(LicenseID))
            {
                mnReleaseDetainedLicenses.Enabled = false;
            }
            else
            {
                mnReleaseDetainedLicenses.Enabled = true;
            }
        }

        private void mnReleaseDetainedLicenses_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenses frm = new ReleaseDetainedLicenses(_UserID, (int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshLocalDLAList();
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



        /*البحث ID*/
        private void SearchByLocalDLAID()
        {
            string pattern = @"^\d+$";

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTableDetainedLicenses;
                return;
            }

            DataRow[] RowPersens;
            if (Regex.IsMatch(txtSearch.Text.ToString(), pattern))
            {
                RowPersens = _DataTableDetainedLicenses.Select($"{cbFilterBy.Text}=" + txtSearch.Text.ToString());
            }
            else
            {
                txtSearch.Text = "";
                RowPersens = _DataTableDetainedLicenses.Select($"{cbFilterBy.Text}=" + "- 1");
            }


            DataTable dataTable = _DataTableDetainedLicenses.Clone();

            if (RowPersens.Length > 0)
            {
                dataTable = RowPersens.CopyToDataTable();
            }

            dataGridView1.DataSource = dataTable;

        }

        private void SearchByName()
        {

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTableDetainedLicenses;
                return;
            }


            DataRow[] RowPerson = _DataTableDetainedLicenses.Select($"{cbFilterBy.Text} like '%{txtSearch.Text.ToString()}%'");

            DataTable dataTable = _DataTableDetainedLicenses.Clone();

            if (RowPerson.Length > 0)
            {
                dataTable = RowPerson.CopyToDataTable();
                dataTable.DefaultView.Sort = cbFilterBy.Text.ToString();
            }


            dataGridView1.DataSource = dataTable;

        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.Text == "DetainID" || cbFilterBy.Text == "LicenseID" || 
                cbFilterBy.Text == "IsReleased" || cbFilterBy.Text == "ReleaseApplicationID")
            {

                SearchByLocalDLAID();
            }
            else
            {
                SearchByName();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "DetainID" || cbFilterBy.Text == "LicenseID" ||
                cbFilterBy.Text == "IsReleased" || cbFilterBy.Text == "ReleaseApplicationID")
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
}
