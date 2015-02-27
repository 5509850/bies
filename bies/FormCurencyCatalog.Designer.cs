namespace bies
{
    partial class FormCurencyCatalog
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
            this.gridControl_curency = new DevExpress.XtraGrid.GridControl();
            this.gridView_curency = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton_Save = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Add = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Edit = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_curency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_curency)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControl_curency);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 288);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "справочник курсов валют";
            // 
            // gridControl_curency
            // 
            this.gridControl_curency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_curency.EmbeddedNavigator.Name = "";
            this.gridControl_curency.Location = new System.Drawing.Point(3, 16);
            this.gridControl_curency.MainView = this.gridView_curency;
            this.gridControl_curency.Name = "gridControl_curency";
            this.gridControl_curency.Size = new System.Drawing.Size(633, 269);
            this.gridControl_curency.TabIndex = 0;
            this.gridControl_curency.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_curency});
            // 
            // gridView_curency
            // 
            this.gridView_curency.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridView_curency.GridControl = this.gridControl_curency;
            this.gridView_curency.Name = "gridView_curency";
            this.gridView_curency.OptionsBehavior.Editable = false;
            this.gridView_curency.OptionsView.ShowAutoFilterRow = true;
            this.gridView_curency.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "currencyRateID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "курс";
            this.gridColumn2.FieldName = "rate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "на дату";
            this.gridColumn3.FieldName = "date";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.FieldName = "currencyID";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "валюта";
            this.gridColumn5.FieldName = "shortname";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "название";
            this.gridColumn6.FieldName = "name";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "код валюты";
            this.gridColumn7.FieldName = "code";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // simpleButton_Save
            // 
            this.simpleButton_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_Save.Location = new System.Drawing.Point(15, 332);
            this.simpleButton_Save.Name = "simpleButton_Save";
            this.simpleButton_Save.Size = new System.Drawing.Size(90, 44);
            this.simpleButton_Save.TabIndex = 1;
            this.simpleButton_Save.Text = "OK";
            // 
            // simpleButton_Add
            // 
            this.simpleButton_Add.Location = new System.Drawing.Point(338, 332);
            this.simpleButton_Add.Name = "simpleButton_Add";
            this.simpleButton_Add.Size = new System.Drawing.Size(135, 44);
            this.simpleButton_Add.TabIndex = 3;
            this.simpleButton_Add.Text = "Добавить курсы валют";
            this.simpleButton_Add.Click += new System.EventHandler(this.simpleButton_Add_Click);
            // 
            // simpleButton_Edit
            // 
            this.simpleButton_Edit.Location = new System.Drawing.Point(488, 332);
            this.simpleButton_Edit.Name = "simpleButton_Edit";
            this.simpleButton_Edit.Size = new System.Drawing.Size(160, 44);
            this.simpleButton_Edit.TabIndex = 4;
            this.simpleButton_Edit.Text = "Редактировать курс валют";
            this.simpleButton_Edit.Click += new System.EventHandler(this.simpleButton_Edit_Click);
            // 
            // FormCurencyCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 388);
            this.Controls.Add(this.simpleButton_Edit);
            this.Controls.Add(this.simpleButton_Add);
            this.Controls.Add(this.simpleButton_Save);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormCurencyCatalog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник курсов валют";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_curency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_curency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Save;
        private DevExpress.XtraGrid.GridControl gridControl_curency;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_curency;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Add;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Edit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
    }
}