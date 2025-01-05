using BusinessLayer;
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

namespace PresentationLayer.Applications.LocalDrivingLicenseApplications.Controls
{
    public partial class ctrlDrivingLicenesApplicationInfo : UserControl
    {
        private int _BaseApplicationID = -1;
        private clsApplication _BaseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public ctrlDrivingLicenesApplicationInfo()
        {
            InitializeComponent();
        }
        public int BaseApplicationID
        {
            get { return _BaseApplicationID; }
        }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo
        {
            get { return _LocalDrivingLicenseApplication; }
        }
        public clsApplication BaseApplication
        {
            get { return _BaseApplication; }
        }
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        private void _ResetControl()
        {
            ctrlApplicationBasicInfo1.ResetDefaultValues();
            lblAppliedForLicense.Text = "[???]";
            lblLDLAppID.Text = "[???]";
            lblPassedTests.Text = "[???]";
            llShowLicenseInfo.Enabled = false;
        }
        public void LoadLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No local driving license application is found with ID:" + _LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetControl();
                return;
            }
            lblAppliedForLicense.Text = _LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblLDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTests().ToString();
            ctrlApplicationBasicInfo1.LoadBasicApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);
            //llShowLicenseInfo.Enabled = clsLicense.IsLicenseIssued(_LocalDrivingLicenseApplication);


        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }
    }
}
