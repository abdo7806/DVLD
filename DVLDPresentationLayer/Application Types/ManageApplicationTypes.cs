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

namespace DVLDPresentationLayer.Application_Types
{
    public partial class ManageApplicationTypes : Form
    {
        DataTable _DataTableApplicationTypes = new DataTable();

        public ManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationTypesList()
        {
            _DataTableApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();

            dataGridView1.DataSource = _DataTableApplicationTypes;
            lblRecords.Text = _DataTableApplicationTypes.Rows.Count.ToString();

            if(_DataTableApplicationTypes.Rows.Count > 0)
            dataGridView1.Columns[1].Width = 400; // تغيير عرض العمود
        }

        private void ManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateApplicationTypes frm = new UpdateApplicationTypes((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshApplicationTypesList();
        }
    }
}
