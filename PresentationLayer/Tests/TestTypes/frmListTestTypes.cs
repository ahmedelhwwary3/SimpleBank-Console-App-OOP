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

namespace PresentationLayer.Tests.TestTypes
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtTestTypesList;
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtTestTypesList = clsTestType.GetAllTestTypesList();
            dgvTestTypes.DataSource = _dtTestTypesList;
            lblRecords.Text = dgvTestTypes.Rows.Count.ToString();
            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "Test Type ID";
                dgvTestTypes.Columns[0].Width = 150;

                dgvTestTypes.Columns[1].HeaderText = "Test Type Title";
                dgvTestTypes.Columns[1].Width = 250;

                dgvTestTypes.Columns[2].HeaderText = "Test Type Description";
                dgvTestTypes.Columns[2].Width = 350;

                dgvTestTypes.Columns[3].HeaderText = "Test Type Fees";
                dgvTestTypes.Columns[3].Width = 130;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);
        }
    }
}
