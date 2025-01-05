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

namespace PresentationLayer.Applications.LocalDrivingLicenseApplications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID = -1;
        private clsApplication _Application;
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public int ApplicationID
        {
            get { return _ApplicationID; }
        }
        public clsApplication Application
        { get { return _Application; } }

        public void ResetDefaultValues()
        {
            _ResetControl();
        }
        private void _ResetControl()
        {
            llViewPersonInfo.Enabled = false;
            lblApplicant.Text = "[???]";
            lblCreatedByUser.Text = "[???]";
            lblDate.Text = "[???]";
            lblFees.Text = "[???]";
            lblID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblStatusDate.Text = "[???]";
            lblType.Text = "[???]";
        }

        public void LoadBasicApplicationInfo(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            _Application = clsApplication.FindBaseApplication(_ApplicationID);
            if (_Application == null)
            {
                MessageBox.Show("No Application was found with ID:" + _ApplicationID.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblApplicant.Text = _Application.ApplicantPersonID.ToString();
            lblCreatedByUser.Text = _Application.CreatedByUser.UserName;
            lblDate.Text = clsFormat.DateToShortString(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShortString(_Application.LastStatusDate);
            lblType.Text = clsApplicationType.Find((int)_Application.ApplicationTypeID).ApplicationTypeTitle;
            lblFees.Text = _Application.PaidFees.ToString();
            lblID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = (_Application.ApplicationStatusText);


        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonCard frm = new frmShowPersonCard(_Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void llViewPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonCard frm=new frmShowPersonCard(_Application.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
