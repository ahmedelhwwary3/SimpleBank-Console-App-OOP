﻿namespace PresentationLayer.Users
{
    partial class frmUserInfo
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
            this.ctrlUserCard1 = new PresentationLayer.Users.ctrlUserCard();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlUserCard1
            // 
            this.ctrlUserCard1.BackColor = System.Drawing.Color.White;
            this.ctrlUserCard1.Location = new System.Drawing.Point(3, 3);
            this.ctrlUserCard1.Name = "ctrlUserCard1";
            this.ctrlUserCard1.Size = new System.Drawing.Size(870, 446);
            this.ctrlUserCard1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::PresentationLayer.Properties.Resources.Close_32;
            this.btnClose.Location = new System.Drawing.Point(672, 442);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 55);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(868, 503);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlUserCard1);
            this.Name = "frmUserInfo";
            this.Text = "frmUserInfo";
            this.Load += new System.EventHandler(this.frmUserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlUserCard ctrlUserCard1;
        private System.Windows.Forms.Button btnClose;
    }
}