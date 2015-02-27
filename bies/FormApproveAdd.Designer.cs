namespace bies
{
    partial class FormApproveAdd
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
            this.simpleButton_Yes = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_No = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit_remark = new DevExpress.XtraEditors.MemoEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textEdit_title = new DevExpress.XtraEditors.TextEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_remark.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_title.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButton_Yes
            // 
            this.simpleButton_Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.simpleButton_Yes.Location = new System.Drawing.Point(12, 194);
            this.simpleButton_Yes.Name = "simpleButton_Yes";
            this.simpleButton_Yes.Size = new System.Drawing.Size(97, 44);
            this.simpleButton_Yes.TabIndex = 0;
            this.simpleButton_Yes.Text = "Одобрено";
            this.simpleButton_Yes.Click += new System.EventHandler(this.simpleButton_Yes_Click);
            // 
            // simpleButton_No
            // 
            this.simpleButton_No.DialogResult = System.Windows.Forms.DialogResult.No;
            this.simpleButton_No.Location = new System.Drawing.Point(115, 194);
            this.simpleButton_No.Name = "simpleButton_No";
            this.simpleButton_No.Size = new System.Drawing.Size(97, 44);
            this.simpleButton_No.TabIndex = 1;
            this.simpleButton_No.Text = "Не одобрено";
            this.simpleButton_No.Click += new System.EventHandler(this.simpleButton_No_Click);
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_Cancel.Location = new System.Drawing.Point(218, 194);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(97, 44);
            this.simpleButton_Cancel.TabIndex = 2;
            this.simpleButton_Cancel.Text = "Отмена";
            // 
            // memoEdit_remark
            // 
            this.memoEdit_remark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit_remark.Location = new System.Drawing.Point(3, 16);
            this.memoEdit_remark.Name = "memoEdit_remark";
            this.memoEdit_remark.Properties.MaxLength = 200;
            this.memoEdit_remark.Size = new System.Drawing.Size(297, 101);
            this.memoEdit_remark.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.memoEdit_remark);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 120);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Замечание/резолюция";
            // 
            // textEdit_title
            // 
            this.textEdit_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit_title.Location = new System.Drawing.Point(3, 16);
            this.textEdit_title.Name = "textEdit_title";
            this.textEdit_title.Properties.MaxLength = 50;
            this.textEdit_title.Size = new System.Drawing.Size(294, 20);
            this.textEdit_title.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textEdit_title);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 35);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "название";
            // 
            // FormApproveAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 250);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButton_Cancel);
            this.Controls.Add(this.simpleButton_No);
            this.Controls.Add(this.simpleButton_Yes);
            this.Name = "FormApproveAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Одобрить/замечание";
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_remark.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_title.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton_Yes;
        private DevExpress.XtraEditors.SimpleButton simpleButton_No;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private DevExpress.XtraEditors.MemoEdit memoEdit_remark;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit textEdit_title;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}