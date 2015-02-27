using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using bies.Entity;

namespace bies
{
    public partial class FormProjectNameCatalog : Form
    {
        private int projectnameID;
        Trades trade = new Trades();

        public FormProjectNameCatalog()
        {
            InitializeComponent();
            FillGridControl();
        }

        private void simpleButton_Edit_Click(object sender, EventArgs e)
        {
            if (projectnameID < 1)
                return;
            if (String.IsNullOrEmpty(textEdit_edit.Text))
            {
                MessageBox.Show("Не введено название!");
                textEdit_edit.Focus();
                return;
            }
            trade.UpdateProjectName(projectnameID, textEdit_edit.Text);
            FillGridControl();
        }
        
        private void simpleButton_Add_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textEdit_edit.Text))
            {
                MessageBox.Show("Не введено название!");
                textEdit_edit.Focus();
                return;
            }
            switch (MessageBox.Show(String.Format("Добавить '{0}'?", textEdit_edit.Text), "Создание нового названия проекта", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        trade.CreateProjectName(textEdit_edit.Text);
                        FillGridControl();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }

            
        }

        private void FillGridControl()
        {
            trade.FillProjectNameGrid(gridControl_projectname);
        }

        private void gridView_projectname_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            projectnameID = 0;
            if (gridView_projectname.RowCount == 0)
                return;
            if (gridView_projectname.GetSelectedRows().Length == 0 || gridView_projectname.GetSelectedRows() == null)
                return;
            textEdit_edit.Text = gridView_projectname.GetRowCellValue(gridView_projectname.GetSelectedRows()[0], "projectname").ToString();


            var vol4 = gridView_projectname.GetRowCellValue(gridView_projectname.GetSelectedRows()[0], "projectnameID");
            if (vol4 != null)
            {
                Int32.TryParse(vol4.ToString(), out projectnameID);
            }
        }

        private void simpleButton_Del_Click(object sender, EventArgs e)
        {
            if (projectnameID < 1)
                return;
            switch (MessageBox.Show(String.Format("Удалить '{0}'?", textEdit_edit.Text), "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        trade.DeleteProjectName(projectnameID);
                        FillGridControl();   
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }
    }
}
