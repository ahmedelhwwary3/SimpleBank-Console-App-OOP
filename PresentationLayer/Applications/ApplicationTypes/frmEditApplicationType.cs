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

namespace PresentationLayer.Applications.ApplicationTypes
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }
        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This Field can not be empty");
               
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle, "");
            }

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "This Field can not be empty");
                return;
            }
            else
            {
            
                errorProvider1.SetError(txtFees, "");
            }
            if (!clsValidation.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number!");
     
            }
            else
            {
          
                errorProvider1.SetError(txtFees, "");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds are not valid ,Please chech the rec icon messages!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees = Convert.ToDecimal(txtFees.Text.Trim());
            if (_ApplicationType.Save())
            {
                MessageBox.Show("Application Type was updated successfully", "succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("Application Type update Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            if (_ApplicationType == null)
            {
                MessageBox.Show("Application Type with ID:" + _ApplicationTypeID.ToString() + " is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
        }
    }
}
