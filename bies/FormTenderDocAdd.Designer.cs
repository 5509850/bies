namespace bies
{
    partial class FormTenderDocAdd
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
            this.simpleButton_save = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit_DateCreate = new DevExpress.XtraEditors.DateEdit();
            this.checkEdit_sendToBank = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit_trade = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkEdit_isBaseOrShared = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit_signed = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit_las = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit_fileDateCreate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_FileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit_title = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton_doc = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_sendToBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_trade.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_isBaseOrShared.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_signed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_las.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_las.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_fileDateCreate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_fileDateCreate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_FileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_title.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton_save
            // 
            this.simpleButton_save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_save.Location = new System.Drawing.Point(12, 324);
            this.simpleButton_save.Name = "simpleButton_save";
            this.simpleButton_save.Size = new System.Drawing.Size(102, 37);
            this.simpleButton_save.TabIndex = 0;
            this.simpleButton_save.Text = "Сохранить";
            this.simpleButton_save.Click += new System.EventHandler(this.simpleButton_save_Click);
            // 
            // simpleButton_cancel
            // 
            this.simpleButton_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_cancel.Location = new System.Drawing.Point(235, 324);
            this.simpleButton_cancel.Name = "simpleButton_cancel";
            this.simpleButton_cancel.Size = new System.Drawing.Size(102, 37);
            this.simpleButton_cancel.TabIndex = 1;
            this.simpleButton_cancel.Text = "Отмена";
            this.simpleButton_cancel.Click += new System.EventHandler(this.simpleButton_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.dateEdit_DateCreate);
            this.groupBox1.Controls.Add(this.checkEdit_sendToBank);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.comboBoxEdit_trade);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 91);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "конкурсная документация";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(293, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "дата";
            // 
            // dateEdit_DateCreate
            // 
            this.dateEdit_DateCreate.EditValue = null;
            this.dateEdit_DateCreate.Location = new System.Drawing.Point(135, 56);
            this.dateEdit_DateCreate.Name = "dateEdit_DateCreate";
            this.dateEdit_DateCreate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEdit_DateCreate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_DateCreate.Properties.NullDate = new System.DateTime(2011, 2, 25, 13, 6, 42, 421);
            this.dateEdit_DateCreate.Properties.ShowWeekNumbers = true;
            this.dateEdit_DateCreate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_DateCreate.Size = new System.Drawing.Size(152, 20);
            this.dateEdit_DateCreate.TabIndex = 3;
            // 
            // checkEdit_sendToBank
            // 
            this.checkEdit_sendToBank.Location = new System.Drawing.Point(6, 56);
            this.checkEdit_sendToBank.Name = "checkEdit_sendToBank";
            this.checkEdit_sendToBank.Properties.Caption = "отправлено в банк";
            this.checkEdit_sendToBank.Size = new System.Drawing.Size(313, 19);
            this.checkEdit_sendToBank.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Торги";
            // 
            // comboBoxEdit_trade
            // 
            this.comboBoxEdit_trade.Location = new System.Drawing.Point(6, 30);
            this.comboBoxEdit_trade.Name = "comboBoxEdit_trade";
            this.comboBoxEdit_trade.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit_trade.Size = new System.Drawing.Size(313, 20);
            this.comboBoxEdit_trade.TabIndex = 0;
            this.comboBoxEdit_trade.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit_trade_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkEdit_isBaseOrShared);
            this.groupBox2.Controls.Add(this.checkEdit_signed);
            this.groupBox2.Controls.Add(this.labelControl6);
            this.groupBox2.Controls.Add(this.dateEdit_las);
            this.groupBox2.Controls.Add(this.dateEdit_fileDateCreate);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.labelControl4);
            this.groupBox2.Controls.Add(this.textEdit_FileName);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Controls.Add(this.textEdit_title);
            this.groupBox2.Controls.Add(this.simpleButton_doc);
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 218);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "посмотреть документ";
            // 
            // checkEdit_isBaseOrShared
            // 
            this.checkEdit_isBaseOrShared.Enabled = false;
            this.checkEdit_isBaseOrShared.Location = new System.Drawing.Point(122, 112);
            this.checkEdit_isBaseOrShared.Name = "checkEdit_isBaseOrShared";
            this.checkEdit_isBaseOrShared.Properties.Caption = "резервная копия в базе";
            this.checkEdit_isBaseOrShared.Size = new System.Drawing.Size(196, 19);
            this.checkEdit_isBaseOrShared.TabIndex = 10;
            // 
            // checkEdit_signed
            // 
            this.checkEdit_signed.Location = new System.Drawing.Point(9, 193);
            this.checkEdit_signed.Name = "checkEdit_signed";
            this.checkEdit_signed.Properties.Caption = "подписан";
            this.checkEdit_signed.Size = new System.Drawing.Size(75, 19);
            this.checkEdit_signed.TabIndex = 9;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(223, 66);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(53, 13);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "изменения";
            // 
            // dateEdit_las
            // 
            this.dateEdit_las.EditValue = null;
            this.dateEdit_las.Enabled = false;
            this.dateEdit_las.Location = new System.Drawing.Point(223, 85);
            this.dateEdit_las.Name = "dateEdit_las";
            this.dateEdit_las.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_las.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_las.Size = new System.Drawing.Size(100, 20);
            this.dateEdit_las.TabIndex = 7;
            // 
            // dateEdit_fileDateCreate
            // 
            this.dateEdit_fileDateCreate.EditValue = null;
            this.dateEdit_fileDateCreate.Enabled = false;
            this.dateEdit_fileDateCreate.Location = new System.Drawing.Point(121, 85);
            this.dateEdit_fileDateCreate.Name = "dateEdit_fileDateCreate";
            this.dateEdit_fileDateCreate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_fileDateCreate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_fileDateCreate.Size = new System.Drawing.Size(99, 20);
            this.dateEdit_fileDateCreate.TabIndex = 6;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(122, 65);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(75, 13);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "дата создания";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(122, 19);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "название файла";
            // 
            // textEdit_FileName
            // 
            this.textEdit_FileName.Enabled = false;
            this.textEdit_FileName.Location = new System.Drawing.Point(120, 38);
            this.textEdit_FileName.Name = "textEdit_FileName";
            this.textEdit_FileName.Size = new System.Drawing.Size(198, 20);
            this.textEdit_FileName.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 138);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(105, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "название документа";
            // 
            // textEdit_title
            // 
            this.textEdit_title.Location = new System.Drawing.Point(6, 157);
            this.textEdit_title.Name = "textEdit_title";
            this.textEdit_title.Properties.MaxLength = 100;
            this.textEdit_title.Size = new System.Drawing.Size(312, 20);
            this.textEdit_title.TabIndex = 1;
            // 
            // simpleButton_doc
            // 
            this.simpleButton_doc.Image = global::bies.Properties.Resources.word;
            this.simpleButton_doc.Location = new System.Drawing.Point(6, 19);
            this.simpleButton_doc.Name = "simpleButton_doc";
            this.simpleButton_doc.Size = new System.Drawing.Size(108, 113);
            this.simpleButton_doc.TabIndex = 0;
            this.simpleButton_doc.Text = "simpleButton1";
            this.simpleButton_doc.Click += new System.EventHandler(this.simpleButton_doc_Click);
            // 
            // FormTenderDocAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 368);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButton_cancel);
            this.Controls.Add(this.simpleButton_save);
            this.Name = "FormTenderDocAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление документа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTenderDocAdd_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DateCreate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_sendToBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_trade.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_isBaseOrShared.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit_signed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_las.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_las.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_fileDateCreate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_fileDateCreate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_FileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_title.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton_save;
        private DevExpress.XtraEditors.SimpleButton simpleButton_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton simpleButton_doc;
        private DevExpress.XtraEditors.DateEdit dateEdit_DateCreate;
        private DevExpress.XtraEditors.CheckEdit checkEdit_sendToBank;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit_trade;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit_title;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit textEdit_FileName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit dateEdit_las;
        private DevExpress.XtraEditors.DateEdit dateEdit_fileDateCreate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit checkEdit_signed;
        private DevExpress.XtraEditors.CheckEdit checkEdit_isBaseOrShared;
    }
}