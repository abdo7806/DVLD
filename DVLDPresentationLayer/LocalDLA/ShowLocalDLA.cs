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
    public partial class ShowLocalDLA : Form
    {
        int _LocalDLA_ID = -1;
        public ShowLocalDLA(int localDLA_ID)
        {
            InitializeComponent();
            _LocalDLA_ID = localDLA_ID;
        }

        private void ShowLocalDLA_Load(object sender, EventArgs e)
        {
            userControl41.ShowData(_LocalDLA_ID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
