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
    public partial class FormAddCurrency : Form
    {
        private bool CreateOrEdit;
        private readonly Currency currency;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private bool blocked;
        private bool isNew;
        public FormAddCurrency(Currency currency, bool isNew, DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            InitializeComponent();
            this.currency = currency;
            this.gv = gv;
            this.isNew = isNew;
            if (!isNew)
            {
                CreateOrEdit = false;
                this.Text = String.Format("Редактирование курсов валют на дату {0}", currency.Byr.Date);
            }
            else
            {
                CreateOrEdit = true;
                this.Text = "Добавление новых курсов валют";
            } 
            textEdit_USD.EditValue = currency.Usd.Rate;
            textEdit_EUR.EditValue = currency.Eur.Rate;
            textEdit_RUB.EditValue = currency.Rub.Rate;
            dateEdit_date.EditValue = currency.Byr.Date;
            
        }

        private void simpleButton_Save_Click(object sender, EventArgs e)
        {
            currency.Usd.Rate = Convert.ToDouble(textEdit_USD.EditValue);
            currency.Eur.Rate = Convert.ToDouble(textEdit_EUR.EditValue);
            currency.Rub.Rate = Convert.ToDouble(textEdit_RUB.EditValue);

            currency.Byr.Date =
                currency.Eur.Date =
                currency.Rub.Date =
                currency.Usd.Date = Convert.ToDateTime(dateEdit_date.EditValue);
            if (CheckExistData() && isNew) //если создаеются новые курсы и на такую дату курс уже есть.
            {
                blocked = true;
                MessageBox.Show(String.Format("На дату {0} уже есть курсы валют - выберите другую дату или редактируйте эту вместо добавления новой", Convert.ToDateTime(dateEdit_date.EditValue).Date.ToShortDateString()));
            }
            else
            {
                blocked = false;
            }
        }

        private bool CheckExistData()
        {
            if (gv.RowCount == 0)
                return false;
            for (int row = 0; row < gv.RowCount; row++)
            {
                if (Convert.ToDateTime(gv.GetRowCellValue(row, "date")).Date == Convert.ToDateTime(dateEdit_date.EditValue).Date)
                    return true;
            }
            return false;
        }

        private void FormAddCurrency_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = blocked; //блокирование закрытия формы
        }

        private void textEdit_USD_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit_USD.EditValue == null)
                return;
            if (Convert.ToDouble(textEdit_USD.EditValue) < 0)
                textEdit_USD.EditValue = 0;
        }

        private void textEdit_EUR_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit_EUR.EditValue == null)
                return;
            if (Convert.ToDouble(textEdit_EUR.EditValue) < 0)
                textEdit_EUR.EditValue = 0;
        }

        private void textEdit_RUB_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit_RUB.EditValue == null)
                return;
            if (Convert.ToDouble(textEdit_RUB.EditValue) < 0)
                textEdit_RUB.EditValue = 0;
        }

        private void simpleButton_Cancel_Click(object sender, EventArgs e)
        {
            blocked = false;
        }


    }
}
