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

namespace PresentationLayer.Licenses
{
    public partial class frmShowLicenseHistory : Form
    {
        private int _PersonID = -1;
       
        public frmShowLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmShowLicenseHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID==-1)
            {
                ctrlPersonCardWithFilter1.FilterFocus();
                ctrlPersonCardWithFilter1.FilterEnabled= true;
                this.Close();
                return;
            }
            else
            {
          
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID=obj;
            if(clsPerson.Find(_PersonID)==null)
            {
                MessageBox.Show("Error:this person is not a driver","Error"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = clsLicense.FindByPersonID(_PersonID).LicenseID;
            frmShowLicenseInfo frm=new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }
    }
}
