using BusinessLayer;
using PresentationLayer.Global;
using PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.Vision;
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set {
                _TestTypeID = value;
                switch(_TestTypeID)
                {
                    case clsTestType.enTestType.Vision:
                        {
                            lblTestTypeTitle.Text = "Vision Test";
                            pbTestType.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestType.enTestType.Written:
                        {
                            lblTestTypeTitle.Text = "Written Test";
                            pbTestType.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.Street:
                        {
                            lblTestTypeTitle.Text = "Street Test";
                            pbTestType.Image = Resources.driving_test_512;
                            break;
                        }



                }
            }

        }
        public enum enMode { AddNew,Update}
        public enMode Mode= enMode.AddNew;
        public enum enCreationMode
        {
            FirstTime,RetakeTest
        }
        public enCreationMode CreationMode = enCreationMode.FirstTime;

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        private bool LoadData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if(_TestAppointment==null)
            {
                MessageBox.Show("Error:Test Appointment with ID:"+_TestAppointmentID.ToString()+" was not found","Error",
         MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            //we already got the local driving license application data like (licenseClass) before
            //we also have (Date)&(Fees)&(Retake Application info "if was found")might have changed in the appointment data
            if(DateTime.Compare(DateTime.Now,_TestAppointment.AppointmentDate)<0)
            {
                dtpScheduleDate.MinDate= DateTime.Now;
            }
            else
                dtpScheduleDate.MinDate =_TestAppointment.AppointmentDate;
            lblFees.Text= _TestAppointment.PaidFees.ToString();
            if(_TestAppointment.RetakeTestApplicationID==-1)
            {
                gbRetakeTestInfo.Enabled =false;
                lblRTestAppID.Text = "N/A";
                lblRAppFees.Text = "0";
                lblTestTypeTitle.Text = "Schedule Test First Time";

            }
            else
            {
                gbRetakeTestInfo.Enabled = true;
                lblRTestAppID.Text = _TestAppointment.RetakeTestApplicationInfo.ApplicationID.ToString();
                lblRAppFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
                lblTestTypeTitle.Text = "Schedule Retake Test";
            }
            return true;
        }
        public void LoadTestAppointmentData(int LocalDrivingLicenseApplication,int TestAppointmentID=-1)
        {
            _TestAppointmentID= TestAppointmentID;
            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplication;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("Error: Local Driving License Application was not found","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            

            if (_TestAppointmentID == -1)
            {
                Mode = enMode.AddNew;
            }
            else
                Mode = enMode.Update;
            if (_LocalDrivingLicenseApplication.DoesAttentTestType(this.TestTypeID))
            {
                CreationMode = enCreationMode.RetakeTest;
            }
            else
                CreationMode = enCreationMode.FirstTime;

            if(CreationMode==enCreationMode.FirstTime)
            {
                gbRetakeTestInfo.Enabled = false;
                lblRAppFees.Text = "0";
                lblTestTypeTitle.Text = "Schedule Test First Time";
                lblRTestAppID.Text = "N/A";

            }
            else
            {
                gbRetakeTestInfo.Enabled = true;
                lblRAppFees.Text=clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                lblTestTypeTitle.Text = "Schedule Retake Test";
                lblRTestAppID.Text = "[???]";
            }
            lblClassName.Text = _LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblFees.Text=_LocalDrivingLicenseApplication.PaidFees.ToString();
            lblLDAppID.Text=_LocalDrivingLicenseApplicationID.ToString();
            lblName.Text = _LocalDrivingLicenseApplication.Person.FullName;
            lblTrials.Text = _LocalDrivingLicenseApplication.CountAllTestTrials(_TestTypeID).ToString();
            if(Mode==enMode.AddNew)
            {
                dtpScheduleDate.MinDate = DateTime.Now;
                lblFees.Text=clsTestType.Find(_TestTypeID).TestTypeFees.ToString();
                _TestAppointment=new clsTestAppointment();
            }
            else
            {
                if (!LoadData())
                    return;
            }
       
            //In AddNew or Update Mode
            lblTotalFees.Text = (decimal.Parse(lblFees.Text) + decimal.Parse(lblRAppFees.Text)).ToString();
            if (!HandleIfPersonHaveActiveAppointmentInAddNewMode())
                return;
            if (!HandleIfTestAppointmentIsLockedInUpdateMode())
                return;
            if (!HandleIfPersonPassThePreviousTestType())
                return;
        }
        private bool HandleIfPersonHaveActiveAppointmentInAddNewMode()
        {
            //If User clicked the (add new appointment) button And there was an active test appointment
            if (Mode == enMode.AddNew && _LocalDrivingLicenseApplication.DoesHaveActiveTestAppointment(_TestTypeID))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "You Can not Schedule a new test appointment as you already have an active one";
                dtpScheduleDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
                return true;
        }
        private bool HandleIfTestAppointmentIsLockedInUpdateMode()
        {
            //it will only work with update mode (testAppointment!=null) else will return true
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "You Can not Update This  test appointment as It is Locked";
                dtpScheduleDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            return true;
        }
        private bool HandleIfPersonPassThePreviousTestType()
        {
            switch(TestTypeID)
            {
                case clsTestType.enTestType.Vision:
                    {
                        return true;
                    }
                case clsTestType.enTestType.Written:
                    {
                        if(_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.Vision))
                        {
                            return true;
                        }
                        else
                        {
                            btnSave.Enabled = false;
                            dtpScheduleDate.Enabled = false;
                            lblUserMessage.Visible = true;
                            lblUserMessage.Text = "You had not passed the vision Test yet";
                            return false;
                        }
                    }
                case clsTestType.enTestType.Street:
                    {
                        if (_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.Written))
                        {
                            return true;
                        }
                        else
                        {
                            btnSave.Enabled = false;
                            dtpScheduleDate.Enabled = false;
                            lblUserMessage.Visible = true;
                            lblUserMessage.Text = "You had not passed the written Test yet";
                            return false;
                        }
                    }




            }
            return true;
        }
        private bool HandleRetakeTestApplication()
        {
            clsApplication RetakeApplication = new clsApplication();
            //as if the mood was update .. you must not add a new application (it is already exists)
            if (Mode==enMode.AddNew&&CreationMode==enCreationMode.RetakeTest)
            {
                
                RetakeApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                RetakeApplication.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                RetakeApplication.ApplicationDate = DateTime.Now;
                RetakeApplication.LastStatusDate = DateTime.Now;
                RetakeApplication.ApplicationTypeID = clsApplication.enApplicationType.RetakeTest;
                RetakeApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                RetakeApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                
                if(!RetakeApplication.Save())
                {
                    MessageBox.Show("Error:Retake Application Save Failed","Error",
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
                    btnSave.Enabled = false;
                    return false;
                }
            }
            _TestAppointment.RetakeTestApplicationID=RetakeApplication.ApplicationID;
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!HandleRetakeTestApplication())
                return;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate= dtpScheduleDate.Value;
            _TestAppointment.PaidFees = decimal.Parse(lblFees.Text);
            _TestAppointment.CreatedByUserID= clsGlobal.CurrentUser.UserID;
            _TestAppointment.IsLocked = false;
            _TestAppointment.TestTypeID=_TestTypeID;
            if(!_TestAppointment.Save())
            {
                MessageBox.Show("Error: Test Appointment Save Failed","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                Mode = enMode.Update;
                MessageBox.Show("Test Appointment Was Saved Successfully","Saved",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }
    }
}
