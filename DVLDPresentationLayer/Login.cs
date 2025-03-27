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

namespace DVLDPresentationLayer
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        // نعيب بيانات التذكير
        private void RegisterDataRememberMe()
        {
            try
            {
                // نشوف هل نسجل بيانات الدخول له
                if (chkRememberMe.Checked)
                {
                    string RememberMe = ClsUsers._User.UserName + "\n" + textBox2.Text.Trim();
                    File.WriteAllText("MyFileRememberMe.txt", RememberMe);
                }
                else
                {
                    string RememberMe = "\n";
                    File.WriteAllText("MyFileRememberMe.txt", RememberMe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // عرض بيانات التذكير
        private void ViewReminderData()
        {
         
            try
            {
                // فتح الملف للقراءة
                using (StreamReader sr = new StreamReader("MyFileRememberMe.txt"))
                {
                    // قراءة وطباعة كل سطر من الملف
                    string line;
                    if((line = sr.ReadLine()) != null)
                    {

                        textBox1.Text = line;

                        line = sr.ReadLine();
                        textBox2.Text = line;
                        if(textBox2.Text.Trim() != "" || textBox1.Text.Trim() != "")
                        {
                            chkRememberMe.Checked = true;
                        }
                        else
                        {
                            chkRememberMe.Checked = false;
                        }
                    }
                    else
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                        chkRememberMe.Checked = false;

                    }





                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: "+ex.Message, "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            //التحقق لو كان المستخدم موجود
            if(ClsUsers.Find(textBox1.Text, textBox2.Text))
            {
                // التحقق لو المستخدم ناشط
                if(ClsUsers.UserIsActive())
                {

                        RegisterDataRememberMe();// نعيب بيانات التذكير

                   // MessageBox.Show(ClsUsers._User.UserID.ToString());

                    Index frm = new Index(ClsUsers._User.UserID);
                    frm.ShowDialog();
                    // عرض بيانات التذكير
                    ViewReminderData();
                }
                else
                {
                    MessageBox.Show("User Is Not Active", "Worng Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Invalid UserName/Password.", "Worng Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ViewReminderData();
        }
    }
}
