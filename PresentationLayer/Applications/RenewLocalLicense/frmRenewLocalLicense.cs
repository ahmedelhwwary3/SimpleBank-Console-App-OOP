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

namespace PresentationLayer.Applications.RenewLocalLicense
{
    public partial class frmRenewLocalLicense : Form
    {
        private int _LicenseID = -1;
        public frmRenewLocalLicense()
        {
            InitializeComponent();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            clsLicense NewLicense = clsLicense.Find(_LicenseID).Renew(txtNotes.Text, clsGlobal.CurrentUser.UserID);
            if (NewLicense == null)
            {
                MessageBox.Show("Error:Renew License Failed", "Error",
                    MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                return;
            }
            lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
            llShowLicenseInfo.Enabled = true;
            btnRenewLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            _LicenseID = NewLicense.LicenseID;
            MessageBox.Show("License Renew succeeded","Done",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRenewLocalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShortString(DateTime.Now);
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicenseService).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblIssueDate.Text = clsFormat.DateToShortString(DateTime.Now);
            llShowLicenseInfo.Enabled = (_LicenseID != -1);
            

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //
            //
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            clsLicense OldLicense = clsLicense.Find(obj);
            if (OldLicense != null)
            {
                if (!OldLicense.IsDateExpirated())
                {
                    MessageBox.Show("Error:Selected License is not Expirated", "Error"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!OldLicense.IsActive)
                {
                    MessageBox.Show("Error:Selected License is not active", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);   
                    return;
                }
                _LicenseID = obj;
                lblOldLicenseID.Text = obj.ToString();
                lblExpirationDate.Text = DateTime.Now.AddYears(ctrlDriverLicenseInfoWithFilter1.License.LicenseClassInfo.DefaultValidityLength).ToString();
                lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.License.LicenseClassInfo.ClassFees.ToString();
                lblTotalFees.Text = (decimal.Parse(lblLicenseFees.Text) + decimal.Parse(lblApplicationFees.Text)).ToString();
                btnRenewLicense.Enabled = true;
            }
        }
    }
}

