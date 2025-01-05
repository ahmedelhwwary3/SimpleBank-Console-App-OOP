namespace PresentationLayer.Applications.LocalDrivingLicenseApplications.Controls
{
    partial class frmShowLocalDrivingLicenseApplicationInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlDrivingLicenesApplicationInfo1 = new PresentationLayer.Applications.LocalDrivingLicenseApplications.Controls.ctrlDrivingLicenesApplicationInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Image = global::PresentationLayer.Properties.Resources.Close_32;
            this.btnClose.Location = new System.Drawing.Point(814, 427);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 44);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlDrivingLicenesApplicationInfo1
            // 
            this.ctrlDrivingLicenesApplicationInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlDrivingLicenesApplicationInfo1.Location = new System.Drawing.Point(2, 2);
            this.ctrlDrivingLicenesApplicationInfo1.Name = "ctrlDrivingLicenesApplicationInfo1";
            this.ctrlDrivingLicenesApplicationInfo1.Size = new System.Drawing.Size(971, 436);
            this.ctrlDrivingLicenesApplicationInfo1.TabIndex = 0;
            // 
            // frmShowLocalDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(970, 483);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlDrivingLicenesApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowLocalDrivingLicenseApplicationInfo";
            this.Text = "frmShowLocalDrivingLicenseApplicationInfo";
            this.Load += new System.EventHandler(this.frmShowLocalDrivingLicenseApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDrivingLicenesApplicationInfo ctrlDrivingLicenesApplicationInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}