namespace bies
{
    partial class FormTemplateDoc
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
            this.gridControl_templateDoc = new DevExpress.XtraGrid.GridControl();
            this.gridView_templateDoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton_Save = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Add = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_doc = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Del = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_templateDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_templateDoc)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControl_templateDoc);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 288);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "шаблоны документов";
            // 
            // gridControl_templateDoc
            // 
            this.gridControl_templateDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_templateDoc.EmbeddedNavigator.Name = "";
            this.gridControl_templateDoc.Location = new System.Drawing.Point(3, 16);
            this.gridControl_templateDoc.MainView = this.gridView_templateDoc;
            this.gridControl_templateDoc.Name = "gridControl_templateDoc";
            this.gridControl_templateDoc.Size = new System.Drawing.Size(633, 269);
            this.gridControl_templateDoc.TabIndex = 0;
            this.gridControl_templateDoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_templateDoc});
            // 
            // gridView_templateDoc
            // 
            this.gridView_templateDoc.GridControl = this.gridControl_templateDoc;
            this.gridView_templateDoc.Name = "gridView_templateDoc";
            this.gridView_templateDoc.OptionsBehavior.Editable = false;
            this.gridView_templateDoc.OptionsView.ShowGroupPanel = false;
            this.gridView_templateDoc.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_projectname_FocusedRowChanged);
            this.gridView_templateDoc.DoubleClick += new System.EventHandler(this.gridView_templateDoc_DoubleClick);
            // 
            // simpleButton_Save
            // 
            this.simpleButton_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton_Save.Location = new System.Drawing.Point(15, 306);
            this.simpleButton_Save.Name = "simpleButton_Save";
            this.simpleButton_Save.Size = new System.Drawing.Size(166, 109);
            this.simpleButton_Save.TabIndex = 1;
            this.simpleButton_Save.Text = "Закрыть справочник";
            // 
            // simpleButton_Add
            // 
            this.simpleButton_Add.Location = new System.Drawing.Point(301, 306);
            this.simpleButton_Add.Name = "simpleButton_Add";
            this.simpleButton_Add.Size = new System.Drawing.Size(173, 109);
            this.simpleButton_Add.TabIndex = 3;
            this.simpleButton_Add.Text = "Добавить новый документ";
            this.simpleButton_Add.Click += new System.EventHandler(this.simpleButton_Add_Click);
            // 
            // simpleButton_doc
            // 
            this.simpleButton_doc.Image = global::bies.Properties.Resources.word;
            this.simpleButton_doc.Location = new System.Drawing.Point(187, 305);
            this.simpleButton_doc.Name = "simpleButton_doc";
            this.simpleButton_doc.Size = new System.Drawing.Size(108, 110);
            this.simpleButton_doc.TabIndex = 4;
            this.simpleButton_doc.Text = "simpleButton1";
            this.simpleButton_doc.Click += new System.EventHandler(this.simpleButton_doc_Click);
            // 
            // simpleButton_Del
            // 
            this.simpleButton_Del.Location = new System.Drawing.Point(480, 306);
            this.simpleButton_Del.Name = "simpleButton_Del";
            this.simpleButton_Del.Size = new System.Drawing.Size(168, 109);
            this.simpleButton_Del.TabIndex = 5;
            this.simpleButton_Del.Text = "Удалить шаблон";
            this.simpleButton_Del.Click += new System.EventHandler(this.simpleButton_Del_Click);
            // 
            // FormTemplateDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 430);
            this.ControlBox = false;
            this.Controls.Add(this.simpleButton_Del);
            this.Controls.Add(this.simpleButton_doc);
            this.Controls.Add(this.simpleButton_Add);
            this.Controls.Add(this.simpleButton_Save);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormTemplateDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник шаблонов документов";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_templateDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_templateDoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Save;
        private DevExpress.XtraGrid.GridControl gridControl_templateDoc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_templateDoc;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Add;
        private DevExpress.XtraEditors.SimpleButton simpleButton_doc;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Del;
    }
}