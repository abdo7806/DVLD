using DVLDBusinessLayer;
using DVLDPresentationLayer.Users;
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

namespace DVLDPresentationLayer
{
    public partial class ManageUsers : Form
    {
        DataTable _DataTableUsers = new DataTable();

        public ManageUsers()
        {
            InitializeComponent();
        }
        private void _RefreshUsersList()
        {
            _DataTableUsers = ClsUsers.GetAllUsers();

            dataGridView1.DataSource = _DataTableUsers;
            lblRecords.Text = _DataTableUsers.Rows.Count.ToString();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[2].Width = 260; // تغيير عرض العمود

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            cbFilterBy.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAndUpdateUser frm = new AddAndUpdateUser(-1, -1);
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "IsActive")
            {
                txtSearch.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else

            {

                txtSearch.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

            

                txtSearch.Text = "";
                txtSearch.Focus();
            }
        
        }



        /*البحث ID*/
        private void SearchByUserID()
        {
            string pattern = @"^\d+$";

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTableUsers;
                return;
            }

            DataRow[] RowPersens;
            if (Regex.IsMatch(txtSearch.Text.ToString(), pattern))
            {
                RowPersens = _DataTableUsers.Select($"{cbFilterBy.Text}=" + txtSearch.Text.ToString());
            }
            else
            {
                txtSearch.Text = "";
                RowPersens = _DataTableUsers.Select($"{cbFilterBy.Text}=" + "- 1");
            }


            DataTable dataTable = _DataTableUsers.Clone();

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
                dataGridView1.DataSource = _DataTableUsers;
                return;
            }


            DataRow[] RowPerson = _DataTableUsers.Select($"{cbFilterBy.Text} like '%{txtSearch.Text.ToString()}%'");

            DataTable dataTable = _DataTableUsers.Clone();

            if (RowPerson.Length > 0)
            {
                dataTable = RowPerson.CopyToDataTable();
                dataTable.DefaultView.Sort = cbFilterBy.Text.ToString();
            }


            dataGridView1.DataSource = dataTable;

        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.Text == "UserID" || cbFilterBy.Text == "PersonID")
            {
                SearchByUserID();
            }
            else
            {
                SearchByName();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "UserID" || cbFilterBy.Text == "PersonID")
            {
                // تحقق مما إذا كانت المدخلة ليست رقمًا
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كانت المدخلة ليست رقمًا، قم بإلغاء الحدث
                    e.Handled = true;
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndUpdateUser frm = new AddAndUpdateUser((int)dataGridView1.CurrentRow.Cells[0].Value, (int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshUsersList();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_Password frm = new Change_Password((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserDetails frm = new ShowUserDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int UserID = ((int)dataGridView1.CurrentRow.Cells[0].Value);

            if (MessageBox.Show("Are you sure you want to delete User ID = [" + UserID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                    //Perform Delele and refresh
                    if (ClsUsers.DeleteUsers(UserID))
                    {

                        MessageBox.Show("User Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       _RefreshUsersList();
                    }
                    else
                        MessageBox.Show("User is not deleted.", "not deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);

                
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _DataTableUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _DataTableUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecords.Text = _DataTableUsers.Rows.Count.ToString();
        }
    }
}
