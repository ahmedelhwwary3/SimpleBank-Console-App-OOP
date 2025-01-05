using BusinessLayer;
using PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;
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
                            lblTestTypeTitle.Text = "Scheduled Vision Test";
                            pbTestType.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestType.enTestType.Written:
                        {
                            lblTestTypeTitle.Text = "Scheduled Written Test";
                            pbTestType.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.Street:
                        {
                            lblTestTypeTitle.Text = "Scheduled Street Test";
                            pbTestType.Image = Resources.driving_test_512;
                            break;
                        }


                }
            }
        }
        public int _TestID = -1;
        public int TestID
        {
            get { return _TestID; }
            set { _TestID = value;
                lblTestID.Text = _TestID.ToString();
            }
        }
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }
        public void LoadScheduledTestData(int TestAppointmentID,clsTestType.enTestType TestTypeID,int TestID=-1)
        {
            _TestTypeID = TestTypeID;
            _TestAppointmentID =TestAppointmentID;
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if(_TestAppointment==null)
            {
                MessageBox.Show("Error:Test Appointment with ID:"+_TestAppointmentID.ToString()+" is not found","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            
            lblClassName.Text = _TestAppointment.LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblDate.Text = _TestAppointment.AppointmentDate.ToString();
            lblFees.Text=_TestAppointment.PaidFees.ToString();
            lblLDAppID.Text=_TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblName.Text = _TestAppointment.LocalDrivingLicenseApplication.Person.FullName;
            lblTestID.Text = TestID.ToString();
            lblTestTypeTitle.Text = _TestAppointment.TestType.TestTypeTitle;
            lblTrials.Text = _TestAppointment.LocalDrivingLicenseApplication.CountAllTestTrials(_TestAppointment.TestTypeID).ToString();

        }


    }
}
