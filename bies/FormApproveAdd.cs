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
    public partial class FormApproveAdd : Form
    {
        private Approve approve;
        public FormApproveAdd(Approve aprrove)
        {
            InitializeComponent();
            this.approve = aprrove;
            this.Text = this.approve.IsNew ? "Одобрить / Замечание" : "Редактирование замечания";
            FillData();
        }

        private void FillData()
        {
            textEdit_title.Text = approve.Title;
            memoEdit_remark.Text = approve.Remark;
        }

        private void simpleButton_Yes_Click(object sender, EventArgs e)
        {
            if (textEdit_title.Text.Equals("Не одобрено") || textEdit_title.Text.Length == 0)
                textEdit_title.Text = "Одобрено";
            SaveInfo();
        }

        private void simpleButton_No_Click(object sender, EventArgs e)
        {
            if (textEdit_title.Text.Equals("Одобрено") || textEdit_title.Text.Length == 0)
                textEdit_title.Text = "Не одобрено";
            SaveInfo();
        }

        private void SaveInfo()
        {
            approve.Title = textEdit_title.Text;
            if (memoEdit_remark.Text.Length > 200)
                memoEdit_remark.Text = memoEdit_remark.Text.Remove(198);
            approve.Remark = memoEdit_remark.Text;
        }
    }
}
