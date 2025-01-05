using BusinessLayer;
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
    public partial class frmUsersManagement : Form
    {
        private DataTable _dtUsersList;
        public frmUsersManagement()
        {
            InitializeComponent();
        }

         

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            frmUsersManagement_Load(null, null);
        }

      

         
        private void changePasswordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void frmUsersManagement_Load(object sender, EventArgs e)
        {
            _dtUsersList = clsUser.GetAllUsersList();
            dgvUsers.DataSource = _dtUsersList;
            cbFilterColumn.Text = "None";
            cbIsActive.Visible = false;
            lblRecords.Text = _dtUsersList.Rows.Count.ToString();
            if (_dtUsersList.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 180;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 180;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 230;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 180;
            }
        }

        private void btnAddNewUser_Click_1(object sender, EventArgs e)
        {
            frmAddEditUser frm=new frmAddEditUser();
            frm.ShowDialog();
            frmUsersManagement_Load(null, null);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterColumn_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbFilterColumn.Text == "Is Active")
            {
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
                txtFilterValue.Visible = false;
                return;
            }

            txtFilterValue.Visible = (cbFilterColumn.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
                cbIsActive.Visible = false;
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            //It can not be (Is Active) as the logic we set in 'cbFilterColumn_SelectedIndexChanged' prevent this
            string FilterColumn = "";
            switch (cbFilterColumn.Text)
            {
                case "Person ID":
                    {
                        FilterColumn = "PersonID";
                        break;
                    }
                case "User ID":
                    {
                        FilterColumn = "UserID";
                        break;
                    }
                case "User Name":
                    {
                        FilterColumn = "UserName";
                        break;
                    }
                case "Password":
                    {
                        FilterColumn = "Password";
                        break;
                    }

                default:
                    { break; }
            }
            if (txtFilterValue.Text.Trim() == "" || cbFilterColumn.Text == "None")
            {
                _dtUsersList.DefaultView.RowFilter = "";
                lblRecords.Text = _dtUsersList.Rows.Count.ToString();
                return;
            }
            if (cbFilterColumn.Text == "Person ID" || cbFilterColumn.Text == "User ID")
            {
                _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());


            }
            else
            {
                _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
                lblRecords.Text = _dtUsersList.Rows.Count.ToString();

            }
            lblRecords.Text = _dtUsersList.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterColumn.Text == "Person ID" || cbFilterColumn.Text == "User ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void addNewUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            frmUsersManagement_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmShowPersonCard frm = new frmShowPersonCard((int)dgvUsers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmUsersManagement_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this User?", "confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {


                if (clsUser.Delete((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User was deleted successfully", "delete succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmUsersManagement_Load(null, null);
                }
                else
                {
                    MessageBox.Show("User was not deleted successfully", "delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void cbIsActive_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = "";
            switch (cbIsActive.Text)
            {

                case "Yes":
                    {
                        FilterValue = "1";
                        break;
                    }
                case "No":
                    {
                        FilterValue = "0";
                        break;
                    }
                default:
                    { break; }
            }
            if (cbIsActive.Text == "All")
            {
                _dtUsersList.DefaultView.RowFilter = "";
            }
            else
            {
                _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            }
            lblRecords.Text = _dtUsersList.Rows.Count.ToString();

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This method is not implemented yet", "Stup", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }                                                                                     
                                                                                              
        private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)               
        {                                                                                     
            MessageBox.Show("This method is not implemented yet", "Stup", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }                                                                                     
    }
}
