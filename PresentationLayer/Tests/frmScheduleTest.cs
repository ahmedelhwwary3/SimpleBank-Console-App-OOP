using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.Vision;
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType testTypeID,int TestAppointmentID=-1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = testTypeID;
            _TestAppointmentID = TestAppointmentID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID= _TestTypeID;
            ctrlScheduleTest1.LoadTestAppointmentData(_LocalDrivingLicenseApplicationID,_TestAppointmentID);
        }
    }
}
