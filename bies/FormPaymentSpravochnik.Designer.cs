namespace bies
{
    partial class FormPaymentSpravochnik
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridControl_base = new DevExpress.XtraGrid.GridControl();
            this.gridView_base = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.simpleButton_delete = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_edit = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_edit = new DevExpress.XtraEditors.TextEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.simpleButton_Add = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_add = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton_OK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEdit_work_oblast = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl_work_oblast = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_base)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_base)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_edit.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_add.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_work_oblast.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControl_base);
            this.groupBox1.Location = new System.Drawing.Point(18, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(733, 191);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "справочник";
            // 
            // gridControl_base
            // 
            this.gridControl_base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_base.EmbeddedNavigator.Name = "";
            this.gridControl_base.Location = new System.Drawing.Point(3, 16);
            this.gridControl_base.MainView = this.gridView_base;
            this.gridControl_base.Name = "gridControl_base";
            this.gridControl_base.Size = new System.Drawing.Size(727, 172);
            this.gridControl_base.TabIndex = 0;
            this.gridControl_base.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_base});
            // 
            // gridView_base
            // 
            this.gridView_base.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView_base.GridControl = this.gridControl_base;
            this.gridView_base.Name = "gridView_base";
            this.gridView_base.OptionsBehavior.Editable = false;
            this.gridView_base.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridView_base.OptionsSelection.InvertSelection = true;
            this.gridView_base.OptionsView.ShowGroupPanel = false;
            this.gridView_base.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_base_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "название";
            this.gridColumn2.FieldName = "name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 593;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Активно/удалено";
            this.gridColumn3.FieldName = "isActive";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 113;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.FieldName = "pCategoryworkID";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "gridColumn5";
            this.gridColumn5.FieldName = "pOblastID";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.simpleButton_delete);
            this.groupBox2.Controls.Add(this.simpleButton_edit);
            this.groupBox2.Controls.Add(this.textEdit_edit);
            this.groupBox2.Location = new System.Drawing.Point(24, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Редактирование";
            // 
            // simpleButton_delete
            // 
            this.simpleButton_delete.Location = new System.Drawing.Point(198, 56);
            this.simpleButton_delete.Name = "simpleButton_delete";
            this.simpleButton_delete.Size = new System.Drawing.Size(130, 23);
            this.simpleButton_delete.TabIndex = 2;
            this.simpleButton_delete.Text = "удалить/восстановить";
            this.simpleButton_delete.Click += new System.EventHandler(this.simpleButton_delete_Click);
            // 
            // simpleButton_edit
            // 
            this.simpleButton_edit.Location = new System.Drawing.Point(6, 56);
            this.simpleButton_edit.Name = "simpleButton_edit";
            this.simpleButton_edit.Size = new System.Drawing.Size(116, 23);
            this.simpleButton_edit.TabIndex = 1;
            this.simpleButton_edit.Text = "сохранить";
            this.simpleButton_edit.Click += new System.EventHandler(this.simpleButton_edit_Click);
            // 
            // textEdit_edit
            // 
            this.textEdit_edit.Location = new System.Drawing.Point(6, 19);
            this.textEdit_edit.Name = "textEdit_edit";
            this.textEdit_edit.Properties.MaxLength = 200;
            this.textEdit_edit.Size = new System.Drawing.Size(322, 20);
            this.textEdit_edit.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.simpleButton_Add);
            this.groupBox3.Controls.Add(this.textEdit_add);
            this.groupBox3.Location = new System.Drawing.Point(396, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(361, 85);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Добавить";
            // 
            // simpleButton_Add
            // 
            this.simpleButton_Add.Location = new System.Drawing.Point(6, 56);
            this.simpleButton_Add.Name = "simpleButton_Add";
            this.simpleButton_Add.Size = new System.Drawing.Size(349, 22);
            this.simpleButton_Add.TabIndex = 2;
            this.simpleButton_Add.Text = "Добавить новую запись";
            this.simpleButton_Add.Click += new System.EventHandler(this.simpleButton_Add_Click);
            // 
            // textEdit_add
            // 
            this.textEdit_add.Location = new System.Drawing.Point(6, 19);
            this.textEdit_add.Name = "textEdit_add";
            this.textEdit_add.Properties.MaxLength = 200;
            this.textEdit_add.Size = new System.Drawing.Size(349, 20);
            this.textEdit_add.TabIndex = 1;
            // 
            // simpleButton_OK
            // 
            this.simpleButton_OK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton_OK.Appearance.Options.UseFont = true;
            this.simpleButton_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_OK.Location = new System.Drawing.Point(24, 331);
            this.simpleButton_OK.Name = "simpleButton_OK";
            this.simpleButton_OK.Size = new System.Drawing.Size(347, 47);
            this.simpleButton_OK.TabIndex = 3;
            this.simpleButton_OK.Text = "OK";
            // 
            // simpleButton_Cancel
            // 
            this.simpleButton_Cancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton_Cancel.Appearance.Options.UseFont = true;
            this.simpleButton_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton_Cancel.Location = new System.Drawing.Point(396, 331);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new System.Drawing.Size(361, 47);
            this.simpleButton_Cancel.TabIndex = 4;
            this.simpleButton_Cancel.Text = "Отмена";
            // 
            // comboBoxEdit_work_oblast
            // 
            this.comboBoxEdit_work_oblast.Location = new System.Drawing.Point(129, 12);
            this.comboBoxEdit_work_oblast.Name = "comboBoxEdit_work_oblast";
            this.comboBoxEdit_work_oblast.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit_work_oblast.Size = new System.Drawing.Size(622, 20);
            this.comboBoxEdit_work_oblast.TabIndex = 5;
            // 
            // labelControl_work_oblast
            // 
            this.labelControl_work_oblast.Location = new System.Drawing.Point(24, 19);
            this.labelControl_work_oblast.Name = "labelControl_work_oblast";
            this.labelControl_work_oblast.Size = new System.Drawing.Size(6, 13);
            this.labelControl_work_oblast.TabIndex = 6;
            this.labelControl_work_oblast.Text = "*";
            // 
            // FormPaymentSpravochnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 390);
            this.Controls.Add(this.labelControl_work_oblast);
            this.Controls.Add(this.comboBoxEdit_work_oblast);
            this.Controls.Add(this.simpleButton_Cancel);
            this.Controls.Add(this.simpleButton_OK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormPaymentSpravochnik";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormPaymentSpravochnik";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_base)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_base)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_edit.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_add.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit_work_oblast.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton simpleButton_OK;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Cancel;
        private DevExpress.XtraGrid.GridControl gridControl_base;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_base;
        private DevExpress.XtraEditors.SimpleButton simpleButton_edit;
        private DevExpress.XtraEditors.TextEdit textEdit_edit;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Add;
        private DevExpress.XtraEditors.TextEdit textEdit_add;
        private DevExpress.XtraEditors.SimpleButton simpleButton_delete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit_work_oblast;
        private DevExpress.XtraEditors.LabelControl labelControl_work_oblast;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}