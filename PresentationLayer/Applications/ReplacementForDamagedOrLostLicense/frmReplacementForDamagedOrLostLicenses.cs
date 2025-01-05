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

namespace PresentationLayer.Applications.ReplacementForDamagedOrLostLicense
{
    public partial class frmReplacementForDamagedOrLostLicenses : Form
    {
        private int _LicenseID = -1;
        
        public frmReplacementForDamagedOrLostLicenses()
        {
            InitializeComponent();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            
            clsLicense NewLicense = clsLicense.Find(_LicenseID).Replace(clsGlobal.CurrentUser.UserID,_GetApplicationTypeID());
            if(NewLicense==null)
            {
                MessageBox.Show(
                    "Error:License Replacement Failed",
                    "Error"
                    ,MessageBoxButtons.OK
                    ,MessageBoxIcon.Error);
                return;
            }
            
            lblOldLicenseID.Text=_LicenseID.ToString();
            _LicenseID = NewLicense.LicenseID;
            lblRreplacedLicenseID.Text=_LicenseID.ToString() ;
            llShowLicenseInfo.Enabled = true;
            btnIssueReplacement.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            MessageBox.Show("License Replacement succeeded","succeeded",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            //
            //
        }
        
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement For ReplacementForDamaged License";
            lblTitle.Text = "Replacement For ReplacementForDamaged License";
            _GetApplicationTypeID();

        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement For ReplacementForLost License";
            lblTitle.Text = "Replacement For ReplacementForLost License";
            _GetApplicationTypeID();

        }
        private int _GetApplicationTypeID()
        {
            if(rbDamagedLicense.Checked)
            {
                return (int)clsApplication.enApplicationType.ReplacementForDamagedDrivingLicense;
            }
            else
            {
                return (int)clsApplication.enApplicationType.ReplacementForLostDrivingLicense;
            }
        }

        private void frmReplacementForDamagedOrLostLicenses_Load(object sender, EventArgs e)
        {
            lblCreatedByUser.Text=clsGlobal.CurrentUser.UserName;
            lblApplicationDate.Text = clsFormat.DateToShortString(DateTime.Now);
            lblApplicationFees.Text=clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();
            rbDamagedLicense.Checked = true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID=obj;
            if(_LicenseID==-1)
            {
               
                return;
            }
 
            if(clsLicense.Find(_LicenseID).IsActive==false)
            {
                return;
            }
            btnIssueReplacement.Enabled = true;

        }
    }
}
