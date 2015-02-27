namespace bies
{
    partial class FormAddCurrency
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
            this.simpleButton_Save = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit_date = new DevExpress.XtraEditors.DateEdit();
            this.textEdit_RUB = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_EUR = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_USD = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_date.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_RUB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_EUR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_USD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton_Save
            // 
            this.simpleButton_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_Save.Location = new System.Drawing.Point(13, 212);
            this.simpleButton_Save.Name = "simpleButton_Save";
            this.simpleButton_Save.Size = new System.Drawing.Size(121, 42);
            this.simpleButton_Save.TabIndex = 0;
            this.simpleButton_Save.Text = "Сохранить";
            this.simpleButton_Save.Click += new System.EventHandler(this.simpleButton_Save_Click);
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_Cancel.Location = new System.Drawing.Point(162, 212);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(121, 42);
            this.simpleButton_Cancel.TabIndex = 1;
            this.simpleButton_Cancel.Text = "Отмена";
            this.simpleButton_Cancel.Click += new System.EventHandler(this.simpleButton_Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.dateEdit_date);
            this.groupBox1.Controls.Add(this.textEdit_RUB);
            this.groupBox1.Controls.Add(this.textEdit_EUR);
            this.groupBox1.Controls.Add(this.textEdit_USD);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 171);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "курсы валют в бел.руб.";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(220, 116);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(41, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "RUB       ";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(220, 90);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "EUR       ";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(220, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(20, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "USD";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(220, 148);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "на дату";
            // 
            // dateEdit_date
            // 
            this.dateEdit_date.EditValue = null;
            this.dateEdit_date.Location = new System.Drawing.Point(104, 145);
            this.dateEdit_date.Name = "dateEdit_date";
            this.dateEdit_date.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEdit_date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit_date.Properties.NullDate = new System.DateTime(2011, 4, 6, 15, 46, 44, 593);
            this.dateEdit_date.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_date.Size = new System.Drawing.Size(100, 20);
            this.dateEdit_date.TabIndex = 4;
            // 
            // textEdit_RUB
            // 
            this.textEdit_RUB.EditValue = "110";
            this.textEdit_RUB.Location = new System.Drawing.Point(104, 113);
            this.textEdit_RUB.Name = "textEdit_RUB";
            this.textEdit_RUB.Properties.DisplayFormat.FormatString = "N2";
            this.textEdit_RUB.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEdit_RUB.Properties.Mask.EditMask = "N2";
            this.textEdit_RUB.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit_RUB.Size = new System.Drawing.Size(100, 20);
            this.textEdit_RUB.TabIndex = 3;
            this.textEdit_RUB.EditValueChanged += new System.EventHandler(this.textEdit_RUB_EditValueChanged);
            // 
            // textEdit_EUR
            // 
            this.textEdit_EUR.EditValue = "4500";
            this.textEdit_EUR.Location = new System.Drawing.Point(104, 87);
            this.textEdit_EUR.Name = "textEdit_EUR";
            this.textEdit_EUR.Properties.DisplayFormat.FormatString = "N2";
            this.textEdit_EUR.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEdit_EUR.Properties.Mask.EditMask = "N2";
            this.textEdit_EUR.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit_EUR.Size = new System.Drawing.Size(100, 20);
            this.textEdit_EUR.TabIndex = 2;
            this.textEdit_EUR.EditValueChanged += new System.EventHandler(this.textEdit_EUR_EditValueChanged);
            // 
            // textEdit_USD
            // 
            this.textEdit_USD.EditValue = "3100";
            this.textEdit_USD.Location = new System.Drawing.Point(104, 59);
            this.textEdit_USD.Name = "textEdit_USD";
            this.textEdit_USD.Properties.DisplayFormat.FormatString = "N2";
            this.textEdit_USD.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textEdit_USD.Properties.Mask.EditMask = "N2";
            this.textEdit_USD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit_USD.Size = new System.Drawing.Size(100, 20);
            this.textEdit_USD.TabIndex = 1;
            this.textEdit_USD.EditValueChanged += new System.EventHandler(this.textEdit_USD_EditValueChanged);
            // 
            // FormAddCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 266);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simpleButton_Cancel);
            this.Controls.Add(this.simpleButton_Save);
            this.Name = "FormAddCurrency";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление новых курсов валют";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddCurrency_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_date.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_RUB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_EUR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_USD.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton_Save;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit textEdit_RUB;
        private DevExpress.XtraEditors.TextEdit textEdit_EUR;
        private DevExpress.XtraEditors.TextEdit textEdit_USD;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEdit_date;
    }
}