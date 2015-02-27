namespace bies
{
    partial class FormProjectNameCatalog
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
            this.gridControl_projectname = new DevExpress.XtraGrid.GridControl();
            this.gridView_projectname = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton_Save = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Add = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Edit = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.simpleButton_Del = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_edit = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_projectname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_projectname)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_edit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControl_projectname);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 288);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "проекты";
            // 
            // gridControl_projectname
            // 
            this.gridControl_projectname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_projectname.EmbeddedNavigator.Name = "";
            this.gridControl_projectname.Location = new System.Drawing.Point(3, 16);
            this.gridControl_projectname.MainView = this.gridView_projectname;
            this.gridControl_projectname.Name = "gridControl_projectname";
            this.gridControl_projectname.Size = new System.Drawing.Size(523, 269);
            this.gridControl_projectname.TabIndex = 0;
            this.gridControl_projectname.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_projectname});
            // 
            // gridView_projectname
            // 
            this.gridView_projectname.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView_projectname.GridControl = this.gridControl_projectname;
            this.gridView_projectname.Name = "gridView_projectname";
            this.gridView_projectname.OptionsBehavior.Editable = false;
            this.gridView_projectname.OptionsView.ShowGroupPanel = false;
            this.gridView_projectname.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_projectname_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "projectnameID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Название проекта";
            this.gridColumn2.FieldName = "projectname";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // simpleButton_Save
            // 
            this.simpleButton_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_Save.Location = new System.Drawing.Point(15, 321);
            this.simpleButton_Save.Name = "simpleButton_Save";
            this.simpleButton_Save.Size = new System.Drawing.Size(166, 100);
            this.simpleButton_Save.TabIndex = 1;
            this.simpleButton_Save.Text = "Закрыть справочник";
            // 
            // simpleButton_Add
            // 
            this.simpleButton_Add.Location = new System.Drawing.Point(10, 50);
            this.simpleButton_Add.Name = "simpleButton_Add";
            this.simpleButton_Add.Size = new System.Drawing.Size(107, 44);
            this.simpleButton_Add.TabIndex = 3;
            this.simpleButton_Add.Text = "Добавить";
            this.simpleButton_Add.Click += new System.EventHandler(this.simpleButton_Add_Click);
            // 
            // simpleButton_Edit
            // 
            this.simpleButton_Edit.Location = new System.Drawing.Point(123, 50);
            this.simpleButton_Edit.Name = "simpleButton_Edit";
            this.simpleButton_Edit.Size = new System.Drawing.Size(109, 44);
            this.simpleButton_Edit.TabIndex = 4;
            this.simpleButton_Edit.Text = "Редактировать";
            this.simpleButton_Edit.Click += new System.EventHandler(this.simpleButton_Edit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.simpleButton_Del);
            this.groupBox2.Controls.Add(this.textEdit_edit);
            this.groupBox2.Controls.Add(this.simpleButton_Add);
            this.groupBox2.Controls.Add(this.simpleButton_Edit);
            this.groupBox2.Location = new System.Drawing.Point(187, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "редактирование названий проектов";
            // 
            // simpleButton_Del
            // 
            this.simpleButton_Del.Location = new System.Drawing.Point(238, 50);
            this.simpleButton_Del.Name = "simpleButton_Del";
            this.simpleButton_Del.Size = new System.Drawing.Size(109, 44);
            this.simpleButton_Del.TabIndex = 6;
            this.simpleButton_Del.Text = "Удалить";
            this.simpleButton_Del.Click += new System.EventHandler(this.simpleButton_Del_Click);
            // 
            // textEdit_edit
            // 
            this.textEdit_edit.Location = new System.Drawing.Point(7, 20);
            this.textEdit_edit.Name = "textEdit_edit";
            this.textEdit_edit.Size = new System.Drawing.Size(344, 20);
            this.textEdit_edit.TabIndex = 5;
            // 
            // FormProjectNameCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 430);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.simpleButton_Save);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormProjectNameCatalog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник проектов";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_projectname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_projectname)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_edit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Save;
        private DevExpress.XtraGrid.GridControl gridControl_projectname;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_projectname;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Add;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Edit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit textEdit_edit;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Del;
    }
}