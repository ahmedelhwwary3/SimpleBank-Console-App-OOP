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

namespace PresentationLayer.Applications.ApplicationTypes
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtApplycationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtApplycationTypes = clsApplicationType.GetAllApplicationTypesList();
            dgvApplicationTypes.DataSource = _dtApplycationTypes;
            lblRecords.Text = dgvApplicationTypes.Rows.Count.ToString();
            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "Application Type ID";
                dgvApplicationTypes.Columns[0].Width = 180;

                dgvApplicationTypes.Columns[1].HeaderText = "Application Type Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Application Type Fees";
                dgvApplicationTypes.Columns[2].Width = 250;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationTypes_Load(null, null);
        }
    }
}
