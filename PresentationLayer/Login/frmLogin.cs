using BusinessLayer;
using PresentationLayer.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace PresentationLayer.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string HashedPassword=clsUtil.ComputeHash(this.txtPassword.Text.ToString());
            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(),HashedPassword);
            if(User!=null)
            {
                if(User.IsActive==false)
                {
                    MessageBox.Show("User is not active,Please contact admin!",
                        "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if(chkRemember.Checked)
                {
                    clsGlobal.RememberCredentialsToRegistry(txtUserName.Text,txtPassword.Text);
                }
                else
                {
                    //instead of deleting file
                    clsGlobal.RememberCredentialsToRegistry("","");
                }
            }
            else
            {
                MessageBox.Show("Invalid User Name/Password!",
              "Wrong Credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            clsGlobal.CurrentUser= User;
            this.Hide();
            frmMain frm=new frmMain(this);
            frm.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            clsGlobal.GetStoredCredentialsFromRegistry(ref UserName, ref Password);
            
                
            txtUserName.Text = UserName;
            txtPassword.Text = Password;
            txtUserName.Focus();
            chkRemember.Checked = true;
            
             
        }
    }
}
