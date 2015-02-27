namespace bies
{
    partial class ImportExcelForm
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
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem_1A = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_report_2a = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_report_1b = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_report_2b = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit_zaim = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_ru = new System.Windows.Forms.RadioButton();
            this.radioButton_en = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_zaim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem_1A,
            this.barButtonItem_report_2a,
            this.barButtonItem_report_1b,
            this.barButtonItem_report_2b});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 12;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar2.FloatLocation = new System.Drawing.Point(97, 297);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_1A, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_report_2a),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_report_1b),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_report_2b)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barButtonItem_1A
            // 
            this.barButtonItem_1A.Border = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.barButtonItem_1A.Caption = "1А - отчет в Excel";
            this.barButtonItem_1A.Id = 7;
            this.barButtonItem_1A.Name = "barButtonItem_1A";
            this.barButtonItem_1A.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_1A_ItemClick);
            // 
            // barButtonItem_report_2a
            // 
            this.barButtonItem_report_2a.Border = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.barButtonItem_report_2a.Caption = "2A отчет в Excel";
            this.barButtonItem_report_2a.Id = 9;
            this.barButtonItem_report_2a.Name = "barButtonItem_report_2a";
            this.barButtonItem_report_2a.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_report_2a_ItemClick);
            // 
            // barButtonItem_report_1b
            // 
            this.barButtonItem_report_1b.Border = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.barButtonItem_report_1b.Caption = "1B отчет в Excel";
            this.barButtonItem_report_1b.Id = 10;
            this.barButtonItem_report_1b.Name = "barButtonItem_report_1b";
            this.barButtonItem_report_1b.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_report_1b_ItemClick);
            // 
            // barButtonItem_report_2b
            // 
            this.barButtonItem_report_2b.Border = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.barButtonItem_report_2b.Caption = "2B отчет в Excel";
            this.barButtonItem_report_2b.Id = 11;
            this.barButtonItem_report_2b.Name = "barButtonItem_report_2b";
            this.barButtonItem_report_2b.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_report_2b_ItemClick);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.Appearance.BackColor = System.Drawing.Color.LightGreen;
            this.repositoryItemComboBox1.Appearance.Options.UseBackColor = true;
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.Appearance.BackColor = System.Drawing.Color.LightPink;
            this.repositoryItemComboBox2.Appearance.Options.UseBackColor = true;
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Займ";
            // 
            // comboBoxEdit_zaim
            // 
            this.comboBoxEdit_zaim.Location = new System.Drawing.Point(87, 12);
            this.comboBoxEdit_zaim.Name = "comboBoxEdit_zaim";
            this.comboBoxEdit_zaim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit_zaim.Size = new System.Drawing.Size(400, 20);
            this.comboBoxEdit_zaim.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 48);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "за период с";
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(87, 45);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(117, 20);
            this.dateEditFrom.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(69, 95);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(12, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "по";
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(87, 88);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(117, 20);
            this.dateEditTo.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_ru);
            this.groupBox1.Controls.Add(this.radioButton_en);
            this.groupBox1.Location = new System.Drawing.Point(234, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 48);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "язык";
            // 
            // radioButton_ru
            // 
            this.radioButton_ru.AutoSize = true;
            this.radioButton_ru.Location = new System.Drawing.Point(96, 19);
            this.radioButton_ru.Name = "radioButton_ru";
            this.radioButton_ru.Size = new System.Drawing.Size(66, 17);
            this.radioButton_ru.TabIndex = 1;
            this.radioButton_ru.TabStop = true;
            this.radioButton_ru.Text = "русский";
            this.radioButton_ru.UseVisualStyleBackColor = true;
            // 
            // radioButton_en
            // 
            this.radioButton_en.AutoSize = true;
            this.radioButton_en.Checked = true;
            this.radioButton_en.Location = new System.Drawing.Point(6, 19);
            this.radioButton_en.Name = "radioButton_en";
            this.radioButton_en.Size = new System.Drawing.Size(84, 17);
            this.radioButton_en.TabIndex = 0;
            this.radioButton_en.TabStop = true;
            this.radioButton_en.Text = "английский";
            this.radioButton_en.UseVisualStyleBackColor = true;
            // 
            // ImportExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 151);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dateEditTo);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.dateEditFrom);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.comboBoxEdit_zaim);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportExcelForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчеты";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_zaim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_1A;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit_zaim;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_report_2a;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_report_1b;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_report_2b;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_en;
        private System.Windows.Forms.RadioButton radioButton_ru;
    }
}