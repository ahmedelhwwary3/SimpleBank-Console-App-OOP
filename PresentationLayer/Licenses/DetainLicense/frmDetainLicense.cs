using BusinessLayer;
using PresentationLayer.Global;
using PresentationLayer.Licenses.LocalLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Licenses.DetainLicense
{
    public partial class frmDetainLicense : Form
    {
        private int _LicenseID = -1;
        private int _PersonID = -1;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                
                return;
            }
            int DetainID = clsLicense.Find(_LicenseID).Detain(int.Parse(txtFineFees.Text.Trim()),clsGlobal.CurrentUser.UserID);
            if(DetainID==-1)
            {
                MessageBox.Show("Error:Detain Failed","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text=DetainID.ToString();
            MessageBox.Show("Detaind succeeded","Detain",MessageBoxButtons.OK,MessageBoxIcon.Information);
            llShowLicenseHistory.Enabled=true;
            llShowLicenseInfo.Enabled=true;
            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.Enabled = false;
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm=new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = clsLicense.Find(_LicenseID).Driver.PersonID;
            frmShowLicenseHistory frm=new frmShowLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainDate.Text=clsFormat.DateToShortString(DateTime.Now);
            llShowLicenseInfo.Enabled = (_LicenseID!=-1);
            llShowLicenseHistory.Enabled = (_PersonID!=-1);
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            if(_LicenseID==-1)
            {
                MessageBox.Show("Error:Invalid License ID","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(clsLicense.Find(_LicenseID)==null)
            {
                return;
            }
            if (clsLicense.Find(_LicenseID).IsLicenseDetained())
            {
                ctrlDriverLicenseInfoWithFilter1.Clear();
                MessageBox.Show("Error:License is detained","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _PersonID = clsLicense.Find(_LicenseID).Driver.PersonID;
            lblLicenseID.Text = _LicenseID.ToString();
            btnDetain.Enabled = true;
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees,"Input Fees");
                txtFineFees.Focus();
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }
            if(!clsValidation.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Input Valid Number");
                txtFineFees.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }
        }
    }
}
