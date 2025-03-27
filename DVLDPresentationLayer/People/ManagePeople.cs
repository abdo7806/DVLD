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
using System.IO;
using System.Text.RegularExpressions;
using DVLDPresentationLayer.People;

namespace DVLDPresentationLayer
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
        }

        DataTable _DataTablePeoples = new DataTable();
        private void _RefreshPeoplesList()
        {
            _DataTablePeoples = clsPeople.GetAllPeople();

            dataGridView1.DataSource = _DataTablePeoples;
            lblRecords.Text = _DataTablePeoples.Rows.Count.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshPeoplesList();
            cbFilterBy.SelectedIndex = 0;
            txtSearch.Visible = false;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddAndUpdetPerson frm = new AddAndUpdetPerson(-1);
            frm.ShowDialog();
            _RefreshPeoplesList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndUpdetPerson frm = new AddAndUpdetPerson(-1);
            frm.ShowDialog();
            _RefreshPeoplesList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndUpdetPerson frm = new AddAndUpdetPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplesList();
        }


        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails frm = new PersonDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplesList();

        }
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int PeopleID = ((int)dataGridView1.CurrentRow.Cells[0].Value);

            if (MessageBox.Show("Are you sure you want to delete Person [" + PeopleID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // نفحص لو كان مرطبط بابيانات في النظام
                if (clsPeople.IsThisPersonConnectedToTheSystem(PeopleID))
                {
                    MessageBox.Show("Person is Not Deleted because it has data to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    clsPeople People = new clsPeople();
                    People = clsPeople.Find(PeopleID);

                    File.Delete(People.ImagePath);
                    //Perform Delele and refresh
                    if (clsPeople.DeletePeople(PeopleID))
                    {


                        MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RefreshPeoplesList();
                    }

                    else
                        MessageBox.Show("Person is not deleted.", "not deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

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
        private void SearchByPersonID()
        {
            string pattern = @"^\d+$";

            if (txtSearch.Text.ToString() == "")
            {
                dataGridView1.DataSource = _DataTablePeoples;
                return;
            }

            DataRow[] RowPersens;
            if (Regex.IsMatch(txtSearch.Text.ToString(), pattern))
            {
                RowPersens = _DataTablePeoples.Select("PersonID=" + txtSearch.Text.ToString());
            }
            else
            {
                txtSearch.Text = "";
                RowPersens = _DataTablePeoples.Select("PersonID=" + "- 1");
            }


            DataTable dataTable = _DataTablePeoples.Clone();

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
                dataGridView1.DataSource = _DataTablePeoples;
                return;
            }


            DataRow[] RowPerson = _DataTablePeoples.Select($"{cbFilterBy.Text} like '%{txtSearch.Text.ToString()}%'");

            DataTable dataTable = _DataTablePeoples.Clone();

            if (RowPerson.Length > 0)
            {
                dataTable = RowPerson.CopyToDataTable();
                dataTable.DefaultView.Sort = cbFilterBy.Text.ToString();
            }


            dataGridView1.DataSource = dataTable;

        }




     

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {



            if (cbFilterBy.Text == "PersonID")
            {
              
                SearchByPersonID();
            }
            else 
            { 
                SearchByName();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAndUpdetPerson frm = new AddAndUpdetPerson(-1);
            frm.ShowDialog();
            _RefreshPeoplesList();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "PersonID")
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
