using BusinessLayer;
using PresentationLayer.Global;
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
using System.IO;

namespace PresentationLayer.People
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _PersonInfo;
        private int _PersonID;
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return _PersonInfo; }
        }
        private void _ResetDefaultValues()
        {
            lblAddress.Text = "[????]";
            lblCountry.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblEmail.Text = "[????]";
            lblFullName.Text = "[????]";
            lblGendor.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblPersonID.Text = "[????]";
            lblPhone.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
            llEdit.Enabled = false;
        }
        public void ResetDefaultValues()
        {
            _ResetDefaultValues();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _PersonInfo = clsPerson.Find(PersonID);

            if (_PersonInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Error:Person is not found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _LoadData();
        }
        private void _LoadData()
        {
            _PersonID = SelectedPersonInfo.PersonID;
            lblAddress.Text = _PersonInfo.Address;
            lblCountry.Text = _PersonInfo.Country.CountryName;
            lblDateOfBirth.Text = clsFormat.DateToShortString(_PersonInfo.DateOfBirth);
            lblEmail.Text = _PersonInfo.Email;
            lblGendor.Text = (_PersonInfo.Gendor == 1 ? "Male" : "Female");
            if (_PersonInfo.Gendor == 0)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
                pbPersonImage.Image = Resources.Female_512;
            lblNationalNo.Text = _PersonInfo.NationalNo;
            lblPersonID.Text = _PersonInfo.PersonID.ToString();
            lblPhone.Text = _PersonInfo.Phone;
            lblFullName.Text = _PersonInfo.FullName;
            if (_PersonInfo.ImagePath != "" && File.Exists(_PersonInfo.ImagePath))
            {
                pbPersonImage.ImageLocation = _PersonInfo.ImagePath;
            }
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _PersonInfo = clsPerson.Find(NationalNo);
            if (_PersonInfo == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Error:Person is not found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _LoadData();
        }
        private void llEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonInfo.PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }
    }
}
