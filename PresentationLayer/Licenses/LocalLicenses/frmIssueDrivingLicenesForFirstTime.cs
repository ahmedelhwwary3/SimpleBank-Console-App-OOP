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

namespace PresentationLayer.Licenses.LocalLicenses
{
    public partial class frmIssueDrivingLicenesForFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
     
        public frmIssueDrivingLicenesForFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            
            int LicenseID = _LocalDrivingLicenseApplication.IssueDrivingLicenseForFirstTime(clsGlobal.CurrentUser.UserID,txtNotes.Text.Trim());
            if(LicenseID==-1)
            {
           
                MessageBox.Show("Error:Error with saving New License","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                btnIssueLicense.Enabled = false;
                MessageBox.Show("Save succeeded", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void frmIssueDrivingLicenesForFirstTime_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("Error:Local Driving License Application is not found","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (clsLicense.IsThereActiveLicenseForPersonPerLicenseClass(_LocalDrivingLicenseApplication.ApplicantPersonID, _LocalDrivingLicenseApplication.LicenseClassID))
            {
                MessageBox.Show("Error:This person already has an active license with LicenseClass:" + _LocalDrivingLicenseApplication.LicenseClass.ClassName, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (!_LocalDrivingLicenseApplication.DoesPassAllTestTypes())
            {
                MessageBox.Show("Error:This person did not pass all test types", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlDrivingLicenesApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplicationID);
            txtNotes.Focus();
        }
    }
}
