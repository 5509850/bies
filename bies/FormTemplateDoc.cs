using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using bies.Entity;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace bies
{
    public partial class FormTemplateDoc : Form
    {
        private int projectnameID;

        public FormTemplateDoc()
        {
            InitializeComponent();
            FillGridControl();
            CheckButton();
        }

     
        private void simpleButton_Add_Click(object sender, EventArgs e)
        {
            Files files = UploadFileDoc("Выберите документ шаблон");
            FillGridControl();
        }

        private Files UploadFileDoc(string title_dialog)
        {
            DevExpress.XtraEditors.TextEdit text = new TextEdit();
            FormDocNameAdd form = new FormDocNameAdd(text);
            form.ShowDialog();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Word doc|*.doc|Word docx|*.docx|Все файлы (*.*)|*.*",
                Title = title_dialog,
                FilterIndex = 3,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Files file = new Files();
                file.Title = text.Text;
                file.Datecreate = file.Datelastupdate = DateTime.Now;

                FileInfo fi = new FileInfo(openFileDialog.FileName);

                if (Path.GetFileName(openFileDialog.FileName).Length < 90)//сокращение имени файла - ограничение 100 символов
                    file.Filename = Path.GetFileName(openFileDialog.FileName);
                else
                    file.Filename = Path.GetFileName(openFileDialog.FileName).Remove(80) + fi.Extension;

                file.Body = utils.ReadFromFileToByte(openFileDialog.FileName);//-----------------
                file.IsFileInBaseOrInShare = true;

                file.IsNew = true;
                file.IsTemplate = true;
                if (!file.SaveTemplate())//1 сохраняем файл в БД
                {
                    MessageBox.Show("Ошибка file.SaveTemplate()");
                    return null;
                }

                
                if (!utils.FilesCopyFromTo(openFileDialog.FileName, Properties.Settings.Default.SavePathShare, file.FileID.ToString(), false))
                {
                    MessageBox.Show(String.Format("Ошибка сохранения файла из {0} в {1}", openFileDialog.FileName,
                                    Properties.Settings.Default.SavePathShare));
                    return null;
                }

                return file;
            }
            openFileDialog.Dispose();
            return null;
        }

        private void FillGridControl()
        {
            Files file = new Files();
            gridControl_templateDoc.DataSource = file.GetListAllUsers();
            for (int i = 0; i < gridView_templateDoc.Columns.Count; i++)
            {
                if ( i != 1 && i != 2 && i != 4)
                {
                    gridView_templateDoc.Columns[i].Visible = false;
                }

                if (i == 1 )
                {
                    gridView_templateDoc.Columns[i].Caption = "Название";
                }
                if (i == 2 )
                {
                    gridView_templateDoc.Columns[i].Caption = "Имя файла";
                }
                if (i == 4 )
                {
                    gridView_templateDoc.Columns[i].Caption = "Дата создания";
                }
                
            }
        }

        private void gridView_projectname_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CheckButton();
        }

        private void CheckButton()
        {
            if (GetSelectedFile() == null)
            {
                simpleButton_Del.Enabled =
                    simpleButton_doc.Enabled = false;
            }
            else
            {
                simpleButton_Del.Enabled =
                    simpleButton_doc.Enabled = true;
            }
        }

        private Files GetSelectedFile()
        {
            if (gridView_templateDoc.RowCount == 0)
                return null;
            if (gridView_templateDoc.GetSelectedRows().Length == 0)
                return null;  
           return (Files)gridView_templateDoc.GetRow(gridView_templateDoc.GetSelectedRows()[0]);
        }

        private void simpleButton_doc_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void gridView_templateDoc_DoubleClick(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            Files files = GetSelectedFile();
            if (files == null)
                return;
            files.Body = files.GetFileBodyByFileID();
            //Просмотр документа
            if (files.Body != null)
            {
                utils.SaveBytesToFile(Path.Combine(Path.GetTempPath(), files.Filename), files.Body);
            }
            else
            {
                utils.SaveBytesToFile(Path.Combine(Path.GetTempPath(), files.Filename), files.GetFileBodyByFileID());
            }

            utils.OpenFileInWordOrExcel(Path.GetTempPath(), files.Filename);
        }

        private void simpleButton_Del_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Удалить шаблон?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.OK:
                    {
                        Files file = GetSelectedFile();
                        if (file == null)
                            return;
                        if (!file.DeleteTemplate())
                            MessageBox.Show("Ошибка удаления!");
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
