using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bies
{
    public partial class FormDocNameAdd : Form
    {
        private DevExpress.XtraEditors.TextEdit title;
        public FormDocNameAdd(DevExpress.XtraEditors.TextEdit text)
        {
            InitializeComponent();
            this.title = text;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            title.Text = textEdit_title.Text;
        }
    }
}
