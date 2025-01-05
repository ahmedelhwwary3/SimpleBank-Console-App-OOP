using BusinessLayer;
using PresentationLayer.Global;
using PresentationLayer.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace PresentationLayer.Users
{
    public partial class frmAddEditUser : Form
    {
        private int _UserID;
        private clsUser _User;
        int _PersonID = -1;
        public enum enMode { AddNew, Update };
        public enMode Mode;
        public frmAddEditUser()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            Mode = enMode.Update;
        }


         
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


         

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "This field can not be empty");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwords are not matched with each other");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }



        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password can not be empty");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true; // Cancel the event if the username is empty
                errorProvider1.SetError(txtUserName, "User name cannot be empty");
                return;
            }
            else
            {
                e.Cancel = false; // Allow the event to proceed if the username is not empty
                errorProvider1.SetError(txtUserName, null);
            }

            if (Mode == enMode.AddNew && clsUser.IsExist(txtUserName.Text.Trim()))
            {
                e.Cancel = true; // Cancel the event if the username already exists
                errorProvider1.SetError(txtUserName, "This username is taken by another user. Choose another one");
            }
            else
            {
                e.Cancel = false; // Allow the event to proceed if the username is unique
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                tcAddNewUser.TabPages[1].Enabled = true;
                btnSave.Enabled = true;
                tcAddNewUser.SelectedTab = tcAddNewUser.TabPages["tpLoginInfo"];
                return;
            }
            if (_PersonID != -1)
            {
                if (clsUser.FindByPersonID(_PersonID) != null)
                {
                    MessageBox.Show("This person is taken by another user.Use another one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                tcAddNewUser.TabPages[1].Enabled = true;
                btnSave.Enabled = true;
                tcAddNewUser.SelectedTab = tcAddNewUser.TabPages["tpLoginInfo"];
                return;
            }
            else
            {
                MessageBox.Show("This person is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void _ResetDefaultValues()
        {
            if (Mode == enMode.AddNew)
            {

                _User = new clsUser();
                this.Text = "Add New User";
                lblAddEditUser.Text = "Add New User";
                ctrlPersonCardWithFilter1.FilterFocus();
                btnSave.Enabled = false;
                tcAddNewUser.TabPages[1].Enabled = false;//Login Info Page
            }
            else
            {
                this.Text = "Edit User";
                lblAddEditUser.Text = "Edit User";
                _User = clsUser.FindByUserID(_UserID);
                if (_User != null)
                {
                    ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
                    btnSave.Enabled = true;

                }
                else
                {
                    MessageBox.Show("No User is found with ID:" + _UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            //2 Modes
            txtConfirmPassword.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
        }
        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (Mode == enMode.Update)
            {
                _LoadData();
            }

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid.Check the red icon messages", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //User is new with Data or Old with Updated data
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = clsUtil.ComputeHash(txtPassword.Text.Trim());
            _User.PersonID = ctrlPersonCardWithFilter1.SelectedPersonInfo.PersonID;
            if (chkIsActive.Checked)
            {
                _User.IsActive = true;
            }
            else
            {
                _User.IsActive = false;
            }

            if (_User.Save())
            {
                Mode = enMode.Update;
                MessageBox.Show("User Updated successfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("User Update Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID= obj;
        }
    }
    }
        
    
        

