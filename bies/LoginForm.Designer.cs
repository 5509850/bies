namespace bies
{
    partial class LoginForm
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
            this.simpleButton_OK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_login = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_password = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl_pass = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_new = new DevExpress.XtraEditors.TextEdit();
            this.labelControl_new = new DevExpress.XtraEditors.LabelControl();
            this.labelControl_result = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton_save = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_login.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_new.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton_OK
            // 
            this.simpleButton_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_OK.Location = new System.Drawing.Point(19, 115);
            this.simpleButton_OK.Name = "simpleButton_OK";
            this.simpleButton_OK.Size = new System.Drawing.Size(75, 39);
            this.simpleButton_OK.TabIndex = 2;
            this.simpleButton_OK.Text = "OK";
            this.simpleButton_OK.Click += new System.EventHandler(this.simpleButton_OK_Click);
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_Cancel.Location = new System.Drawing.Point(214, 115);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(75, 39);
            this.simpleButton_Cancel.TabIndex = 4;
            this.simpleButton_Cancel.Text = "Отмена";
            this.simpleButton_Cancel.Click += new System.EventHandler(this.simpleButton_Cancel_Click);
            // 
            // textEdit_login
            // 
            this.textEdit_login.Location = new System.Drawing.Point(101, 29);
            this.textEdit_login.Name = "textEdit_login";
            this.textEdit_login.Properties.MaxLength = 50;
            this.textEdit_login.Size = new System.Drawing.Size(188, 20);
            this.textEdit_login.TabIndex = 0;
            // 
            // textEdit_password
            // 
            this.textEdit_password.Location = new System.Drawing.Point(101, 59);
            this.textEdit_password.Name = "textEdit_password";
            this.textEdit_password.Properties.MaxLength = 50;
            this.textEdit_password.Properties.PasswordChar = '*';
            this.textEdit_password.Size = new System.Drawing.Size(188, 20);
            this.textEdit_password.TabIndex = 1;
            this.textEdit_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit_password_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "логин";
            // 
            // labelControl_pass
            // 
            this.labelControl_pass.Location = new System.Drawing.Point(19, 66);
            this.labelControl_pass.Name = "labelControl_pass";
            this.labelControl_pass.Size = new System.Drawing.Size(36, 13);
            this.labelControl_pass.TabIndex = 6;
            this.labelControl_pass.Text = "пароль";
            // 
            // textEdit_new
            // 
            this.textEdit_new.Location = new System.Drawing.Point(101, 89);
            this.textEdit_new.Name = "textEdit_new";
            this.textEdit_new.Properties.MaxLength = 50;
            this.textEdit_new.Properties.PasswordChar = '*';
            this.textEdit_new.Size = new System.Drawing.Size(188, 20);
            this.textEdit_new.TabIndex = 9;
            this.textEdit_new.Visible = false;
            this.textEdit_new.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit_new_KeyDown);
            // 
            // labelControl_new
            // 
            this.labelControl_new.Location = new System.Drawing.Point(19, 96);
            this.labelControl_new.Name = "labelControl_new";
            this.labelControl_new.Size = new System.Drawing.Size(80, 13);
            this.labelControl_new.TabIndex = 8;
            this.labelControl_new.Text = "еще раз пароль";
            this.labelControl_new.Visible = false;
            // 
            // labelControl_result
            // 
            this.labelControl_result.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl_result.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl_result.Appearance.Options.UseFont = true;
            this.labelControl_result.Appearance.Options.UseForeColor = true;
            this.labelControl_result.Location = new System.Drawing.Point(24, 10);
            this.labelControl_result.Name = "labelControl_result";
            this.labelControl_result.Size = new System.Drawing.Size(236, 13);
            this.labelControl_result.TabIndex = 9;
            this.labelControl_result.Text = "Пароль не верный. Попробуйте еще раз.";
            this.labelControl_result.Visible = false;
            // 
            // simpleButton_save
            // 
            this.simpleButton_save.Location = new System.Drawing.Point(101, 115);
            this.simpleButton_save.Name = "simpleButton_save";
            this.simpleButton_save.Size = new System.Drawing.Size(75, 39);
            this.simpleButton_save.TabIndex = 10;
            this.simpleButton_save.Text = "Сохранить";
            this.simpleButton_save.Visible = false;
            this.simpleButton_save.Click += new System.EventHandler(this.simpleButton_ChangePassword_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 166);
            this.ControlBox = false;
            this.Controls.Add(this.simpleButton_save);
            this.Controls.Add(this.labelControl_result);
            this.Controls.Add(this.labelControl_new);
            this.Controls.Add(this.textEdit_new);
            this.Controls.Add(this.labelControl_pass);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEdit_password);
            this.Controls.Add(this.textEdit_login);
            this.Controls.Add(this.simpleButton_Cancel);
            this.Controls.Add(this.simpleButton_OK);
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Введите логин-пароль";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_login.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_new.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton_OK;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private DevExpress.XtraEditors.TextEdit textEdit_login;
        private DevExpress.XtraEditors.TextEdit textEdit_password;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl_pass;
        private DevExpress.XtraEditors.TextEdit textEdit_new;
        private DevExpress.XtraEditors.LabelControl labelControl_new;
        private DevExpress.XtraEditors.LabelControl labelControl_result;
        private DevExpress.XtraEditors.SimpleButton simpleButton_save;
    }
}