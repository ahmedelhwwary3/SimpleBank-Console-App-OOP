using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Licenses.LocalLicenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _License;
        private bool _FilterEnabled = true;



        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler=OnLicenseSelected;
            if(handler!=null)
            {
                handler(LicenseID);
            }
        }
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set { 
                _FilterEnabled = value;
                gbFilters.Enabled=_FilterEnabled;
            }
        }
        public int LicenseID
        {
            get {return ctrlDriverLicenseInfo1.LicenseID; }
        }
        public clsLicense License
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        public void LoadLicenseInfo(int LicenseID)
        {
         
            
            if(LicenseID==-1)
            {
                return;
            }
            ctrlDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID =ctrlDriverLicenseInfo1.LicenseID;
            if (FilterEnabled && OnLicenseSelected != null)
            {
                OnLicenseSelected(LicenseID);
            }
        }
        public void Clear()
        {
            ctrlDriverLicenseInfo1.Clear();
        }
        public void FilterFocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only digits
            e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
            if(e.KeyChar==(char)13)
            {
                btnFind.PerformClick();
            }
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtLicenseID.Text))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtLicenseID,"This field can not be empty");
                return;
            }
            else
            {
                errorProvider1.SetError(txtLicenseID,null);
            }
            clsLicense License = clsLicense.Find(int.Parse(txtLicenseID.Text));
            if (License==null)
            {
                errorProvider1.SetError(txtLicenseID, "License is not found");
                return;
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
            
            if (License.IsActive==false)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This license is not active");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID,null);
            }
        }
         

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Error:Check the red icon messages", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadLicenseInfo(int.Parse(txtLicenseID.Text));
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;        
            
        }
    }
}
