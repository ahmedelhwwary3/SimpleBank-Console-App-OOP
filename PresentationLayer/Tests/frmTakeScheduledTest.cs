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
using static System.Net.Mime.MediaTypeNames;

namespace PresentationLayer.Tests
{
    public partial class frmTakeScheduledTest : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.Vision;
        private int _TestAppointmentID = -1;
        private int _TestID = -1;
        private clsTest _Test;
        public frmTakeScheduledTest(clsTestType.enTestType TestTypeID,int TestAppointmentID, int TestID=-1)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
            _TestAppointmentID = TestAppointmentID;
            _TestID = TestID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to Save Test?","Save"
                ,MessageBoxButtons.OKCancel,MessageBoxIcon.Question)!=DialogResult.OK)
            {
                return;
            }
            //If Edit Mode Notes only can be changed And TestID!=-1()

            if (rbPass.Checked)
            {
                _Test.TestResult = true;
            }
            else
                _Test.TestResult = false;
            
            _Test.Notes = txtNotes.Text;
            _Test.CreatedByUserID=clsGlobal.CurrentUser.UserID; 
            _Test.TestAppointmentID= _TestAppointmentID;
            if(_Test.Save())
            {
                //to prevent from editing Result
                ctrlScheduledTest1.TestID= _Test.TestID;
                btnSave.Enabled = false;           
                MessageBox.Show("Test was saved successfully","Save succeeded",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error:Test was not saved", "Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }


        }

        private void frmTakeScheduledTest_Load(object sender, EventArgs e)
        {
            
            if (clsTestAppointment.Find(_TestAppointmentID).IsLocked)
            {
                MessageBox.Show("Error:This Test Appointment is locked", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            ctrlScheduledTest1.LoadScheduledTestData(_TestAppointmentID, _TestTypeID, _TestID);
            if(_TestID!=-1)
            {
                //Edit Mode
                _Test=clsTest.Find(_TestID);
                if(_Test.TestResult==true)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes;
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "You can not edit the Result.Just Notes";
                rbFail.Enabled = false;
                rbPass.Enabled = false;
            }
            else
            {
                //Add New Mode
                _Test=new clsTest();
            }
        }
    }
}
