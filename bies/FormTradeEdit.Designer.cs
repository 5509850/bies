namespace bies
{
    partial class FormTradeEdit
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
            this.TradeName = new DevExpress.XtraEditors.TextEdit();
            this.Number = new DevExpress.XtraEditors.TextEdit();
            this.UserResponse = new DevExpress.XtraEditors.TextEdit();
            this.ProgectName = new DevExpress.XtraEditors.TextEdit();
            this.DateBegin = new DevExpress.XtraEditors.DateEdit();
            this.radioButton_tradetype1 = new System.Windows.Forms.RadioButton();
            this.radioButton_tradetype2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxEdit_ProjectName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton_save = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_catalogProjects = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEdit_userResponce = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.TradeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserResponse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProgectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateBegin.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateBegin.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_ProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_userResponce.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // TradeName
            // 
            this.TradeName.Location = new System.Drawing.Point(25, 78);
            this.TradeName.Name = "TradeName";
            this.TradeName.Properties.MaxLength = 200;
            this.TradeName.Size = new System.Drawing.Size(409, 20);
            this.TradeName.TabIndex = 1;
            // 
            // Number
            // 
            this.Number.Location = new System.Drawing.Point(25, 35);
            this.Number.Name = "Number";
            this.Number.Properties.MaxLength = 50;
            this.Number.Size = new System.Drawing.Size(409, 20);
            this.Number.TabIndex = 0;
            // 
            // UserResponse
            // 
            this.UserResponse.Enabled = false;
            this.UserResponse.Location = new System.Drawing.Point(25, 345);
            this.UserResponse.Name = "UserResponse";
            this.UserResponse.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.UserResponse.Properties.Appearance.Options.UseFont = true;
            this.UserResponse.Size = new System.Drawing.Size(409, 20);
            this.UserResponse.TabIndex = 5;
            // 
            // ProgectName
            // 
            this.ProgectName.Location = new System.Drawing.Point(173, 140);
            this.ProgectName.Name = "ProgectName";
            this.ProgectName.Properties.MaxLength = 200;
            this.ProgectName.Size = new System.Drawing.Size(261, 20);
            this.ProgectName.TabIndex = 2;
            this.ProgectName.Visible = false;
            // 
            // DateBegin
            // 
            this.DateBegin.EditValue = new System.DateTime(2011, 2, 16, 16, 46, 8, 750);
            this.DateBegin.Location = new System.Drawing.Point(25, 306);
            this.DateBegin.Name = "DateBegin";
            this.DateBegin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DateBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateBegin.Properties.NullDate = new System.DateTime(2011, 2, 16, 17, 56, 29, 156);
            this.DateBegin.Properties.ShowWeekNumbers = true;
            this.DateBegin.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateBegin.Size = new System.Drawing.Size(100, 20);
            this.DateBegin.TabIndex = 4;
            // 
            // radioButton_tradetype1
            // 
            this.radioButton_tradetype1.AutoSize = true;
            this.radioButton_tradetype1.Checked = true;
            this.radioButton_tradetype1.Location = new System.Drawing.Point(14, 31);
            this.radioButton_tradetype1.Name = "radioButton_tradetype1";
            this.radioButton_tradetype1.Size = new System.Drawing.Size(138, 17);
            this.radioButton_tradetype1.TabIndex = 3;
            this.radioButton_tradetype1.TabStop = true;
            this.radioButton_tradetype1.Text = "с предквалификацией";
            this.radioButton_tradetype1.UseVisualStyleBackColor = true;
            // 
            // radioButton_tradetype2
            // 
            this.radioButton_tradetype2.AutoSize = true;
            this.radioButton_tradetype2.Location = new System.Drawing.Point(14, 57);
            this.radioButton_tradetype2.Name = "radioButton_tradetype2";
            this.radioButton_tradetype2.Size = new System.Drawing.Size(144, 17);
            this.radioButton_tradetype2.TabIndex = 8;
            this.radioButton_tradetype2.Text = "без предквалификации";
            this.radioButton_tradetype2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_tradetype1);
            this.groupBox1.Controls.Add(this.radioButton_tradetype2);
            this.groupBox1.Location = new System.Drawing.Point(25, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 95);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "тип торгов";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "№ торгов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "название торгов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "название проекта";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 329);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "ответственный";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "дата начала торгов";
            // 
            // comboBoxEdit_ProjectName
            // 
            this.comboBoxEdit_ProjectName.Location = new System.Drawing.Point(131, 107);
            this.comboBoxEdit_ProjectName.Name = "comboBoxEdit_ProjectName";
            this.comboBoxEdit_ProjectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit_ProjectName.Size = new System.Drawing.Size(303, 20);
            this.comboBoxEdit_ProjectName.TabIndex = 14;
            this.comboBoxEdit_ProjectName.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit_ProjectName_SelectedIndexChanged);
            // 
            // simpleButton_save
            // 
            this.simpleButton_save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_save.Location = new System.Drawing.Point(25, 371);
            this.simpleButton_save.Name = "simpleButton_save";
            this.simpleButton_save.Size = new System.Drawing.Size(138, 35);
            this.simpleButton_save.TabIndex = 15;
            this.simpleButton_save.Text = "Сохранить";
            this.simpleButton_save.Click += new System.EventHandler(this.simpleButton_save_Click);
            // 
            // simpleButton_cancel
            // 
            this.simpleButton_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_cancel.Location = new System.Drawing.Point(296, 371);
            this.simpleButton_cancel.Name = "simpleButton_cancel";
            this.simpleButton_cancel.Size = new System.Drawing.Size(138, 35);
            this.simpleButton_cancel.TabIndex = 16;
            this.simpleButton_cancel.Text = "Отмена";
            this.simpleButton_cancel.Click += new System.EventHandler(this.simpleButton_cancel_Click);
            // 
            // simpleButton_catalogProjects
            // 
            this.simpleButton_catalogProjects.Location = new System.Drawing.Point(25, 137);
            this.simpleButton_catalogProjects.Name = "simpleButton_catalogProjects";
            this.simpleButton_catalogProjects.Size = new System.Drawing.Size(138, 23);
            this.simpleButton_catalogProjects.TabIndex = 17;
            this.simpleButton_catalogProjects.Text = "справочник проектов";
            this.simpleButton_catalogProjects.Click += new System.EventHandler(this.simpleButton_catalogProjects_Click);
            // 
            // comboBoxEdit_userResponce
            // 
            this.comboBoxEdit_userResponce.Location = new System.Drawing.Point(140, 322);
            this.comboBoxEdit_userResponce.Name = "comboBoxEdit_userResponce";
            this.comboBoxEdit_userResponce.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit_userResponce.Size = new System.Drawing.Size(294, 20);
            this.comboBoxEdit_userResponce.TabIndex = 18;
            this.comboBoxEdit_userResponce.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit_userResponce_SelectedIndexChanged);
            // 
            // FormTradeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 418);
            this.ControlBox = false;
            this.Controls.Add(this.comboBoxEdit_userResponce);
            this.Controls.Add(this.simpleButton_catalogProjects);
            this.Controls.Add(this.simpleButton_cancel);
            this.Controls.Add(this.simpleButton_save);
            this.Controls.Add(this.comboBoxEdit_ProjectName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DateBegin);
            this.Controls.Add(this.ProgectName);
            this.Controls.Add(this.UserResponse);
            this.Controls.Add(this.Number);
            this.Controls.Add(this.TradeName);
            this.MaximumSize = new System.Drawing.Size(466, 452);
            this.Name = "FormTradeEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormTradeEdit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTradeEdit_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.TradeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserResponse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProgectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateBegin.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateBegin.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_ProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_userResponce.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit TradeName;
        private DevExpress.XtraEditors.TextEdit Number;
        private DevExpress.XtraEditors.TextEdit UserResponse;
        private DevExpress.XtraEditors.TextEdit ProgectName;
        private DevExpress.XtraEditors.DateEdit DateBegin;
        private System.Windows.Forms.RadioButton radioButton_tradetype1;
        private System.Windows.Forms.RadioButton radioButton_tradetype2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit_ProjectName;
        private DevExpress.XtraEditors.SimpleButton simpleButton_save;
        private DevExpress.XtraEditors.SimpleButton simpleButton_cancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton_catalogProjects;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit_userResponce;
    }
}