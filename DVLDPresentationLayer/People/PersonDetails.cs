using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
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


namespace DVLDPresentationLayer.People
{
    public partial class PersonDetails : Form
    {

        int _PersonID = -1;



        clsPeople _Person;
        public PersonDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
          
            UserControl1.setPersonID(PersonID);

        }



     

        private void btnClose_Click(object sender, EventArgs e)
        {
            // pictureBox1.Dispose();
            // pictureBox1.Image = null;
            UserControl1.setPersonID(-1);

            this.Close();
        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
