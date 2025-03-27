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

namespace DVLDPresentationLayer.Drivers
{
    public partial class ListDriver : Form
    {

        DataTable _DataTableDriver = new DataTable();

        public ListDriver()
        {
            InitializeComponent();
        }


        private void _RefreshDriverList()
        {
            _DataTableDriver = clsDrivers.GetAllDriver();

            dataGridView1.DataSource = _DataTableDriver;
            lblRecords.Text = _DataTableDriver.Rows.Count.ToString();
            if (_DataTableDriver.Rows.Count > 0)
            {
                dataGridView1.Columns[3].Width = 300; // تغيير عرض العمود
                dataGridView1.Columns[4].Width = 200; // تغيير عرض العمود
            }
        }

        private void ListDriver_Load(object sender, EventArgs e)
        {
            _RefreshDriverList();
            cbFilterBy.SelectedIndex = 0;
            txtSearch.Visible = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void SearchByID()
        {
            string pattern = @"^\d+$";

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTableDriver;
                return;
            }

            DataRow[] RowPersens;
            if (Regex.IsMatch(txtSearch.Text.ToString(), pattern))
            {
                RowPersens = _DataTableDriver.Select($"{cbFilterBy.Text}=" + txtSearch.Text.ToString());
            }
            else
            {
                txtSearch.Text = "";
                RowPersens = _DataTableDriver.Select($"{cbFilterBy.Text}=" + "- 1");
            }


            DataTable dataTable = _DataTableDriver.Clone();

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
                dataGridView1.DataSource = _DataTableDriver;
                return;
            }


            DataRow[] RowPerson = _DataTableDriver.Select($"{cbFilterBy.Text} like '%{txtSearch.Text.ToString()}%'");

            DataTable dataTable = _DataTableDriver.Clone();

            if (RowPerson.Length > 0)
            {
                dataTable = RowPerson.CopyToDataTable();
                dataTable.DefaultView.Sort = cbFilterBy.Text.ToString();
            }


            dataGridView1.DataSource = dataTable;

        }


        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtSearch.Visible = false;
                return;
            }
            if (cbFilterBy.Text == "DriverID" || cbFilterBy.Text == "PersonID")
            {

                SearchByID();
            }
            else
            {
                SearchByName();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "DriverID" || cbFilterBy.Text == "PersonID")
            {
                // تحقق مما إذا كانت المدخلة ليست رقمًا
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كانت المدخلة ليست رقمًا، قم بإلغاء الحدث
                    e.Handled = true;
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails frm = new PersonDetails((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshDriverList();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowPersonLicenseHistory frm = new ShowPersonLicenseHistory((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
    }
}
