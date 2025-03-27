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

namespace DVLDPresentationLayer.Test_Types
{
    public partial class ManageTestTypes : Form
    {
        DataTable _DataTableTestTypes = new DataTable();

        public ManageTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            _DataTableTestTypes = clsTestTypes.GetAllTestTypes();

            dataGridView1.DataSource = _DataTableTestTypes;
            lblRecords.Text = _DataTableTestTypes.Rows.Count.ToString();
            if(_DataTableTestTypes.Rows.Count > 0)
            {
                dataGridView1.Columns[2].Width = 450; // تغيير عرض العمود
                dataGridView1.Columns[1].Width = 200; // تغيير عرض العمود
            }


        }

        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTestType frm = new UpdateTestType((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshTestTypesList();

        }
    }
}
